using DataLayer.DbObject;
using DataLayer.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Implementation;
using ServiceLayer.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServiceLayer.ModelViews;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ServiceLayer.Services;
using System.Linq;
using API.Extensions;
using ServiceLayer.ModelViews.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Drawing.Imaging;
using System.Drawing;
using Image = System.Drawing.Image;
using ServiceLayer.Utils;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly TokenService _tokenService;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IConfiguration config;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager
            , IConfiguration configuration, IEmailService emailService
            , TokenService tokenService, RoleManager<Role> roleManager
            , IServiceWrapper serviceWrapper, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _serviceWrapper = serviceWrapper;
            this.config = config;
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> UserRegister([FromForm] RegisterDto input)
        {

            var user = new User
            {
                UserName = input.Username,
                Email = input.Email,
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                DateOfBirth = DateOnly.Parse(input.DateOfBirth),
                CreatedTime = DateTime.Now,
                LoginTypeEnum = DataLayer.DbObject.Enum.LoginTypeEnum.DATABASE,
            };
            User? userInDb = _serviceWrapper.UserService.GetUserByEmail(input.Email).Result;
            if(userInDb == null)
            {
                string imgUrl = await FirebaseStorageUtil.UploadFileAsync(input.Image, "Image/Song", config["Firebase:StorageBucket"]);
                user.Image = imgUrl;
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Player");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new { message = "User registered and role assigned successfully" });
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("artist/register")]
        public async Task<IActionResult> ArtistRegister([FromForm] RegisterDto input)
        {
            var user = new User
            {
                Name = input.Name, 
                UserName = input.Username, 
                Email = input.Email, 
                CreatedTime = DateTime.Now, 
                LoginTypeEnum = DataLayer.DbObject.Enum.LoginTypeEnum.DATABASE,
                DateOfBirth = DateOnly.Parse(input.DateOfBirth),
                PhoneNumber = input.PhoneNumber };
            User? userInDb = _serviceWrapper.UserService.GetUserByEmail(input.Email).Result;
            if (userInDb == null)
            {
                string imgUrl = await FirebaseStorageUtil.UploadFileAsync(input.Image, "Image/Song", config["Firebase:StorageBucket"]);
                user.Image = imgUrl;
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Artist");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new { message = "Artist registered and role assigned successfully" });
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var role = await _userManager.GetRolesAsync(user);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleClaims = role.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = _tokenService.GenerateToken(roleClaims, authClaims);

                return Ok(new { token = token, role = role, name = user.Name, email = user.Email });
            }
            return Unauthorized();
        }

        /// <summary>
        /// Quên mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _emailService.ForgotPassword(email);
            return Ok(new BaseResponse<string>(
                "Quên mật khẩu thành công. Hãy vào email nhận mã code",
                StatusCodes.Status200OK));
        }

        /// <summary>
        /// Kiểm tra mã code hợp lệ để lấy lại mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("check-valid-code")]
        public async Task<IActionResult> CheckValidCode(string email, string code)
        {
            var isValid = _emailService.VerifyCode(email, code);
            return Ok(new BaseResponse<Task<string>>(
           "Kiểm tra mã code thành công",
            StatusCodes.Status200OK,
            isValid));
        }

        /// <summary>
        /// Đặt lại mật khẩu.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string NewPassword, string ConfirmPassword)
        {
            var check = await _emailService.ResetPassword(token, NewPassword, ConfirmPassword);
            return Ok(new BaseResponse<Task<IdentityResult>>(check.ToString()
                , StatusCodes.Status200OK));
        }

        [HttpGet("user/login-google")]
        public IActionResult UserLoginWithGoogle()
        {
            string? redirectUrl = Url.Action(nameof(HandleExternalLogin), "Auth", new { role = "Player" });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("artist/login-google")]
        public IActionResult ArtistLoginWithGoogle()
        {
            string? redirectUrl = Url.Action(nameof(HandleExternalLogin), "Auth", new { role = "Artist" });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("externallogin")]
        public async Task<IActionResult> HandleExternalLogin(string role)
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return BadRequest("External authentication failed.");

            var externalClaims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var emailClaim = externalClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var Name = externalClaims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            if (emailClaim == null)
                return BadRequest("External authentication information is missing.");
            // check if user already exist in DB => not create
            var user = await _userManager.FindByEmailAsync(emailClaim.Value);
            if (user == null)
            {
                user = new User { Name = Name.Value, Email = emailClaim.Value, UserName = Guid.NewGuid().ToString(), CreatedTime = DateTime.Now, LoginTypeEnum = DataLayer.DbObject.Enum.LoginTypeEnum.GOOGLE };
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    return BadRequest(createResult.Errors);
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    return BadRequest("Role is not exist!");
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                if (!addRoleResult.Succeeded)
                    return BadRequest(addRoleResult.Errors);
            }

            var roleClaims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var token = _tokenService.GenerateToken(roleClaims, authClaims);

            return Redirect($"http://localhost:3000?token={token}&role={role}&name={user.Email}");
        }

        [HttpGet("my-info")]
        [Authorize]
        public async Task<IActionResult> getUserByClaims()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            UserDto dto = new UserDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                Name = appUser.Name,
                DateOfBirth = appUser.DateOfBirth.ToString(),
                Roles = await _userManager.GetRolesAsync(appUser),
                Image = appUser.Image
            };
            return Ok(new BaseResponse<UserDto>("Get information user " + username + "successfully", 200, dto));
        }
    }
}
