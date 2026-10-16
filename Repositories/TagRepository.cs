using BusinessObjects.Entities;
using DataAccessObjects;

namespace Repositories;

public class TagRepository : ITagRepository
{
    private readonly TagDAO _tagDAO = new();

    public List<Tag> GetTags() => _tagDAO.GetTags();

    public Tag? GetTagById(int id) => _tagDAO.GetTagById(id);

    public void SaveTag(Tag tag) => _tagDAO.SaveTag(tag);

    public void UpdateTag(Tag tag) => _tagDAO.UpdateTag(tag);

    public void DeleteTag(int id) => _tagDAO.DeleteTag(id);
}
