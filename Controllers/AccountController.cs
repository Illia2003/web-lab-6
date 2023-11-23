using LR4.DbConnection;
using LR4.Entities;
using LR4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LR4.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly DataContext _context;

    public AccountController
    (
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        RoleManager<ApplicationRole> roleManager,
        DataContext context
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public IActionResult Login([FromQuery] string returnUrl = "/Home/Index")
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        LoginModel model,
        CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Email == model.Username, cancellationToken);

        if (user is null) throw new Exception("Користувача з даною поштою не існує");

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        if (!result.Succeeded)
        {
            throw new Exception("Пароль неправильний");
        }

        return Redirect(model.ReturnUrl);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken = default)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            UserName = String.Concat(model.FirstName, model.LastName, model.Email),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied([FromQuery] string returnUrl)
    {
        return View();
    }
}
