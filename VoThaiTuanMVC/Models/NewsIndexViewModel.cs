using BusinessObjects.Entities;

namespace VoThaiTuanMVC.Models;

public class NewsIndexViewModel
{
    public List<NewsArticle> NewsArticles { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();

    public string? Search { get; set; }
    public short? CategoryId { get; set; }

    /// <summary>Dữ liệu form khi cần mở lại popup (validate lỗi).</summary>
    public NewsFormModel Form { get; set; } = new();

    /// <summary>"create" hoặc "edit" để tự động mở lại popup sau khi post lỗi.</summary>
    public string? OpenModal { get; set; }
}
