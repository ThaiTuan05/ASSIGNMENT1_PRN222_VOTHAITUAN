using BusinessObjects.Entities;

namespace VoThaiTuanMVC.Models;

public class HomeIndexViewModel
{
    public List<NewsArticle> Articles { get; set; } = new();
    public List<NewsArticle> Suggestions { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();

    /// <summary>Số bài active theo category (cho sidebar, không phụ thuộc bộ lọc).</summary>
    public Dictionary<short, int> CategoryCounts { get; set; } = new();

    /// <summary>Gợi ý tiêu đề cho ô tìm kiếm (autocomplete).</summary>
    public List<string> TitleSuggestions { get; set; } = new();

    public string? Search { get; set; }
    public short? CategoryId { get; set; }

    public NewsArticle? Featured => Articles.FirstOrDefault();
    public List<NewsArticle> Rest => Articles.Skip(1).ToList();
    public bool IsSearching => !string.IsNullOrWhiteSpace(Search) || CategoryId.HasValue;
}
