using BusinessObjects.Entities;

namespace Services;

public interface ICategoryService
{
    List<Category> GetCategories();
    List<Category> SearchCategories(string? keyword);
    Category? GetCategoryById(short id);
    void SaveCategory(Category category);
    void UpdateCategory(Category category);

    /// <summary>Xoá category. Trả về false nếu category đang thuộc về news article nào đó.</summary>
    bool DeleteCategory(short id, out string message);
}
