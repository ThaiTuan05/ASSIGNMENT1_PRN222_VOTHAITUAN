using BusinessObjects.Entities;

namespace Repositories;

public interface INewsArticleRepository
{
    List<NewsArticle> GetNewsArticles();
    List<NewsArticle> GetActiveNewsArticles();
    List<NewsArticle> GetNewsArticlesByCreator(short accountId);
    NewsArticle? GetNewsArticleById(string id);
    string GetNextNewsArticleId();
    void SaveNewsArticle(NewsArticle news);
    void UpdateNewsArticle(NewsArticle news);
    void DeleteNewsArticle(string id);
}
