using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.ViewsModel.Articles;

public class AddArticleRequest
{


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

    public List<Tag> Tags { get; set; }
    public List<string> SelectedTags
    {
        get; set;
    }



    public Article ToArticle()
    {
        return new Article()
        {


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
