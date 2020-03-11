using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deskrs.DAL.EF.Entities;
using Deskrs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Deskrs.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        //private readonly IStoreService _storeService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //_storeService = storeService;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= "/home/index";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation",
                                              new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= "/home/index";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            //if (ModelState.IsValid)
            //{
            //    // This doesn't count login failures towards account lockout
            //    // To enable password failures to trigger account lockout, 
            //    // set lockoutOnFailure: true
            //    var result = await signInManager.PasswordSignInAsync(Input.Email,
            //                       Input.Password, Input.RememberMe, lockoutOnFailure: true);
            //    if (result.Succeeded)
            //    {
            //        _logger.LogInformation("User logged in.");
            //        return LocalRedirect(returnUrl);
            //    }
            //    if (result.RequiresTwoFactor)
            //    {
            //        return RedirectToPage("./LoginWith2fa", new
            //        {
            //            ReturnUrl = returnUrl,
            //            RememberMe = Input.RememberMe
            //        });
            //    }
            //    if (result.IsLockedOut)
            //    {
            //        _logger.LogWarning("User account locked out.");
            //        return RedirectToPage("./Lockout");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //        return Page();
            //    }
            //}

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}