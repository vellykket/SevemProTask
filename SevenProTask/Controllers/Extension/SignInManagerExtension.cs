using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SevenProTask.Models;

namespace SevenProTask.Controllers.Extension
{
    internal static class SignInManagerExtension
    {
        public static async Task<SignInResult> PasswordSignInAsyncEmail(this SignInManager<User> signInManager,
            UserManager<User> UserManager, string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}