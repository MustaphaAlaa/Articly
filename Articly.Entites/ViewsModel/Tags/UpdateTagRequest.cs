using System.ComponentModel.DataAnnotations;

namespace Entities.ViewsModel.Tags;

public class UpdateTagRequest
{
    [Key]
    public int TagId { get; set; }
    [Required]
    [StringLength(30)]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }






}
