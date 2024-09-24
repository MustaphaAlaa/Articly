using Entities;
using Entities.Domain;
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

        public async Task<int> UpdateAsync(Article article)
        {
            if (article == null)
            {
                return 0;
            }


            List<int> articleTags = (await this.GetAllTagsInArticle(article.ArticleId))
                                     .Select(t => t.TagID)
                                     .ToList();



            List<int> cloned = articleTags.Select(s => s).ToList();




            List<int> tagsToUpdate = article.Tags
                                        .Select(t => t.TagId)
                                        .ToList();



            foreach (var TId in cloned)
            {
                //if tags in db not inside tagsUpdate delete it
                if (!tagsToUpdate.Contains(TId))
                {
                    await this.DeleteAsyncWhere(a =>
                                   a.ArticleID == article.ArticleId && a.TagID == TId);

                    articleTags.Remove(TId);
                }

            }



            foreach (var t in tagsToUpdate)
            {
                if (articleTags.Contains(t))
                    continue;

                await this.AddAsync(new ArticleTag()
                {
                    ArticleID = article.ArticleId,
                    Article = article,

                });
            }
            //int updated = await _db.SaveChangesAsync();
            return 1;
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

        public async Task<bool> DeleteAsyncWhere(Func<ArticleTag, bool> predicate)
        {

            ArticleTag? article = _db.ArticleTag.FirstOrDefault(predicate);

            if (article == null)
                return false;

            _db.ArticleTag.Remove(article);
            int deleted = await _db.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ArticleTag?> GetByIdAsync(int Id)
        {
            return await _db.ArticleTag.Include(at => at.Article).Include(at => at.Tag).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<ArticleTag>> GetAllAsync()
        {
            return await _db.ArticleTag.Select(a => a).ToListAsync();
        }

        public Task<List<ArticleTag>> GetAllTagsInArticle(int ArticleId)
        {
            return _db.ArticleTag.Include(at => at.Article).Include(at => at.Tag).Where(artT => artT.ArticleID == ArticleId).ToListAsync();

        }

        public Task<List<ArticleTag>> GetAllArticlesInTag(int TagId)
        {
            return _db.ArticleTag.Include(at => at.Article).Include(at => at.Tag).Where(artT => artT.TagID == TagId).ToListAsync();

        }



        public Task<int> DeleteAllTagsFromArticle(int ArticleId)
        {
            return Task.FromResult(_db.ArticleTag.Where(ArticleTag => ArticleTag.ArticleID == ArticleId).ExecuteDelete());
        }

        public Task<int> DeleteAllArticleFromTag(int TagId)
        {
            return Task.FromResult(_db.ArticleTag.Where(ArticleTag => ArticleTag.TagID == TagId).ExecuteDelete());
        }

        public Task DeleteAllArticleTags(int ArticleId)
        {
            throw new NotImplementedException();
        }
    }
}
