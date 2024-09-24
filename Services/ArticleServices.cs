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
using Articly.Entites.ViewsModel.Articles;
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



            Article article = articleRequest.ToArticle();
            article.PublishDate = DateTime.Now;

            await _articleRepository.AddAsync(article);


            return article?.ToResponse();
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            _logger.LogInformation($"Reached To DeleteBlogAsync() In {this.GetType().Name}");
            if (id <= 0)
                return false;

            if (this.GetArticleAsync(id).Result == null)
                return false;

            return await _articleRepository.DeleteAsync(id);
        }

        public async Task<ArticleResponse?> GetArticleAsync(int id)
        {
            _logger.LogInformation($"Reached To GetArticleById() In {this.GetType().Name}");
            var article = await _articleRepository.GetByIdAsync(id);

            if (article == null)
                return null;

            return article.ToResponse();
        }

        public async Task<ArticleResponse?> EditArticleAsync(UpdateArticleRequest article)
        {
            Article? UpdatedArticle = new();
            try
            {
                if (article == null)
                    throw new ArgumentNullException(nameof(article));

                UpdatedArticle = await _articleRepository.GetByIdAsync(article.ArticleId);

                //var Found = _articleRepository.GetAllAsync().Result.Exists(a => a.ArticleId == article.ArticleId);

                if (UpdatedArticle == null)
                    //if (!Found)
                    throw new Exception("Can't Find This ID");

                ModelValidate.ModelValidation(article);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }



            //UpdatedArticle.ShortDescription = article.ShortDescription;
            //UpdatedArticle.Contnet = article.Contnet;
            //UpdatedArticle.FeaturedImaageUrl = article.FeaturedImaageUrl;
            //UpdatedArticle.Heading = article.Heading;
            //UpdatedArticle.PageTitle = article.PageTitle;
            //UpdatedArticle.Visible = article.Visible;
            UpdatedArticle.ArticleId = article.ArticleId;
            UpdatedArticle.ShortDescription = article.ShortDescription;
            UpdatedArticle.Contnet = article.Contnet;
            UpdatedArticle.FeaturedImaageUrl = article.FeaturedImaageUrl;
            UpdatedArticle.Heading = article.Heading;
            UpdatedArticle.PageTitle = article.PageTitle;
            UpdatedArticle.Visible = article.Visible;
            UpdatedArticle.Tags = article.Tags;


            int Updated = await _articleRepository.EditAsync(UpdatedArticle);

            if (Updated > 0)
                return UpdatedArticle.ToResponse();
            else
                return null;
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
