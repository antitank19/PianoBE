using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body);
        Task ForgotPassword(string email);
        Task<string> VerifyCode(string email, string code);
        Task<IdentityResult> ResetPassword(string token, string newPassword, string ConfirmPassword);
    }
}
