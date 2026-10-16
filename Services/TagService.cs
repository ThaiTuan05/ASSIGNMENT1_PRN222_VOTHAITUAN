using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public List<Tag> GetTags() => _tagRepository.GetTags();

    public Tag? GetTagById(int id) => _tagRepository.GetTagById(id);

    public void SaveTag(Tag tag) => _tagRepository.SaveTag(tag);

    public void UpdateTag(Tag tag) => _tagRepository.UpdateTag(tag);

    public void DeleteTag(int id) => _tagRepository.DeleteTag(id);
}
