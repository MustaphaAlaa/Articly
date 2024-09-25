
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Repository_Interfaces;
namespace Repositories
{
    public class ArticleTagRepository : IArticleTagRepository
    {

        private readonly ArticleDbContext _db;

        public ArticleTagRepository(ArticleDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsRelated(int ArticleId, int TagId)
        {
            var Found = await _db.ArticleTags.FirstOrDefaultAsync(at => at.TagId == TagId && at.ArticleId == ArticleId);

            return Found != null;

        }
        public async Task<ArticleTag?> GetRelation(int ArticleId, int TagId)
        {
            return await _db.ArticleTags.FirstOrDefaultAsync(at => at.TagId == TagId && at.ArticleId == ArticleId);


        }
        public async Task<int> Add(int ArticleId, int TagId)
        {
            ArticleTag articleTag = new() { ArticleId = ArticleId, TagId = TagId };
            await _db.ArticleTags.AddAsync(articleTag);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Remove(int ArticleId, int TagId)
        {
            ArticleTag? articleTag = await GetRelation(ArticleId, TagId);

            if (articleTag != null)
            {
                _db.ArticleTags.Remove(articleTag);
                return await _db.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<List<Tag>> GetTagsOnArticle(int ArticleId)
        {
            var articletags = _db.ArticleTags.Where(at => at.ArticleId == ArticleId).ToList();
            List<Tag> tags = new List<Tag>();

            foreach (var tag in articletags)
            {

                Tag? articleTag = await _db.Tags.FirstOrDefaultAsync(t => t.TagId == tag.TagId);

                if (articleTag != null)
                    tags.Add(articleTag);
            }


            return tags;
        }

        public async Task<List<Article>> GetArticlesOnTags(int TagId)
        {
            var TagArticles = _db.ArticleTags.Where(at => at.TagId == TagId).ToList();

            List<Article> Articles = new List<Article>();

            foreach (var article in TagArticles)
            {

                Article? tagArticle = await _db.Articles.FirstOrDefaultAsync(a => a.ArticleId == article.ArticleId);

                if (tagArticle != null)
                    Articles.Add(tagArticle);
            }


            return Articles;
        }

        public async Task<bool> Delete(int ArticleId, int TagId)
        {
            var deleted = await _db.ArticleTags.Where(at => at.ArticleId == ArticleId && at.TagId == TagId).ExecuteDeleteAsync();
            return deleted > 0;
        }
    }
}
