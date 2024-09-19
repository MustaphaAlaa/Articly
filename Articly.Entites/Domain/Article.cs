using System.ComponentModel.DataAnnotations;
using System;
using Entities.ViewsModel.Blogs;
namespace Entities.Domain;

public class Article 
{

    [Key]
    public Guid BlogID { get; set; } = Guid.NewGuid();

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

    public DateTime PublishDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Author { get; set; }

    public bool Visible { get; set; }


    public IEnumerable<BlogsTags> Blogs { get; set; }


    public BlogResponse ToResponse()

    {

        return new BlogResponse()
        {
            BlogID = this.BlogID,


            Heading = this.Heading,


            PageTitle = this.PageTitle,


            Contnet = this.Contnet,

            ShortDescription = this.ShortDescription,


            FeaturedImaageUrl = this.FeaturedImaageUrl,

            PublishDate = this.PublishDate,

            Author = this.Author,

            Visible = this.Visible
        };
    }
}
