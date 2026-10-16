using BusinessObjects.Entities;
using DataAccessObjects;

namespace Repositories;

public class NewsArticleRepository : INewsArticleRepository
{
    private readonly NewsArticleDAO _newsArticleDAO = new();

    public List<NewsArticle> GetNewsArticles() => _newsArticleDAO.GetNewsArticles();

    public List<NewsArticle> GetActiveNewsArticles() => _newsArticleDAO.GetActiveNewsArticles();

    public List<NewsArticle> GetNewsArticlesByCreator(short accountId) => _newsArticleDAO.GetNewsArticlesByCreator(accountId);

    public NewsArticle? GetNewsArticleById(string id) => _newsArticleDAO.GetNewsArticleById(id);

    public string GetNextNewsArticleId() => _newsArticleDAO.GetNextNewsArticleId();

    public void SaveNewsArticle(NewsArticle news) => _newsArticleDAO.SaveNewsArticle(news);

    public void UpdateNewsArticle(NewsArticle news) => _newsArticleDAO.UpdateNewsArticle(news);

    public void DeleteNewsArticle(string id) => _newsArticleDAO.DeleteNewsArticle(id);
}
