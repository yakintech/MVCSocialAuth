using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MVCSocialAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCSocialAuth.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Name == "polynomtech" && model.Password == "123")
            {
                AppUserState appuserstate = new AppUserState();
                appuserstate.Name = "cagatay";
                appuserstate.UserId = "3";
                IdentitySignin(appuserstate);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }

          
        }


        public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId));
            claims.Add(new Claim(ClaimTypes.Name, appUserState.Name));

            // custom – my serialized AppUserState object
            claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }


        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider)
        //{
        //    string returnUrl = WebUtils.ResolveServerUrl("~/new");
        //    //string returnUrl = Url.Action("New", "Snippet", null);

        //    return new ChallengeResult(provider,
        //        Url.Action("ExternalLoginCallback", "Account",
        //        new { ReturnUrl = returnUrl }));
        //}


        //// Initiate oAuth call for external Login
        //// GET: /Account/ExternalLinkLogin
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLinkLogin(string provider)
        //{
        //    var id = Request.Form["Id"];

        //    // create an empty AppUser with a new generated id
        //    AppUserState model = new AppUserState();
        //    model.Name = "test";
        //    IdentitySignin(model, null);

        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("ExternalLinkLoginCallback"), "3");
        //}

        // oAuth Callback for external login
        // GET: /Manage/LinkLogin
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<ActionResult> ExternalLinkLoginCallback()
        //{
        //    // Handle external Login Callback
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, AppUserState.UserId);
        //    if (loginInfo == null)
        //    {
        //        IdentitySignout(); // to be safe we log out
        //        return RedirectToAction("Register", new { message = "Unable to authenticate with external login." });
        //    }

        //    // Authenticated!
        //    string providerKey = loginInfo.Login.ProviderKey;
        //    string providerName = loginInfo.Login.LoginProvider;

        //    // Now load, create or update our custom user

        //    // normalize email and username if available
        //    if (string.IsNullOrEmpty(AppUserState.Email))
        //        AppUserState.Email = loginInfo.Email;
        //    if (string.IsNullOrEmpty(AppUserState.Name))
        //        AppUserState.Name = loginInfo.DefaultUserName;

        //    var userBus = new busUser();
        //    User user = null;

        //    if (!string.IsNullOrEmpty(AppUserState.UserId))
        //        user = userBus.Load(AppUserState.UserId);

        //    if (user == null && !string.IsNullOrEmpty(providerKey))
        //        user = userBus.LoadUserByProviderKey(providerKey);

        //    if (user == null && !string.IsNullOrEmpty(loginInfo.Email))
        //        user = userBus.LoadUserByEmail(loginInfo.Email);

        //    if (user == null)
        //    {
        //        user = userBus.NewEntity();
        //        userBus.SetUserForEmailValidation(user);
        //    }

        //    if (string.IsNullOrEmpty(user.Email))
        //        user.Email = AppUserState.Email;

        //    if (string.IsNullOrEmpty(user.Name))
        //        user.Name = AppUserState.Name ?? "Unknown (" + providerName + ")";


        //    if (loginInfo.Login != null)
        //    {
        //        user.OpenIdClaim = loginInfo.Login.ProviderKey;
        //        user.OpenId = loginInfo.Login.LoginProvider;
        //    }
        //    else
        //    {
        //        user.OpenId = null;
        //        user.OpenIdClaim = null;
        //    }

        //    // finally save user inf
        //    bool result = userBus.Save(user);

        //    // update the actual identity cookie
        //    AppUserState.FromUser(user);
        //    IdentitySignin(AppUserState, loginInfo.Login.ProviderKey);

        //    return RedirectToAction("Register");
        //}



    }
}