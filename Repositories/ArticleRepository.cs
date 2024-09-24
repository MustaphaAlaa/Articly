using System.Reflection;
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

        public async Task<int> EditAsync(Article article)
        {

            var ArticleToUpdate = await _db.Articles.FirstOrDefaultAsync(art => art.ArticleId == article.ArticleId);
            //.ExecuteUpdate(setters => setters
            //.SetProperty(a => a.PageTitle, article.PageTitle)
            //.SetProperty(a => a.Heading, article.Heading)
            //.SetProperty(a => a.Contnet, article.Contnet)
            //.SetProperty(a => a.FeaturedImaageUrl, article.FeaturedImaageUrl)
            //.SetProperty(a => a.Visible, article.Visible));


            //.SetProperty(a => a.Tags, article.Tags));
            //var ArticleToUpdate = await this.GetByIdAsync(article.ArticleId);

            if (ArticleToUpdate != null)
            {

                ArticleToUpdate.ShortDescription = article.ShortDescription;
                ArticleToUpdate.Contnet = article.Contnet;
                ArticleToUpdate.FeaturedImaageUrl = article.FeaturedImaageUrl;
                ArticleToUpdate.Heading = article.Heading;
                ArticleToUpdate.PageTitle = article.PageTitle;
                ArticleToUpdate.Visible = article.Visible;


                _db.Articles.Update(ArticleToUpdate);
                return await _db.SaveChangesAsync();
            }
            return 0;
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
            return await _db.Articles.Include(art => art.Tags).FirstOrDefaultAsync(a => a.ArticleId == Id);
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _db.Articles.Include(Article => Article.Tags).Select(a => a).ToListAsync();
        }
    }
}
