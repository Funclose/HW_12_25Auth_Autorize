using HW_12_25Auth_Autorize.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace HW_12_25Auth_Autorize.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;
        public HomeController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if(ModelState.IsValid)
            {
                var currentUser = _userRepository.Users.FirstOrDefault(e => e.Email.Equals(user.Email));
                if (currentUser != null)
                {
                    if (currentUser.Password.Equals(user.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, currentUser.Email) ,
                            new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Role.Name),
                            new Claim(ClaimTypes.DateOfBirth, currentUser.CreatedAt.ToString())};
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction(nameof(Index));
                    
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Wrong Password");
                }
                ModelState.AddModelError("Email", "Can't find this email addres");
                return View(user);
            }
            else
            {
                return View(user);
            }
        }
        [Authorize(Roles ="Admin, Editor", Policy = "OnlyForAdults")]
        public IActionResult Admin()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                TempData["Success"] = "Registr successfull, Please go to login page";
                return View(user);
            }
            else
            {
                return View(user);
            }
        }
	}
}
