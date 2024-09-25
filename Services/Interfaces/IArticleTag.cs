using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
using Microsoft.Extensions.Logging;
using Repository_Interfaces;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces
{
    public interface IArticleTag
    {

        public Task<int> Add(ArticleTag articleTag);

        public Task<List<Tag>> GetTagsOnArticle(int ArticleId);
        public Task<List<Article>> GetArticlesOnTag(int TagId);

        public Task<bool> IsRelated(int ArticleId, int TagId);

        public Task<bool> Delete(int ArticleId, int TagId);


    }
}
