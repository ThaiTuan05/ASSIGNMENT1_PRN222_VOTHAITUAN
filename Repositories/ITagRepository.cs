using BusinessObjects.Entities;

namespace Repositories;

public interface ITagRepository
{
    List<Tag> GetTags();
    Tag? GetTagById(int id);
    void SaveTag(Tag tag);
    void UpdateTag(Tag tag);
    void DeleteTag(int id);
}
