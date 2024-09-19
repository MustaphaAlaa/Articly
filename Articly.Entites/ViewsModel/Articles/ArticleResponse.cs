using Entities.Domain;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewsModel.Articles;


public class ArticleResponse
{
    [Key]
    public Guid ArticleID { get; set; }


    public string Heading { get; set; }


    public string PageTitle { get; set; }


    public string Contnet { get; set; }

    public string ShortDescription { get; set; }


    public string FeaturedImaageUrl { get; set; }

    public DateTime PublishDate { get; set; }


    public string Author { get; set; }

    public bool Visible { get; set; }

    public List<Tag> Tags { get; set; }
}
