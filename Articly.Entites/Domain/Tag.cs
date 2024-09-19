
using Entities.ViewsModel.Tags;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Domain;

public class Tag
{
    [Key]
    public Guid TagID { get; set; }
    [Required]
    [StringLength(30)]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }



    public IEnumerable<ArticlesTags> ArticlesTags { get; }

    public TagResponse ToResponse()
    {
        return new TagResponse() { TagID = this.TagID, Name = this.Name, DisplayName = this.DisplayName };

    }

}