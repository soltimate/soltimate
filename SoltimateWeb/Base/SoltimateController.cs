using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoltimateLib.Models.Identity;
using SoltimateLib.Services;
using SoltimateWeb.Controllers;
using SoltimateWeb.Data;
using System;
using System.Text;
using System.Text.Encodings.Web;

namespace SoltimateWeb.Base
{
    public abstract class SoltimateController : Controller
    {
        protected readonly UserManager<SoltimateUser> _userManager;
        protected readonly IEmailSender _emailSender;
        protected readonly ILogger _logger;
        protected readonly UrlEncoder _urlEncoder;
        protected readonly SoltimateContext _context;

        /// <summary>
        /// Create new instance of the Base controller.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="logger"></param>
        /// <param name="urlEncoder"></param>
        public SoltimateController(
            UserManager<SoltimateUser> userManager,
            IEmailSender emailSender,
            ILogger logger,
            UrlEncoder urlEncoder,
            SoltimateContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _context = context;
        }
        /// <summary>
        /// Store error message temporarily within the controller.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Store status message temporarily within the controller.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public IFormatProvider AuthenicatorUriFormat { get; private set; }


        #region Helpers

        /// <summary>
        /// The default home redirect.
        /// </summary>
        /// <returns>Redirect to home.</returns>
        protected RedirectToActionResult RedirectHome()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
        }

        /// <summary>
        /// Add errors from IdentityResult to ModelState.
        /// </summary>
        /// <param name="result">IdentityResult where we check the errors from.</param>
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// Redirect to local URL.
        /// </summary>
        /// <param name="returnUrl">URL that we redirect to.</param>
        /// <returns>Redirect action.</returns>
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Format the given key.
        /// </summary>
        /// <param name="unformattedKey">Unformatted key.</param>
        /// <returns>Formatted key.</returns>
        protected string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Generate QrCode to email based on unformatted key.
        /// </summary>
        /// <param name="email">Target email.</param>
        /// <param name="unformattedKey">Unformatted key.</param>
        /// <returns>QrCodeUri.</returns>
        protected string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                _urlEncoder.Encode("GenerateTest"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        #endregion
    }
}
