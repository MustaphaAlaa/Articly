﻿using System.Reflection;
using System.Collections.Concurrent;
using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
namespace Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleDbContext _db;

        public ArticleRepository(ArticleDbContext db)
        {
            _db = db;
        }

        public async Task<Article> AddAsync(Article article)
        {
            await _db.Articles.AddAsync(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            _db.Articles.Update(article);
            await _db.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteAsync(int Id)
        {

            Article? article = await this.GetByIdAsync(Id);

            if (article == null)
                return false;

            _db.Articles.Remove(article);
            int deleted = await _db.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Article?> GetByIdAsync(int Id)
        {
            return await _db.Articles.Include(art=>art.Tags).FirstOrDefaultAsync(a => a.ArticleId == Id);
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _db.Articles.Include(Article=>Article.Tags).Select(a => a).ToListAsync();
        }
    }
}
