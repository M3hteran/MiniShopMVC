using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniShopMVC.Models;
using MiniShopMVC.Services.Interfaces;
using MiniShopMVC.ViewModels;
using System.Security.Claims;

namespace MiniShopMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAppUserService _appUserService;

		public AccountController(IAppUserService appUserService)
		{
			_appUserService = appUserService;
		}
		//----------------------------------------------GET----------------------------------------
		public IActionResult Login(string? returnUrl)
		{
			var model = new LoginViewModel
			{
				ReturnUrl = returnUrl
			};

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "Account");
		}

		public IActionResult Register()
		{
			return View();
		}

		//----------------------------------------------POST----------------------------------------

		[HttpPost]

		public async Task<IActionResult> Login(LoginViewModel model)
		{

			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = await _appUserService.GetByUserNameAsync(model.UserName);

			if (user == null)
			{
				ModelState.AddModelError("", "Kullanıcı Bulunamadı");
				return View(model);
			}


			var passwordHasher = new PasswordHasher<AppUser>();

			var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

			if (result == PasswordVerificationResult.Failed)
			{

				ModelState.AddModelError("", "Şifre Hatalı");
				return View(model);

			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{

				IsPersistent = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)

			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity), authProperties);

			if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
			{
				return Redirect(model.ReturnUrl);
			}

			return RedirectToAction("Index", "Product");
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var existingUser = await _appUserService.GetByUserNameAsync(model.UserName);
			if (existingUser != null)
			{
				ModelState.AddModelError("", "Bu Kullanıcı adı zaten var.");
				return View(model);
			}

			var exintingEmail = await _appUserService.GetByEmailAsync(model.Email);
			if (exintingEmail != null)
			{
				ModelState.AddModelError("", "Bu email zaten kullanılmaktadır.");
				return View(model);
			}

			var user = new AppUser
			{
				UserName = model.UserName,
				Email = model.Email,
				Role = "Client"
			};

			var passwordHasher = new PasswordHasher<AppUser>();
			user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

			await _appUserService.AddAsync(user);

			return RedirectToAction("Login", "Account");
		}

		//--------------------------------------AccessDenied----------------------------------

		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}
