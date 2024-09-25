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

namespace Articly_Services
{
    public class ArticleTagServices : IArticleTag
    {

        private readonly IArticleTagRepository _articleTagRepository;
        private readonly ILogger<ArticleTagServices> _logger;
        //private readonly ITag _tag;

        public ArticleTagServices(ILogger<ArticleTagServices> logger, IArticleTagRepository articleTagRepository)
        {

            _logger = logger;
            _articleTagRepository = articleTagRepository;
        }


        public async Task<int> Add(ArticleTag articleTag)
        {
            try
            {
                if (articleTag == null)
                    throw new ArgumentNullException();

                if (articleTag.TagId <= 0 || articleTag.ArticleId <= 0)
                    throw new ArgumentException();

                if (_articleTagRepository.IsRelated(articleTag.ArticleId, articleTag.TagId).Result)
                    throw new Exception("Dublicated: This Relation Between Article And Tag Is Already Exist;");

            }
            catch
            {
                _logger.LogError($"Something Get Wrong");

                return -1;
            }


            var added = await _articleTagRepository.Add(articleTag.ArticleId, articleTag.TagId);


            return added;
        }

        public async Task<List<Article>> GetArticlesOnTag(int TagId)
        {
            return await _articleTagRepository.GetArticlesOnTags(TagId);
        }

        public async Task<List<Tag>> GetTagsOnArticle(int ArticleId)
        {
            return await _articleTagRepository.GetTagsOnArticle(ArticleId);
        }

        public async Task<bool> IsRelated(int ArticleId, int TagId)
        {
            return await _articleTagRepository.IsRelated(ArticleId, TagId);
        }

        public async Task<bool> Delete(int ArticleId, int TagId)
        {
            return await _articleTagRepository.Delete(ArticleId, TagId);
        }
    }
}
