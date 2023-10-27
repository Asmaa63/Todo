using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using project1.Context;
using project1.Models;
using project1.ViewModel;
using System.Security.Claims;

namespace project1.Controllers
{
    public class AccountController : Controller
    {
        TodoContext context = new TodoContext();
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult SubmitRegister(User user)
        {
            if (ModelState.IsValid)
            {
                context.users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index", "Todo");
            }
            return View("Register", user);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> SubmitLogin(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = context.users.FirstOrDefault(a => (a.email == login.email && a.password == login.password));
                if (user == null)
                {
                    ModelState.AddModelError("", "Email or password is wrong");
                    return View("Login", login);
                }
                else
                {
                    var claimsidentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsidentity.AddClaim(new Claim(ClaimTypes.Sid, $"{user.id.ToString()}"));
                    claimsidentity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

                    var princcalim = new ClaimsPrincipal(claimsidentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princcalim,
                        new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddHours(8)
                        });
                    return RedirectToAction("Index", "Todo");
                }

            }
            return View("Login", login);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Todo");
        }
	}
}
