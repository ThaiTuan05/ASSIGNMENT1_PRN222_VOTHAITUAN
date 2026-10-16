using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

/// <summary>Staff quản lý news article (bao gồm tags) + xem lịch sử news do mình tạo.</summary>
public class NewsArticleController : BaseController
{
    private readonly INewsArticleService _newsArticleService;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;

    public NewsArticleController(
        INewsArticleService newsArticleService,
        ICategoryService categoryService,
        ITagService tagService)
    {
        _newsArticleService = newsArticleService;
        _categoryService = categoryService;
        _tagService = tagService;
    }

    private IActionResult? Guard()
    {
        return IsStaff ? null : RedirectToAction("Login", "Account");
    }

    public IActionResult Index(string? search, short? categoryId)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        return View(BuildViewModel(search, categoryId));
    }

    public IActionResult MyHistory()
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (CurrentUserId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.History = _newsArticleService.GetNewsArticlesByCreator(CurrentUserId.Value);
        return View();
    }

    public IActionResult Details(string id)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        var news = _newsArticleService.GetNewsArticleById(id);
        if (news == null)
        {
            return NotFound();
        }

        return View(news);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NewsFormModel form)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, null, form, "create"));
        }

        if (CurrentUserId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        _newsArticleService.SaveNewsArticle(ToEntity(form), CurrentUserId.Value);
        TempData["Success"] = "News article created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(NewsFormModel form)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        if (!ModelState.IsValid)
        {
            return View("Index", BuildViewModel(null, null, form, "edit"));
        }

        if (CurrentUserId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        _newsArticleService.UpdateNewsArticle(ToEntity(form), CurrentUserId.Value);
        TempData["Success"] = "News article updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string id)
    {
        if (Guard() is { } redirect)
        {
            return redirect;
        }

        _newsArticleService.DeleteNewsArticle(id);
        TempData["Success"] = "News article deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private static NewsArticle ToEntity(NewsFormModel form)
    {
        return new NewsArticle
        {
            NewsArticleId = form.NewsArticleId ?? string.Empty,
            NewsTitle = form.NewsTitle,
            Headline = form.Headline!,
            NewsContent = form.NewsContent,
            NewsSource = form.NewsSource,
            CategoryId = form.CategoryId,
            NewsStatus = form.NewsStatus,
            Tags = form.TagIds.Select(id => new Tag { TagId = id }).ToList()
        };
    }

    private NewsIndexViewModel BuildViewModel(
        string? search,
        short? categoryId,
        NewsFormModel? form = null,
        string? modal = null)
    {
        return new NewsIndexViewModel
        {
            NewsArticles = _newsArticleService.SearchNewsArticles(search, categoryId, activeOnly: false),
            Categories = _categoryService.GetCategories(),
            Tags = _tagService.GetTags(),
            Search = search,
            CategoryId = categoryId,
            Form = form ?? new NewsFormModel { NewsStatus = true },
            OpenModal = modal
        };
    }
}
