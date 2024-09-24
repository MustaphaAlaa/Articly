using System.Security.Cryptography.X509Certificates;
using Articly.Entites.ViewsModel.Articles;
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

        private readonly IArticleTagRepository _artTagRepo;
        public ArticlesController(IArticleTagRepository ar, IArticleTag articleTag, IArticle articleServices, ILogger<ArticlesController> logger, ITag tag)
        {
            _artTagRepo = ar;
            _ArticleServices = articleServices;
            _logger = logger;
            _tag = tag;
            _articleTag = articleTag;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Reached To Index() In {this.GetType().Name}");

            var Articles = await _ArticleServices.GetAllAsync();
            return View(Articles);
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

            await _articleTag.AddFromAddArticleRequest(article, ArticleResponses.ArticleId);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            _logger.LogInformation($"Reached To (HttpGet)UpdaetArticle() In {this.GetType().Name}");

            var GetArticle = await _ArticleServices.GetArticleAsync(id);

            ViewBag.SelectedTags = new List<string>();

            List<TagResponse> tagResponses = await _tag.GetAll();

            ViewBag.Tags = tagResponses.Select(t => t.ToTag()).ToList();

            ViewBag.TagsOnArticle = new List<string>();

            foreach (var tag in GetArticle.Tags)
                ViewBag.SelectedTags.Add(tag.TagId.ToString());

            if (GetArticle != null)
                return View(GetArticle.ToUpdateArticleRequest());
            else
                return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTheArticle(UpdateArticleRequest article)
        {
            _logger.LogInformation($"Reached To (HttpPost)UpdateArticle() In {this.GetType().Name}");

            List<Tag> tag = new List<Tag>();


            foreach (var t in article.SelectedTags)
            {
                var gg = await _tag.GetTagById(int.Parse(t));
                var tt = gg.ToTag();
                tag.Add(tt);
            }


            article.Tags = tag;

            var ar = await _ArticleServices.EditArticleAsync(article);


            if (ar == null)
            {
                return RedirectToAction("EditArticle", new { id = article.ArticleId });
            }


            var r = ar.ToArticle();
            r.Tags = tag;
            var q = await _artTagRepo.UpdateAsync(r);
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


        [HttpGet]
        public async Task<IActionResult> GetArticle(int id)
        {
            ArticleResponse? vr = await _ArticleServices.GetArticleAsync(id);
            //    var at = await _artTagRepo.GetAllTagsInArticle(id);

            int o = 1;
            return View(vr);
        }

    }
}
