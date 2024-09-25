using Entities.ViewsModel.Tags;
using Entities.Domain;
using Entities.ViewsModel;
using Entities.ViewsModel.Tags;

namespace ServicesInterfaces;

public interface ITag
{
    public Task<Tag?> AddTagAsync(AddTagRequest tag);

    public Task<bool> DeleteTag(int id);

    public Task<Tag?> UpdateTag(UpdateTagRequest tag);

    public Task<Tag?> GetTagById(int id);

    public Task<Tag?> GetTagByName(string name);

    public Task<List<Tag>> GetAll();
}

