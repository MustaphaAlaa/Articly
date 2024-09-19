using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Domain;

namespace Repository_Interfaces;

public interface ITagRepository
{
    public Task<Tag> AddAsync(Tag tag);

    public Task<Tag> UpdateAsync(Tag tag);

    public Task<bool> DeleteAsync(Guid Id);

    public Task<Tag?> GetByIdAsync(Guid Id);

    public Task<List<Tag>> GetAllAsync();


}
