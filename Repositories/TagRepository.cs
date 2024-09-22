using Repository_Interfaces;
using Entities.Domain;
using Entities;
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

    public async Task<bool> DeleteAsync(int Id)
    {

        Tag? tag = await this.GetByIdAsync(Id);

        if (tag == null)
            return false;

        _db.Remove(tag);
        int deleted = await _db.SaveChangesAsync();
        return deleted > 0;
    }

    public async Task<Tag?> GetByIdAsync(int Id)
    {
        return await _db.Tags.FirstOrDefaultAsync(T => T.TagId == Id);
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _db.Tags.Select(t => t).ToListAsync();
    }
}
