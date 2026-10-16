using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

/// <summary>
/// Data Access Object cho bảng Category.
/// Mọi thao tác database đều đi qua đây (không truy cập trực tiếp từ Controller).
/// </summary>
public class CategoryDAO
{
    public List<Category> GetCategories()
    {
        using var context = new FUNewsManagementContext();
        return context.Categories
            .AsNoTracking()
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.CategoryId)
            .ToList();
    }

    public Category? GetCategoryById(short id)
    {
        using var context = new FUNewsManagementContext();
        return context.Categories
            .AsNoTracking()
            .FirstOrDefault(c => c.CategoryId == id);
    }

    public void SaveCategory(Category category)
    {
        using var context = new FUNewsManagementContext();
        // CategoryID là cột IDENTITY nên để db tự sinh.
        category.CategoryId = 0;
        context.Categories.Add(category);
        context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        using var context = new FUNewsManagementContext();
        context.Categories.Update(category);
        context.SaveChanges();
    }

    public void DeleteCategory(short id)
    {
        using var context = new FUNewsManagementContext();
        var category = context.Categories.FirstOrDefault(c => c.CategoryId == id);
        if (category != null)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }

    /// <summary>Kiểm tra category có đang được dùng bởi news article nào không.</summary>
    public bool IsCategoryUsed(short id)
    {
        using var context = new FUNewsManagementContext();
        return context.NewsArticles.Any(n => n.CategoryId == id);
    }
}
