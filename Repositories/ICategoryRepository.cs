using BusinessObjects.Entities;

namespace Repositories;

public interface ICategoryRepository
{
    List<Category> GetCategories();
    Category? GetCategoryById(short id);
    void SaveCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(short id);
    bool IsCategoryUsed(short id);
}
