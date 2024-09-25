using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewsModel.Tags
{
    public class TagResponse
    {

        public int TagId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public ICollection<Article> Articles { get; set; }


    }
}
