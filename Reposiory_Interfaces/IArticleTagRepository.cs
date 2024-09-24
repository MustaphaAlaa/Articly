using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiory_Interfaces
{
    public interface IArticleTagRepository
    {

        public Task<ArticleTag> AddAsync(ArticleTag article);

        public Task<int> UpdateAsync(Article article);

        public Task<bool> DeleteAsync(int Id);

        public Task<ArticleTag?> GetByIdAsync(int Id);

        public Task<List<ArticleTag>> GetAllAsync();

        public Task<List<ArticleTag>> GetAllTagsInArticle(int ArticleId);
        public Task<List<ArticleTag>> GetAllArticlesInTag(int TagId);

        public Task DeleteAllArticleTags(int ArticleId);

    }
}
