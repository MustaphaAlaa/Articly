using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Interfaces
{
    public interface IArticleTagRepository
    {
        public Task<bool> IsRelated(int ArticleId, int TagId);

        public Task<ArticleTag?> GetRelation(int ArticleId, int TagId);


        public Task<int> Add(int ArticleId, int TagId);

        public Task<int> Remove(int ArticleId, int TagId);


        public Task<List<Tag>> GetTagsOnArticle(int ArticleId);

        public Task<List<Article>> GetArticlesOnTags(int TagId);


        public Task<bool> Delete(int ArticleId, int TagId);

    }
}
