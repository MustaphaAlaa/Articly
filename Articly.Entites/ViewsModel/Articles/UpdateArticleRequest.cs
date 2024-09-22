using Entities.Domain;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewsModel.Articles;


public class UpdateArticleRequest
{
    [Key]
    public int ArticleID { get; set; }

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

    public IEnumerable<Tag> Tags { get; set; }



    //public Blog ToBlog()
    //{
    //    return new Blog()
    //    {
    //        BlogId = Guid.NewGuid(),

    //        Heading = this.Heading,

    //        PageTitle = this.PageTitle,

    //        Contnet = this.Contnet,

    //        ShortDescription = this.ShortDescription,

    //        FeaturedImaageUrl = this.FeaturedImaageUrl,
    //        PublishDate = this.PublishDate,

    //        Author = this.Author,

    //        Visible = this.Visible


    //    };
    //}


}




