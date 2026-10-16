using System.ComponentModel.DataAnnotations;
using BusinessObjects.Entities;

namespace VoThaiTuanMVC.Models;

public class ReportViewModel
{
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    public bool Searched { get; set; }

    public int TotalArticles { get; set; }

    public List<NewsArticle> Results { get; set; } = new();
}
