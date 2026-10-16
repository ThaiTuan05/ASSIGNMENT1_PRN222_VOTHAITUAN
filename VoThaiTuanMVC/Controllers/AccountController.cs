using BusinessObjects.Common;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

public class AccountController : BaseController
{
    private readonly ISystemAccountService _accountService;
    private readonly IConfiguration _configuration;

    public AccountController(ISystemAccountService accountService, IConfiguration configuration)
    {
        _accountService = accountService;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // 1. Admin account lấy từ appsettings.json
        var adminEmail = _configuration["AdminAccount:Email"];
        var adminPassword = _configuration["AdminAccount:Password"];
        if (model.Email == adminEmail && model.Password == adminPassword)
        {
            SetSession(0, "Administrator", adminEmail, Roles.Admin);
            return RedirectToAction("Index", "Report");
        }

        // 2. Member account lấy từ database
        var user = _accountService.Authenticate(model.Email, model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        SetSession(user.AccountId, user.AccountName, user.AccountEmail, user.AccountRole);

        if (user.AccountRole == Roles.Staff)
        {
            return RedirectToAction("Index", "NewsArticle");
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    /// <summary>Staff xem và cập nhật profile của chính mình.</summary>
    [HttpGet]
    public IActionResult Profile()
    {
        if (!IsStaff || CurrentUserId == null)
        {
            return RedirectToAction("Login");
        }

        var account = _accountService.GetAccountById(CurrentUserId.Value);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Profile([Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount model)
    {
        if (!IsStaff || CurrentUserId == null)
        {
            return RedirectToAction("Login");
        }

        // Chỉ cho phép cập nhật tài khoản của chính mình và không đổi role.
        model.AccountId = CurrentUserId.Value;
        model.AccountRole = Roles.Staff;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _accountService.UpdateAccount(model);

        // Cập nhật lại session
        HttpContext.Session.SetString("UserName", model.AccountName ?? string.Empty);
        HttpContext.Session.SetString("UserEmail", model.AccountEmail ?? string.Empty);

        ViewBag.Message = "Profile updated successfully.";
        return View(model);
    }

    private void SetSession(short userId, string? userName, string? email, int? role)
    {
        HttpContext.Session.SetInt32("UserId", userId);
        HttpContext.Session.SetString("UserName", userName ?? string.Empty);
        HttpContext.Session.SetString("UserEmail", email ?? string.Empty);
        HttpContext.Session.SetInt32("UserRole", role ?? Roles.Lecturer);
    }
}
