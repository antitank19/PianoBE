using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.CustomException;
using DataLayer.DbObject;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Services.Interface;
using DataLayer.EnumsAndConsts;

namespace ServiceLayer.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;
        private readonly ILogger<EmailService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger, UserManager<User> userManager)
        {
            _senderEmail = configuration["EmailSettings:Sender"]
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, "Email Sender is not configured");
            var password = configuration["EmailSettings:Password"];
            var host = configuration["EmailSettings:Host"];
            var port = int.Parse(configuration["EmailSettings:Port"]
                ?? throw new ErrorException(StatusCodes.Status400BadRequest, "Email port is not configured"));
            
            _smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, password)
            };

            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body)
        {
            try
            {
                foreach (var to in toList)
                {
                    var mailMessage = new MailMessage(_senderEmail, to, subject, body) { IsBodyHtml = true };
                    await _smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");
                return false;
            }
            return true;
        }

        public async Task ForgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, "User not found");
                if(user.LoginTypeEnum != LoginTypeEnum.DATABASE)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "User not found");
                }
                var code = new Random().Next(1000, 9999).ToString();
                var codeHash = _userManager.PasswordHasher.HashPassword(user, code);

                user.EmailCode = codeHash;
                user.CodeGeneratedTime = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                var selectedEmail = new List<string> { email };
                var body = $@"
    <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
        <h2 style='color: #333;'>Password Reset Request</h2>
        <p>Dear User,</p>
        <p>We received a request to reset your password. Please use the code below to proceed:</p>
        <div style='margin: 20px 0; padding: 10px; border: 1px solid #ccc; border-radius: 5px; background-color: #f9f9f9; text-align: center;'>
            <span style='font-size: 24px; font-weight: bold; color: #d9534f;'>{code}</span>
        </div>
        <p>If you did not request a password reset, please ignore this email.</p>
        <p>Thank you,<br/>Your Company Team</p>
    </div>";
                await SendEmailAsync(selectedEmail, "Password Reset Code", body);
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        public async Task<string> VerifyCode(string email, string code)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email)
                    ?? throw new ErrorException(StatusCodes.Status404NotFound, "User not found");

                if (user.CodeGeneratedTime.Value.AddMinutes(30) < DateTime.UtcNow)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "Code expired");

                }

                var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.EmailCode, code);
                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "Invalid code");
                }

                return GenerateJwtToken(user.Email);
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        public async Task<IdentityResult> ResetPassword(string token, string newPassword, string ConfirmPassword)
        {
            try
            {
                var principal = GetPrincipalFromToken(token);
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "Invalid token");
                }

                if (newPassword != ConfirmPassword)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "ConfirmPassword is not true");
                }
                
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new ErrorException(StatusCodes.Status404NotFound, "User not found");
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    user.EmailCode = null;
                    user.CodeGeneratedTime = null;
                    await _userManager.UpdateAsync(user);
                    var selectedEmail = new List<string> { email };
                    var time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    var body = $"<p>Your password has been successfully changed at: <strong>{time}</strong></p>";
                    await SendEmailAsync(selectedEmail, "Password Changed Successfully", body);
                    return IdentityResult.Success;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ErrorException(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return principal;
            }
            catch
            {
                throw new ErrorException(StatusCodes.Status404NotFound, "Invalid token");
            }
        }
    }
}
