using BusinessObjects.Entities;
using DataAccessObjects;

namespace Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoryDAO _categoryDAO = new();

    public List<Category> GetCategories() => _categoryDAO.GetCategories();

    public Category? GetCategoryById(short id) => _categoryDAO.GetCategoryById(id);

    public void SaveCategory(Category category) => _categoryDAO.SaveCategory(category);

    public void UpdateCategory(Category category) => _categoryDAO.UpdateCategory(category);

    public void DeleteCategory(short id) => _categoryDAO.DeleteCategory(id);

    public bool IsCategoryUsed(short id) => _categoryDAO.IsCategoryUsed(id);
}
