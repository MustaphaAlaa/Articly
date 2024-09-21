using Bloggie.Services.Helper;
using Entities.Domain;
using Entities.ViewsModel.Articles;
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
    public class ArticleServices : IArticle
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ArticleServices> _logger;
        private readonly ITag _tag;

        public ArticleServices(ITag tag, IArticleRepository BlogRepository, ILogger<ArticleServices> logger)
        {
            _articleRepository = BlogRepository;
            _logger = logger;
            _tag = tag;
        }

        public async Task<ArticleResponse?> AddArticleAsync(AddArticleRequest articleRequest)
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");
            try
            {
                if (articleRequest == null)
                    throw new ArgumentNullException(nameof(articleRequest));



                ModelValidate.ModelValidation(articleRequest);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }

            List<Tag> Tags = new();

            foreach (string tagID in articleRequest.SelectedTags)
            {
                var t = await _tag.GetTagById(Guid.Parse(tagID));

                Tags.Add(t.ToTag());
            }

            Article article = articleRequest.ToArticle();
            //Blog.
            article.Tags = Tags;
            article.PublishDate = DateTime.Now;

            article.ArticleID = Guid.NewGuid();

            await _articleRepository.AddAsync(article);


            return article.ToResponse();
        }

        public async Task<bool> DeleteArticleAsync(Guid id)
        {
            _logger.LogInformation($"Reached To DeleteBlogAsync() In {this.GetType().Name}");
            if (id == Guid.Empty)
                return false;

            if (this.GetArticleAsync(id).Result == null)
                return false;

            return await _articleRepository.DeleteAsync(id);
        }

        public async Task<ArticleResponse?> GetArticleAsync(Guid id)
        {
            _logger.LogInformation($"Reached To GetBlogById() In {this.GetType().Name}");
            var Blog = await _articleRepository.GetByIdAsync(id);
            return Blog.ToResponse();
        }


        public async Task<ArticleResponse?> UpdateArticleAsync(UpdateArticleRequest article)
        {
            Article? UpdatedArticle;
            try
            {
                if (article == null)
                    throw new ArgumentNullException(nameof(article));

                UpdatedArticle = await _articleRepository.GetByIdAsync(article.ArticleID);

                if (UpdatedArticle == null)
                    throw new Exception("Can't Find This ID");

                ModelValidate.ModelValidation(article);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }



            Article? ArticleIsUpdated = await _articleRepository.UpdateAsync(UpdatedArticle);

            return ArticleIsUpdated?.ToResponse();
        }
        public async Task<List<ArticleResponse>> GetAllAsync()
        {
            _logger.LogInformation("Reached To GetAll In BlogServices");

            var ArticleResponseList = await _articleRepository.GetAllAsync();

            return ArticleResponseList
                .ToList()
                .Select(article => article.ToResponse())
                .ToList();
        }


    }
}
