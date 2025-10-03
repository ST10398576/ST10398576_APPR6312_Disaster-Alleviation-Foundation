using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using ST10398576_Disaster_Alleviation_Foundation.ViewModels;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register() => View(new ViewModels.RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(ViewModels.RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new AppUser
            {
                UserName = vm.Email,
                Email = vm.Email,
                FullName = vm.FullName
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
                return View(vm);
            }

            var roleName = string.IsNullOrWhiteSpace(vm.Role) ? "Donor" : vm.Role;
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new UserRole { Name = roleName });

            await _userManager.AddToRoleAsync(user, roleName);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() => View(new ViewModels.LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(ViewModels.LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
