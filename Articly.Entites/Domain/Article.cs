using System.ComponentModel.DataAnnotations;
using System;
using Entities.ViewsModel.Articles;
using System.Runtime.CompilerServices;
using Entities.ViewsModel.Tags;
namespace Entities.Domain;

public class Article
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

    public DateTime PublishDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Author { get; set; }

    public bool Visible { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public ICollection<ArticleTag> ArticleTag { get; set; }



}


public static class Extensions
{
    public static ArticleResponse ToResponse(this Article article)

    {

        return new ArticleResponse()
        {
            ArticleID = article.ArticleId,


            Heading = article.Heading,


            PageTitle = article.PageTitle,


            Contnet = article.Contnet,

            ShortDescription = article.ShortDescription,


            FeaturedImaageUrl = article.FeaturedImaageUrl,

            PublishDate = article.PublishDate,

            Author = article.Author,

            Visible = article.Visible,

            Tags = article.Tags
        };
    }




    public static TagResponse ToResponse(this Tag tag)
    {
        return new TagResponse() { TagID = tag.TagId, Name = tag.Name, DisplayName = tag.DisplayName, Articles = tag.Articles };

    }

    public static Tag ToTag(this TagResponse tag)
    {
        return new Tag() { TagId = tag.TagID, Name = tag.Name, DisplayName = tag.DisplayName, Articles = tag.Articles };

    }




}
