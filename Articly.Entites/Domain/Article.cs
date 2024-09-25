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



    public DateTime PublishDate { get; set; }

    [Required]
    [StringLength(50)]
    public string Author { get; set; }

    public bool Visible { get; set; }

    public ICollection<ArticleTag> ArticleTag { get; set; }



}


