using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _newsArticleRepository;

    public NewsArticleService(INewsArticleRepository newsArticleRepository)
    {
        _newsArticleRepository = newsArticleRepository;
    }

    public List<NewsArticle> GetNewsArticles() => _newsArticleRepository.GetNewsArticles();

    public List<NewsArticle> GetActiveNewsArticles() => _newsArticleRepository.GetActiveNewsArticles();

    public List<NewsArticle> SearchNewsArticles(string? keyword, short? categoryId, bool activeOnly)
    {
        var news = activeOnly
            ? _newsArticleRepository.GetActiveNewsArticles()
            : _newsArticleRepository.GetNewsArticles();

        // LINQ query + sort
        var query = news.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim().ToLower();
            query = query.Where(n =>
                (n.NewsTitle ?? string.Empty).ToLower().Contains(keyword) ||
                (n.Headline ?? string.Empty).ToLower().Contains(keyword) ||
                (n.NewsContent ?? string.Empty).ToLower().Contains(keyword));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(n => n.CategoryId == categoryId.Value);
        }

        return query
            .OrderByDescending(n => n.CreatedDate)
            .ThenByDescending(n => n.NewsArticleId)
            .ToList();
    }

    public List<NewsArticle> GetNewsArticlesByCreator(short accountId)
        => _newsArticleRepository.GetNewsArticlesByCreator(accountId);

    public NewsArticle? GetNewsArticleById(string id) => _newsArticleRepository.GetNewsArticleById(id);

    public string GetNextNewsArticleId() => _newsArticleRepository.GetNextNewsArticleId();

    public void SaveNewsArticle(NewsArticle news, short createdById)
    {
        news.CreatedDate = DateTime.Now;
        news.ModifiedDate = DateTime.Now;
        news.CreatedById = createdById;
        news.UpdatedById = createdById;
        news.NewsStatus ??= true;
        _newsArticleRepository.SaveNewsArticle(news);
    }

    public void UpdateNewsArticle(NewsArticle news, short updatedById)
    {
        news.ModifiedDate = DateTime.Now;
        news.UpdatedById = updatedById;
        _newsArticleRepository.UpdateNewsArticle(news);
    }

    public void DeleteNewsArticle(string id) => _newsArticleRepository.DeleteNewsArticle(id);

    public List<NewsArticle> GetReport(DateTime startDate, DateTime endDate)
    {
        var end = endDate.Date.AddDays(1).AddTicks(-1);

        return _newsArticleRepository.GetNewsArticles()
            .Where(n => n.CreatedDate.HasValue
                     && n.CreatedDate.Value >= startDate.Date
                     && n.CreatedDate.Value <= end)
            .OrderByDescending(n => n.CreatedDate)
            .ToList();
    }
}
