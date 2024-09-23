
using Entities.ViewsModel.Tags;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Domain;

public class Tag
{
    [Key]
    public int TagId { get; set; }
    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    public string DisplayName { get; set; }


    public ICollection<Article> Articles { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; }



}