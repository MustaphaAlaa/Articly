using Repository_Interfaces;
using Entities.Domain;
using  DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class TagRepository : ITagRepository
{

    private readonly ArticleDbContext _db;

    public TagRepository(ArticleDbContext db)
    {
        _db = db;
    }

    public async Task<Tag> AddAsync(Tag tag)
    {
        await _db.Tags.AddAsync(tag);
        await _db.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> UpdateAsync(Tag tag)
    {



        _db.Tags.Update(tag);
        await _db.SaveChangesAsync();
        return tag;
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {

        Tag? tag = await this.GetByIdAsync(Id);

        if (tag == null)
            return false;

        _db.Remove(tag);
        int deleted = await _db.SaveChangesAsync();
        return deleted > 0;
    }

    public async Task<Tag?> GetByIdAsync(Guid Id)
    {
        return await _db.Tags.FirstOrDefaultAsync(T => T.TagID == Id);
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _db.Tags.Select(t => t).ToListAsync();
    }
}
