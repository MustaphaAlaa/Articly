using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
using Microsoft.AspNetCore.Mvc;
using Reposiory_Interfaces;
using Repository_Interfaces;
using ServicesInterfaces;

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

            var Blogs = await _ArticleServices.GetAllAsync();
            return View(Blogs);
        }

        [HttpGet]
        public async Task<IActionResult> AddArticle()
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");


            AddArticleRequest addArticleRequest = new AddArticleRequest();

            List<TagResponse> tagResponses = await _tag.GetAll();

            ViewBag.Tags = tagResponses;
            //addArticleRequest.Tags = tagResponses.Select(t => t.ToTag()).ToList();
            ViewBag.TagsOnArticle = new List<string>();

            addArticleRequest.SelectedTags = new List<string>(tagResponses.Count);

            //addArticleRequest.Tags = tagResponses.Select(t => t.ToTag());

            return View(addArticleRequest);
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleRequest article)
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");



            var ArticleResponses = await _ArticleServices.AddArticleAsync(article);

            await _articleTag.AddFromAddArticleRequest(article, ArticleResponses.ArticleID);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int ArticleId)
        {
            _logger.LogInformation($"Reached To (HttpGet)UpdaetArticle() In {this.GetType().Name}");

            var GetBlog = await _ArticleServices.GetArticleAsync(ArticleId);
            if (GetBlog != null)
                return View(GetBlog);
            else
                return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTheArticle(UpdateArticleRequest article)
        {
            _logger.LogInformation($"Reached To (HttpPost)UpdateArticle() In {this.GetType().Name}");

            await _ArticleServices.UpdateArticleAsync(article);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int ArticleId)
        {
            _logger.LogInformation($"Reached To (HttpGet)DeleteArticle() In {this.GetType().Name}");

            var ArticleResponse = await _ArticleServices.GetArticleAsync(ArticleId);
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
    }
}
