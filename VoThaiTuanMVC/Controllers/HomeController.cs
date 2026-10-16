using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

public class HomeController : BaseController
{
    private readonly INewsArticleService _newsArticleService;
    private readonly ICategoryService _categoryService;
    private readonly ITagService _tagService;

    public HomeController(
        INewsArticleService newsArticleService,
        ICategoryService categoryService,
        ITagService tagService)
    {
        _newsArticleService = newsArticleService;
        _categoryService = categoryService;
        _tagService = tagService;
    }

    /// <summary>Trang public: chỉ hiển thị news có NewsStatus = active (không cần đăng nhập).</summary>
    public IActionResult Index(string? search, short? categoryId)
    {
        var articles = _newsArticleService.SearchNewsArticles(search, categoryId, activeOnly: true);
        var allActive = _newsArticleService.GetActiveNewsArticles();

        var suggestions = new List<BusinessObjects.Entities.NewsArticle>();
        if (articles.Count == 0 || !string.IsNullOrWhiteSpace(search) || categoryId.HasValue)
        {
            suggestions = allActive
                .Where(n => articles.All(a => a.NewsArticleId != n.NewsArticleId))
                .Take(4)
                .ToList();
        }

        var counts = allActive
            .Where(a => a.CategoryId.HasValue)
            .GroupBy(a => a.CategoryId!.Value)
            .ToDictionary(g => g.Key, g => g.Count());

        var model = new HomeIndexViewModel
        {
            Articles = articles,
            Suggestions = suggestions,
            Categories = _categoryService.GetCategories(),
            Tags = _tagService.GetTags(),
            CategoryCounts = counts,
            TitleSuggestions = allActive
                .Select(a => a.NewsTitle ?? string.Empty)
                .Where(t => t.Length > 0)
                .Distinct()
                .OrderBy(t => t)
                .ToList(),
            Search = search,
            CategoryId = categoryId
        };

        return View(model);
    }

    public IActionResult Details(string id)
    {
        var news = _newsArticleService.GetNewsArticleById(id);
        if (news == null)
        {
            return NotFound();
        }

        // News inactive chỉ cho Staff/Admin xem.
        if (news.NewsStatus != true && !IsStaff && !IsAdmin)
        {
            return NotFound();
        }

        ViewBag.Related = _newsArticleService
            .SearchNewsArticles(null, news.CategoryId, activeOnly: true)
            .Where(n => n.NewsArticleId != news.NewsArticleId)
            .Take(4)
            .ToList();

        return View(news);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
