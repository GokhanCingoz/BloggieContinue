using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModal registerViewModal)
        {
            var identityUser = new IdentityUser
            {
                Email = registerViewModal.Email,
                UserName = registerViewModal.UserName,

            };

            var identityResult = await userManager.CreateAsync(identityUser,registerViewModal.Password);

            if (identityResult.Succeeded)
            {
                //bu kullanıcıya rolünü verelim

                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }

            }

            return View();
        }
    }
}
