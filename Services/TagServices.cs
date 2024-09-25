
using Entities.Domain;
using Entities.ViewsModel;
using ServicesInterfaces;
using Repository_Interfaces;
using Microsoft.Extensions.Logging;
using Bloggie.Services.Helper;
using Entities.ViewsModel.Tags;

namespace Articly_Services;

public class TagService : ITag
{

    private readonly ITagRepository _tagRepository;
    private readonly ILogger<TagService> _logger;


    public TagService(ITagRepository tagRepository, ILogger<TagService> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<Tag?> AddTagAsync(AddTagRequest TagRequest)
    {
        _logger.LogInformation($"Reached To AddTag() In {this.GetType().Name}");
        try
        {
            if (TagRequest == null)
                throw new ArgumentNullException(nameof(TagRequest));

            List<Tag> tagResponses = await _tagRepository.GetAllAsync();
            TagRequest.DisplayName = TagRequest.DisplayName.ToUpper();

            if (tagResponses.FirstOrDefault(t => t.DisplayName == TagRequest.DisplayName) != null)
                throw new Exception("This Tag Is Already Added");

            ModelValidate.ModelValidation(TagRequest);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return null;
        }


        Tag tag = await _tagRepository.AddAsync(TagRequest.ToTag());


        return tag;
    }

    public async Task<bool> DeleteTag(int id)
    {
        _logger.LogInformation($"Reached To DeleteTag() In {this.GetType().Name}");
        if (id <= 0)
            return false;

        if (this.GetTagById(id).Result == null)
            return false;

        return await _tagRepository.DeleteAsync(id);
    }

    public async Task<Tag?> GetTagById(int id)
    {
        _logger.LogInformation($"Reached To GetTagById() In {this.GetType().Name}");
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
            return null;

        return tag;
    }

    public async Task<Tag?> GetTagByName(string name)
    {
        _logger.LogInformation($"Reached To GetTagByName() In {this.GetType().Name}");
        _logger.LogDebug("Pram", name);

        var tag = this.GetAll().Result.FirstOrDefault(tag => tag.Name == name);

        return tag;
    }

    public async Task<Tag?> UpdateTag(UpdateTagRequest tag)
    {
        Tag? UpdatedTag;
        try
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            UpdatedTag = await _tagRepository.GetByIdAsync(tag.TagId);

            if (UpdatedTag == null)
                throw new Exception("Can't Find This ID");

            ModelValidate.ModelValidation(tag);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return null;
        }

        UpdatedTag.Name = tag.Name;
        UpdatedTag.DisplayName = tag.DisplayName;
        tag.DisplayName = tag.DisplayName.ToUpper();

        Tag TagIsUpdated = await _tagRepository.UpdateAsync(UpdatedTag);

        return TagIsUpdated;
    }
    public async Task<List<Tag>> GetAll()
    {
        _logger.LogInformation("Reached To GetAll In TagServices");

        var TagResponseList = await _tagRepository.GetAllAsync();

        return TagResponseList
            .ToList();
    }



}
