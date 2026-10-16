using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

/// <summary>Staff quản lý category (CRUD + search). Create/Update dùng popup dialog.</summary>
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    private IActionResult? Guard()
    {
        return IsStaff ? null : RedirectToAction("Login", "Account");
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
    public IActionResult Create(Category model)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, model, "create"));
        }

        _categoryService.SaveCategory(model);
        TempData["Success"] = "Category created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category model)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, model, "edit"));
        }

        _categoryService.UpdateCategory(model);
        TempData["Success"] = "Category updated successfully.";
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

        if (_categoryService.DeleteCategory(id, out var message))
        {
            TempData["Success"] = message;
        }
        else
        {
            TempData["Error"] = message;
        }

        return RedirectToAction(nameof(Index));
    }

    private CategoryIndexViewModel BuildViewModel(string? search, Category? form = null, string? modal = null)
    {
        return new CategoryIndexViewModel
        {
            Categories = _categoryService.SearchCategories(search),
            Search = search,
            Form = form ?? new Category { IsActive = true },
            OpenModal = modal
        };
    }
}
