using Entities.ViewsModel.Tags;
using Entities.Domain;
using Entities.ViewsModel;
using Entities.ViewsModel.Tags;

namespace ServicesInterfaces;

public interface ITag
{
    public Task<TagResponse?> AddTagAsync(AddTagRequest tag);

    public Task<bool> DeleteTag(int id);

    public Task<TagResponse?> UpdateTag(UpdateTagRequest tag);

    public Task<TagResponse?> GetTagById(int id);

    public Task<TagResponse?> GetTagByName(string name);

    public Task<List<TagResponse>> GetAll();
}

