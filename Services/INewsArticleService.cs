using BusinessObjects.Entities;

namespace Services;

public interface INewsArticleService
{
    List<NewsArticle> GetNewsArticles();
    List<NewsArticle> GetActiveNewsArticles();
    List<NewsArticle> SearchNewsArticles(string? keyword, short? categoryId, bool activeOnly);
    List<NewsArticle> GetNewsArticlesByCreator(short accountId);
    NewsArticle? GetNewsArticleById(string id);
    string GetNextNewsArticleId();
    void SaveNewsArticle(NewsArticle news, short createdById);
    void UpdateNewsArticle(NewsArticle news, short updatedById);
    void DeleteNewsArticle(string id);

    /// <summary>Thống kê news theo khoảng thời gian dựa trên CreatedDate, sắp xếp giảm dần.</summary>
    List<NewsArticle> GetReport(DateTime startDate, DateTime endDate);
}
