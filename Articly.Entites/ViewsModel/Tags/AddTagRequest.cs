using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewsModel.Tags
{
    public class AddTagRequest
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }


        public Tag ToTag()
        {
            return new Tag() { Name = this.Name, DisplayName = this.DisplayName };
        }
    }
}
