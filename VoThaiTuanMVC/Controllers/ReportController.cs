using Microsoft.AspNetCore.Mvc;
using Services;
using VoThaiTuanMVC.Models;

namespace VoThaiTuanMVC.Controllers;

/// <summary>Admin tạo thống kê news theo khoảng thời gian (theo CreatedDate), sắp xếp giảm dần.</summary>
public class ReportController : BaseController
{
    private readonly INewsArticleService _newsArticleService;

    public ReportController(INewsArticleService newsArticleService)
    {
        _newsArticleService = newsArticleService;
    }

    public IActionResult Index()
    {
        if (!IsAdmin)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new ReportViewModel
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ReportViewModel model)
    {
        if (!IsAdmin)
        {
            return RedirectToAction("Login", "Account");
        }

        if (model.StartDate == null || model.EndDate == null)
        {
            ModelState.AddModelError(string.Empty, "Please select both start date and end date.");
            return View(model);
        }

        if (model.StartDate > model.EndDate)
        {
            ModelState.AddModelError(string.Empty, "Start date must be earlier than or equal to end date.");
            return View(model);
        }

        var results = _newsArticleService.GetReport(model.StartDate.Value, model.EndDate.Value);
        model.Results = results;
        model.TotalArticles = results.Count;
        model.Searched = true;

        return View(model);
    }
}
