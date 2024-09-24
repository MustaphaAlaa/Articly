using Entities.Domain;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewsModel.Articles;


public class UpdateArticleRequest
{
    [Key]
    public int ArticleId { get; set; }

    [Required]
    public string Heading { get; set; }

    [Required]
    public string PageTitle { get; set; }

    [Required]
    public string Contnet { get; set; }

    [Required]
    public string ShortDescription { get; set; }

    [Required]
    public string FeaturedImaageUrl { get; set; }

    public bool Visible { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public List<string> SelectedTags
    {
        get; set;
    }




}




