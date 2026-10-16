using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<Category> GetCategories() => _categoryRepository.GetCategories();

    public List<Category> SearchCategories(string? keyword)
    {
        var categories = _categoryRepository.GetCategories();
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return categories;
        }

        keyword = keyword.Trim().ToLower();
        return categories
            .Where(c => c.CategoryName.ToLower().Contains(keyword)
                     || c.CategoryDesciption.ToLower().Contains(keyword))
            .ToList();
    }

    public Category? GetCategoryById(short id) => _categoryRepository.GetCategoryById(id);

    public void SaveCategory(Category category) => _categoryRepository.SaveCategory(category);

    public void UpdateCategory(Category category) => _categoryRepository.UpdateCategory(category);

    public bool DeleteCategory(short id, out string message)
    {
        if (_categoryRepository.IsCategoryUsed(id))
        {
            message = "This category is being used by one or more news articles, so it cannot be deleted.";
            return false;
        }

        _categoryRepository.DeleteCategory(id);
        message = "Category deleted successfully.";
        return true;
    }
}
