

using Entities.Domain;
using Entities.ViewsModel.Tags;
using Microsoft.AspNetCore.Mvc;
using Articly.Entites.ViewsModel.Tags;
using ServicesInterfaces;

namespace Articly.Web.Controllers;

public class TagsController : Controller
{
    private readonly ITag _TagServices;
    private readonly ILogger<TagsController> _Logger;
    private readonly IArticleTag _articleTagServices;
    public TagsController(IArticleTag articleTag, ITag tagServices, ILogger<TagsController> logger)
    {
        _articleTagServices = articleTag;
        _Logger = logger;
        _TagServices = tagServices;
    }

    public async Task<IActionResult> Index()
    {
        _Logger.LogInformation($"Reached To Index() In {this.GetType().Name}");

        var tags = await _TagServices.GetAll();
        return View(tags);
    }

    [HttpGet]
    public IActionResult AddTag()
    {
        _Logger.LogInformation($"Reached To AddTag() In {this.GetType().Name}");


        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTag(AddTagRequest tag)
    {
        _Logger.LogInformation($"Reached To AddTag() In {this.GetType().Name}");

        var tagResponse = await _TagServices.AddTagAsync(tag);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTag(int id)
    {
        _Logger.LogInformation($"Reached To (HttpGet)UpdaetTag() In {this.GetType().Name}");

        var GetTag = await _TagServices.GetTagById(id);
        if (GetTag != null)
            return View(GetTag);
        else
            return RedirectToAction("index");
    }
    [HttpPost]
    public async Task<IActionResult> UpdateTheTag(UpdateTagRequest tag)
    {
        _Logger.LogInformation($"Reached To (HttpPost)UpdateTag() In {this.GetType().Name}");

        await _TagServices.UpdateTag(tag);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteTag(int TagId)
    {
        _Logger.LogInformation($"Reached To (HttpGet)DeletetTag() In {this.GetType().Name}");

        var tagResponse = await _TagServices.GetTagById(TagId);
        if (tagResponse != null)
            return View(tagResponse);
        else
            return RedirectToAction("index");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteTheTag(int TagId)
    {
        _Logger.LogInformation($"Reached To (HttpPost)DeleteTag() In {this.GetType().Name}");

        await _TagServices.DeleteTag(TagId);

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> GetTag(int id)
    {
        _Logger.LogInformation($"Reached To GetTag(int id) In {this.GetType().Name}");

        if (id <= 0)
        {
            _Logger.LogError($"Invalid Id, id: {id}");
            return RedirectToAction("Index");
        }
        var tag = await _TagServices.GetTagById(id);

        if (tag == null)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return RedirectToAction("index");
        }


        var tagResponse = tag.ToResponse();

        tagResponse.Articles = await _articleTagServices.GetArticlesOnTag(tagResponse.TagId);

        return View(tagResponse);
    }

}









