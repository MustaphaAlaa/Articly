

using Entities.Domain;
using Entities.ViewsModel.Tags;
using Microsoft.AspNetCore.Mvc;

using ServicesInterfaces;

namespace Articly.Web.Controllers;

public class AdminTagsController : Controller
{
    private readonly ITag _TagServices;
    private readonly ILogger<AdminTagsController> _Logger;
    public AdminTagsController(ITag tagServices, ILogger<AdminTagsController> logger)
    {
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
    public async Task<IActionResult> UpdateTag(Guid TagId)
    {
        _Logger.LogInformation($"Reached To (HttpGet)UpdaetTag() In {this.GetType().Name}");

        var GetTag = await _TagServices.GetTagById(TagId);
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
    public async Task<IActionResult> DeleteTag(Guid TagId)
    {
        _Logger.LogInformation($"Reached To (HttpGet)DeletetTag() In {this.GetType().Name}");

        var tagResponse = await _TagServices.GetTagById(TagId);
        if (tagResponse != null)
            return View(tagResponse);
        else
            return RedirectToAction("index");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteTheTag(Guid TagId)
    {
        _Logger.LogInformation($"Reached To (HttpPost)DeleteTag() In {this.GetType().Name}");

        await _TagServices.DeleteTag(TagId);

        return RedirectToAction("Index");
    }

}