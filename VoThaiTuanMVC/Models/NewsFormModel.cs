using System.ComponentModel.DataAnnotations;

namespace VoThaiTuanMVC.Models;

/// <summary>Dữ liệu cho form tạo/cập nhật news article (hiển thị trong popup dialog).</summary>
public class NewsFormModel
{
    public string? NewsArticleId { get; set; }

    [Required(ErrorMessage = "News title is required.")]
    [StringLength(400)]
    [Display(Name = "News Title")]
    public string? NewsTitle { get; set; }

    [Required(ErrorMessage = "Headline is required.")]
    [StringLength(150)]
    public string? Headline { get; set; }

    [StringLength(4000)]
    [Display(Name = "News Content")]
    public string? NewsContent { get; set; }

    [StringLength(400)]
    [Display(Name = "News Source")]
    public string? NewsSource { get; set; }

    [Display(Name = "Category")]
    public short? CategoryId { get; set; }

    [Display(Name = "Active")]
    public bool NewsStatus { get; set; } = true;

    public List<int> TagIds { get; set; } = new();
}
