using Entities.Domain;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reposiory_Interfaces;

namespace Repositories
{
    public class ArticleTagRepsitory : IArticleTagRepository
    {

        private readonly ArticleDbContext _db;

        public ArticleTagRepsitory(ArticleDbContext db)
        {
            _db = db;
        }

        public async Task<ArticleTag> AddAsync(ArticleTag articleTag)
        {
            await _db.ArticleTag.AddAsync(articleTag);
            await _db.SaveChangesAsync();
            return articleTag;
        }

        public async Task<ArticleTag> UpdateAsync(ArticleTag articleTag)
        {
            _db.ArticleTag.Update(articleTag);
            await _db.SaveChangesAsync();
            return articleTag;
        }

        public async Task<bool> DeleteAsync(int Id)
        {

            ArticleTag? article = await this.GetByIdAsync(Id);

            if (article == null)
                return false;

            _db.ArticleTag.Remove(article);
            int deleted = await _db.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ArticleTag?> GetByIdAsync(int Id)
        {
            return await _db.ArticleTag.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<ArticleTag>> GetAllAsync()
        {
            return await _db.ArticleTag.Select(a => a).ToListAsync();
        }

    }
}
