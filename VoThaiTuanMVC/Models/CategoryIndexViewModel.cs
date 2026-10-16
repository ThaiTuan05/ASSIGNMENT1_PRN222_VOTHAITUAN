using BusinessObjects.Entities;

namespace VoThaiTuanMVC.Models;

public class CategoryIndexViewModel
{
    public List<Category> Categories { get; set; } = new();
    public string? Search { get; set; }
    public Category Form { get; set; } = new();
    public string? OpenModal { get; set; }
}
