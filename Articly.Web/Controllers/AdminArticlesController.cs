using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
using Microsoft.AspNetCore.Mvc;
using ServicesInterfaces;

namespace Articly.Web.Controllers
{
    public class AdminArticlesController : Controller
    {
        private readonly IArticle _ArticleServices;
        private readonly ILogger<AdminArticlesController> _logger;
        private readonly ITag _tag;

        public AdminArticlesController(IArticle articleServices, ILogger<AdminArticlesController> logger, ITag tag)
        {
            _ArticleServices = articleServices;
            _logger = logger;
            _tag = tag;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Reached To Index() In {this.GetType().Name}");

            var Blogs = await _ArticleServices.GetAllAsync();
            return View(Blogs);
        }

        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            _logger.LogInformation($"Reached To AddBlog() In {this.GetType().Name}");


            AddArticleRequest addArticleRequest = new AddArticleRequest();
            List<TagResponse> tagResponses = await _tag.GetAll();
            addArticleRequest.Tags = tagResponses.Select(t => t.ToTag());

            return View(addArticleRequest);
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(AddArticleRequest article)
        {
            _logger.LogInformation($"Reached To AddArticle() In {this.GetType().Name}");



            var ArticleResponses = await _ArticleServices.AddArticleAsync(article);


            return RedirectToAction("Index");
         }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(Guid BlogId)
        {
            _logger.LogInformation($"Reached To (HttpGet)UpdaetBlog() In {this.GetType().Name}");

            var GetBlog = await _ArticleServices.GetArticleAsync(BlogId);
            if (GetBlog != null)
                return View(GetBlog);
            else
                return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTheBlog(UpdateArticleRequest article)
        {
            _logger.LogInformation($"Reached To (HttpPost)UpdateArticle() In {this.GetType().Name}");

            await _ArticleServices.UpdateArticleAsync(article);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBlog(Guid BlogId)
        {
            _logger.LogInformation($"Reached To (HttpGet)DeleteArticle() In {this.GetType().Name}");

            var BlogResponse = await _ArticleServices.GetArticleAsync(BlogId);
            if (BlogResponse != null)
                return View(BlogResponse);
            else
                return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTheArticle(Guid ArticleId)
        {
            _logger.LogInformation($"Reached To (HttpPost)DeleteArticle() In {this.GetType().Name}");

            await _ArticleServices.DeleteArticleAsync(ArticleId);

            return RedirectToAction("Index");
        }
    }
}
