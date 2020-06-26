using System;
using System.Security.Claims;
using System.Threading.Tasks;

using AllInterfaces.Interface;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ModelLayer;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

namespace WebRepository.Controllers {
    public class AccountController : Controller {
        public UserManager<IdentityUser> _UserManager { get; }
        public SignInManager<IdentityUser> _SignInManager { get; }
        public IMessageSender _MessageSender { get; }
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMessageSender messageSender) {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _MessageSender = messageSender;
        }

        [Obsolete]
        public IActionResult Register() {
            if (ModelState.IsValid)
                if (_SignInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");
            return View();
            //fe80::4d1a:45ce:161e:3bb6%43  <=  خروجی
            //using System.Net;
            //string dns = Dns.GetHostName();
            //string ipa = Dns.GetHostByName(dns).AddressList[0].ToString();


            //14DDA9E9C010   <=  خروجی
            //using System.Net.NetworkInformation;
            //NetworkInterface[] nic = NetworkInterface.GetAllNetworkInterfaces();
            //String sMACA = string.Empty;
            //foreach (NetworkInterface item in nic) {
            //    if (sMACA == String.Empty) {
            //        IPInterfaceProperties iP = item.GetIPProperties();
            //        String mac = sMACA = item.GetPhysicalAddress().ToString();
            //    }
            //}



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                if (_SignInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");
                model.Time = DateTime.Now;
                var user = new IdentityUser() { UserName = model.UserName, Email = model.Email };
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    var emailConfirmToken = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailMessage = Url.Action("ConfirmEmail", "Account",
                        (userName: user.UserName, token: emailConfirmToken),
                        Request.Scheme);
                    await _MessageSender.SendEmailAsync(model.Email, "Email Confirmation", emailMessage);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Login(string returnUrl = null) {
            if (ModelState.IsValid) {
                if (_SignInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");
                var model = new LoginViewModel() {
                    ReturnUrl = returnUrl,
                    ExternalLogin = await _SignInManager.GetExternalAuthenticationSchemesAsync()
                };
                ViewData["returnUrl"] = returnUrl;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null) {
            if (ModelState.IsValid) {
                if (_SignInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");
                ViewData["returnUrl"] = returnUrl;
                if (ModelState.IsValid) {
                    var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                    if (result.Succeeded) {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        return RedirectToAction("Index", "Home");
                    }
                    if (result.IsLockedOut) { ViewData["ErrorMessage"] = "اکانت شما قفل شد"; return View(model); }
                    ModelState.AddModelError("", "رمز عبور یا ایمیل شما اشتباه است");
                }
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut() {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsEmailIsUse(string email) {
            var user = await _UserManager.FindByEmailAsync(email);
            if (user == null) return Json(true);
            return Json("ایمیل وارد شده از قبل موجود می باشد");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsUserIsUse(string userName) {
            var user = await _UserManager.FindByNameAsync(userName);
            if (user == null) return Json(true);
            return Json("نام کاربری وارد شده از قبل موجود می باشد");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token) {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token)) return NotFound();
            var user = await _UserManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var result = await _UserManager.ConfirmEmailAsync(user, token);
            return Conflict(result.Succeeded ? "EmailConfirm" : "EmailNotConfirm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl) {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
                new { ReturnUrl = returnUrl });

            var properties = _SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null) {
            returnUrl =
                (returnUrl != null && Url.IsLocalUrl(returnUrl)) ? returnUrl : Url.Content("~/");

            var loginViewModel = new LoginViewModel() {
                ReturnUrl = returnUrl,
                ExternalLogin = await _SignInManager.GetExternalAuthenticationSchemesAsync()
            };

            if (remoteError != null) {
                ModelState.AddModelError("", $"Error : {remoteError}");
                return View("Login", loginViewModel);
            }

            var externalLoginInfo = await _SignInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null) {
                ModelState.AddModelError("ErrorLoadingExternalLoginInfo", $"مشکلی پیش آمد");
                return View("Login", loginViewModel);
            }

            var signInResult = await _SignInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, false, true);

            if (signInResult.Succeeded) {
                return Redirect(returnUrl);
            }

            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

            if (email != null) {
                var user = await _UserManager.FindByEmailAsync(email);
                if (user == null) {
                    var userName = email.Split('@')[0];
                    user = new IdentityUser() {
                        UserName = (userName.Length <= 10 ? userName : userName.Substring(0, 10)),
                        Email = email,
                        EmailConfirmed = true
                    };

                    await _UserManager.CreateAsync(user);
                }

                await _UserManager.AddLoginAsync(user, externalLoginInfo);
                await _SignInManager.SignInAsync(user, false);

                return Redirect(returnUrl);
            }

            ViewBag.ErrorTitle = "لطفا با بخش پشتیبانی تماس بگیرید";
            ViewBag.ErrorMessage = $"دریافت کرد {externalLoginInfo.LoginProvider} نمیتوان اطلاعاتی از";
            return View();
        }
    }
}