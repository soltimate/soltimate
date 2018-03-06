using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoltimateLib.Models.Identity;
using SoltimateLib.Services;
using SoltimateWeb.Base;
using SoltimateWeb.Controllers;
using SoltimateWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SoltimateWeb.Areas.Account.ViewModels.Auth;

namespace SoltimateWeb.Areas.Account.Controllers
{
    [Area("Account")]
    public class AuthController : SoltimateController
    {
        private readonly SignInManager<SoltimateUser> _signInManager;

        /// <summary>
        /// Create new instance of the Auth controller.
        /// </summary>
        public AuthController(
            UserManager<SoltimateUser> userManager,
            IEmailSender emailSender,
            ILogger<AuthController> logger,
            UrlEncoder urlEncoder,
            SoltimateContext context,
            SignInManager<SoltimateUser> signInManager)
        : base(userManager, emailSender, logger, urlEncoder, context)
        {
            _signInManager = signInManager;
        }

        #region login

        /// <summary>
        /// Return the login view.
        /// </summary>
        /// <param name="returnUrl">Return url for after the login.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Attempt the login based on POST variables.
        /// </summary>
        /// <param name="model">ViewModel that validates the data.</param>
        /// <param name="returnUrl">Return URL on case of success.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectHome();
                    //return RedirectToLocal(returnUrl); //<-- Area goes wrong, use this if you fix it.
                }
                /*if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }*/
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region register

        /// <summary>
        /// Return the register view.
        /// </summary>
        /// <param name="returnUrl">Return url for after the register.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Attempt the register based on POST variables.
        /// </summary>
        /// <param name="model">ViewModel that validates the data.</param>
        /// <param name="returnUrl">Return URL on case of success.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new SoltimateUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectHome();
                    //return RedirectToLocal(returnUrl); //<-- Area goes wrong, use this if you fix it.
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        /// <summary>
        /// Log user out of the web-application.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectHome();
        }

        #region auth

        /// <summary>
        /// Show the lockout screen for user when user is locked out.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// Show access denied screen for user without access.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion
    }
}
