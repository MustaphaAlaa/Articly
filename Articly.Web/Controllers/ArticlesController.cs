using System.Security.Cryptography.X509Certificates;
using Articly.Entites.ViewsModel.Articles;
using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
using Microsoft.AspNetCore.Mvc;
using Repository_Interfaces;
using ServicesInterfaces;
using Articly.Entites.ViewsModel.Tags;
namespace Articly.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticle _ArticleServices;
        private readonly ILogger<ArticlesController> _logger;
        private readonly ITag _tag;
        private readonly IArticleTag _articleTag;


        public ArticlesController(IArticleTag articleTag, IArticle articleServices, ILogger<ArticlesController> logger, ITag tag)
        {

            _ArticleServices = articleServices;
            _logger = logger;
            _tag = tag;
            _articleTag = articleTag;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Reached To Index() In {this.GetType().Name}");

            var Articles = await _ArticleServices.GetAllAsync();



            foreach (var article in Articles)

                article.Tags = await _articleTag.GetTagsOnArticle(article.ArticleId);


            return View(Articles);
        }

        [HttpGet]
        public async Task<IActionResult> AddArticle()
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");


            AddArticleRequest addArticleRequest = new AddArticleRequest();

            List<Tag> tagResponses = await _tag.GetAll();


            ViewBag.Tags = tagResponses;

            ViewBag.TagsOnArticle = new List<string>();



            addArticleRequest.SelectedTags = new List<string>(tagResponses.Count);



            return View(addArticleRequest);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleRequest article)
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");



            var articleResponse = await _ArticleServices.AddArticleAsync(article);

            List<Tag> tags = new List<Tag>();
            if (article.SelectedTags != null || article.SelectedTags.Count > 0)
            {

                foreach (var TagId in article.SelectedTags)
                {
                    var tag = await _tag.GetTagById(int.Parse(TagId));
                    tags.Add(tag);
                }

                foreach (var tag in tags)
                {
                    await _articleTag.Add(new ArticleTag()
                    {
                        ArticleId = articleResponse.ArticleId,
                        TagId = tag.TagId
                    });


                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            _logger.LogInformation($"Reached To (HttpGet)UpdaetArticle() In {this.GetType().Name}");

            var GetArticle = await _ArticleServices.GetArticleAsync(id);

            if (GetArticle != null)
                GetArticle.Tags = await _articleTag.GetTagsOnArticle(GetArticle.ArticleId);
            else
                return RedirectToAction("Index");

            ViewBag.Tags = await _tag.GetAll();

            if (GetArticle != null)
                return View(GetArticle.ToUpdateArticleRequest());
            else
                return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTheArticle(UpdateArticleRequest article)
        {
            _logger.LogInformation($"Reached To (HttpPost)UpdateArticle() In {this.GetType().Name}");

            List<Tag> tags = new List<Tag>();


            foreach (var t in article.SelectedTags)
            {
                var tag = await _tag.GetTagById(int.Parse(t));

                if (tag != null)
                    tags.Add(tag);
            }



            var UpdatedArtilce = await _ArticleServices.EditArticleAsync(article);


            if (UpdatedArtilce == null)
            {
                return RedirectToAction("EditArticle", new { id = article.ArticleId });
            }


            var r = UpdatedArtilce.ToArticle();

            int TagsCountBeforeUpdate = (_articleTag.GetTagsOnArticle(UpdatedArtilce.ArticleId).Result.Count);


            foreach (var tag in tags)
            {
                bool related = await _articleTag.IsRelated(UpdatedArtilce.ArticleId, tag.TagId);

                if (!related)
                    await _articleTag.Add(new ArticleTag() { ArticleId = UpdatedArtilce.ArticleId, TagId = tag.TagId });
            }

            var articleTags = await _articleTag.GetTagsOnArticle(UpdatedArtilce.ArticleId);


            foreach (var tag in articleTags)
            {
                if (tags.FirstOrDefault(t => t.TagId == tag.TagId) == null)
                    await _articleTag.Delete(UpdatedArtilce.ArticleId, tag.TagId);
            }

            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int Id)
        {
            _logger.LogInformation($"Reached To (HttpGet)DeleteArticle() In {this.GetType().Name}");

            var ArticleResponse = await _ArticleServices.GetArticleAsync(Id);

            if (ArticleResponse != null)
                return View(ArticleResponse);
            else
                return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTheArticle(int ArticleId)
        {
            _logger.LogInformation($"Reached To (HttpPost)DeleteArticle() In {this.GetType().Name}");

            await _ArticleServices.DeleteArticleAsync(ArticleId);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> GetArticle(int id)
        {
            ArticleResponse? article = await _ArticleServices.GetArticleAsync(id);
            article.Tags = await _articleTag.GetTagsOnArticle(article.ArticleId);


            return View(article);
        }

    }
}
