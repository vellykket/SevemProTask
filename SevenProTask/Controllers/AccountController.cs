using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SevenProTask.Controllers.Extension;
using SevenProTask.Models;
using SevenProTask.ViewModels;

namespace SevenProTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
 
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User { Email = model.Email, UserName = model.UserName, UserLastName = model.UserLastName};
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            var result = await _signInManager
                .PasswordSignInAsyncEmail(_userManager,model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                    ? (IActionResult) Redirect(model.ReturnUrl)
                    : RedirectToAction("Index", "Home");
            
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            return View(model);

        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}