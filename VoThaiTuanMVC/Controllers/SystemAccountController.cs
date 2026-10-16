using BusinessObjects.Common;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

/// <summary>Admin quản lý account (CRUD + search). Create/Update dùng popup dialog.</summary>
public class SystemAccountController : BaseController
{
    private readonly ISystemAccountService _accountService;

    public SystemAccountController(ISystemAccountService accountService)
    {
        _accountService = accountService;
    }

    private IActionResult? Guard()
    {
        return IsAdmin ? null : RedirectToAction("Login", "Account");
    }

    public IActionResult Index(string? search)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        return View(BuildViewModel(search));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SystemAccount model)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, model, "create"));
        }

        _accountService.SaveAccount(model);
        TempData["Success"] = "Account created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(SystemAccount model)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, model, "edit"));
        }

        _accountService.UpdateAccount(model);
        TempData["Success"] = "Account updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(short id)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        _accountService.DeleteAccount(id);
        TempData["Success"] = "Account deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private AccountIndexViewModel BuildViewModel(string? search, SystemAccount? form = null, string? modal = null)
    {
        return new AccountIndexViewModel
        {
            Accounts = _accountService.SearchAccounts(search),
            Search = search,
            Form = form ?? new SystemAccount { AccountRole = Roles.Staff },
            OpenModal = modal
        };
    }
}
