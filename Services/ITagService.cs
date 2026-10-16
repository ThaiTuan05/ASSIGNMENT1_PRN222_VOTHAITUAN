using BusinessObjects.Entities;

namespace Services;

public interface ITagService
{
    List<Tag> GetTags();
    Tag? GetTagById(int id);
    void SaveTag(Tag tag);
    void UpdateTag(Tag tag);
    void DeleteTag(int id);
}
