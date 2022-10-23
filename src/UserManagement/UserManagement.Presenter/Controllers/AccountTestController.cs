using System.Net;
using System.Security.Claims;
using System.Web;
using Dantooine.Server.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Sandbox.AspNetCore.Server.ViewModels.Account;
using UserManagement.Core.Domains;
using UserManagement.Presenter.ViewModels.Authorization;

namespace UserManagement.Presenter.Controllers;

[Area("Identity")]
public class AccountTestController : Controller
{
    private readonly SignInManager<User> _signInManager;

    public AccountTestController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }
    //
    // [HttpGet]
    // [AllowAnonymous]
    // public async Task<IActionResult> Login(string returnUrl = null)
    // {
    //     var vm = new LoginViewModel();
    //     await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
    //     
    //
    //     // vm.ExternalProviders= (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList().Select(a => new ExternalProvider()
    //     // {
    //     //     AuthenticationScheme = a.Name,
    //     //     DisplayName = a.DisplayName
    //     // });
    //     //
    //     // vm.ReturnUrl = returnUrl;
    //     // ViewData["ReturnUrl"] = returnUrl;
    //      return View(vm);
    // // }
    //
    //
    // [HttpPost]
    // [AllowAnonymous]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Login([FromForm]LoginViewModel model)
    // {
    //     ViewData["ReturnUrl"] = model.ReturnUrl;
    //     if (ModelState.IsValid)
    //     {
    //         // This doesn't count login failures towards account lockout
    //         // To enable password failures to trigger account lockout, set lockoutOnFailure: true
    //         var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
    //         if (result.Succeeded)
    //         {
    //             return RedirectToLocal( model.ReturnUrl);
    //         }
    //         else
    //         {
    //             ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //             return View(model);
    //         }
    //     }
    //
    //     // If we got this far, something failed, redisplay form
    //     return View(model);
    //         
    //
    //
    // }/media/morteza/6b53ded8-ff21-4348-afd5-962880213bc41/MyProjects/Mixed/Microservice Example For DrSaina/src/UserManagement

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}