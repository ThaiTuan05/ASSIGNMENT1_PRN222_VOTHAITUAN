using BusinessObjects;
using BusinessObjects.Entities;

namespace DataAccessObjects;

/// <summary>Data Access Object cho bảng Tag.</summary>
public class TagDAO
{
    public List<Tag> GetTags()
    {
        using var context = new FUNewsManagementContext();
        return context.Tags
            .OrderBy(t => t.TagId)
            .ToList();
    }

    public Tag? GetTagById(int id)
    {
        using var context = new FUNewsManagementContext();
        return context.Tags.FirstOrDefault(t => t.TagId == id);
    }

    public void SaveTag(Tag tag)
    {
        using var context = new FUNewsManagementContext();
        tag.TagId = context.Tags.Any() ? context.Tags.Max(t => t.TagId) + 1 : 1;
        context.Tags.Add(tag);
        context.SaveChanges();
    }

    public void UpdateTag(Tag tag)
    {
        using var context = new FUNewsManagementContext();
        context.Tags.Update(tag);
        context.SaveChanges();
    }

    public void DeleteTag(int id)
    {
        using var context = new FUNewsManagementContext();
        var tag = context.Tags.FirstOrDefault(t => t.TagId == id);
        if (tag != null)
        {
            context.Tags.Remove(tag);
            context.SaveChanges();
        }
    }
}
