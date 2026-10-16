using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects;

/// <summary>
/// Data Access Object cho bảng NewsArticle (bao gồm cả quan hệ many-to-many với Tag qua NewsTag).
/// </summary>
public class NewsArticleDAO
{
    public List<NewsArticle> GetNewsArticles()
    {
        using var context = new FUNewsManagementContext();
        return context.NewsArticles
            .AsNoTracking()
            .Include(n => n.Category)
            .Include(n => n.CreatedBy)
            .Include(n => n.Tags)
            .OrderByDescending(n => n.CreatedDate)
            .ToList();
    }

    public List<NewsArticle> GetActiveNewsArticles()
    {
        using var context = new FUNewsManagementContext();
        return context.NewsArticles
            .AsNoTracking()
            .Where(n => n.NewsStatus == true)
            .Include(n => n.Category)
            .Include(n => n.CreatedBy)
            .Include(n => n.Tags)
            .OrderByDescending(n => n.CreatedDate)
            .ToList();
    }

    public List<NewsArticle> GetNewsArticlesByCreator(short accountId)
    {
        using var context = new FUNewsManagementContext();
        return context.NewsArticles
            .AsNoTracking()
            .Where(n => n.CreatedById == accountId)
            .Include(n => n.Category)
            .Include(n => n.Tags)
            .OrderByDescending(n => n.CreatedDate)
            .ToList();
    }

    public NewsArticle? GetNewsArticleById(string id)
    {
        using var context = new FUNewsManagementContext();
        return context.NewsArticles
            .AsNoTracking()
            .Include(n => n.Category)
            .Include(n => n.CreatedBy)
            .Include(n => n.Tags)
            .FirstOrDefault(n => n.NewsArticleId == id);
    }

    public string GetNextNewsArticleId()
    {
        using var context = new FUNewsManagementContext();
        return GenerateNextId(context);
    }

    private static string GenerateNextId(FUNewsManagementContext context)
    {
        var ids = context.NewsArticles.Select(n => n.NewsArticleId).ToList();
        var max = 0;
        foreach (var id in ids)
        {
            if (int.TryParse(id, out var value) && value > max)
            {
                max = value;
            }
        }
        return (max + 1).ToString();
    }

    public void SaveNewsArticle(NewsArticle news)
    {
        using var context = new FUNewsManagementContext();
        news.NewsArticleId = GenerateNextId(context);

        var tagIds = news.Tags.Select(t => t.TagId).ToList();
        var tags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
        news.Tags = tags;

        context.NewsArticles.Add(news);
        context.SaveChanges();
    }

    public void UpdateNewsArticle(NewsArticle news)
    {
        using var context = new FUNewsManagementContext();
        var existing = context.NewsArticles
            .Include(n => n.Tags)
            .FirstOrDefault(n => n.NewsArticleId == news.NewsArticleId);

        if (existing == null)
        {
            return;
        }

        existing.NewsTitle = news.NewsTitle;
        existing.Headline = news.Headline;
        existing.NewsContent = news.NewsContent;
        existing.NewsSource = news.NewsSource;
        existing.CategoryId = news.CategoryId;
        existing.NewsStatus = news.NewsStatus;
        existing.UpdatedById = news.UpdatedById;
        existing.ModifiedDate = news.ModifiedDate;

        var tagIds = news.Tags.Select(t => t.TagId).ToList();
        var tags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();

        existing.Tags.Clear();
        foreach (var tag in tags)
        {
            existing.Tags.Add(tag);
        }

        context.SaveChanges();
    }

    public void DeleteNewsArticle(string id)
    {
        using var context = new FUNewsManagementContext();
        var news = context.NewsArticles
            .Include(n => n.Tags)
            .FirstOrDefault(n => n.NewsArticleId == id);
        if (news != null)
        {
            news.Tags.Clear();
            context.NewsArticles.Remove(news);
            context.SaveChanges();
        }
    }
}
