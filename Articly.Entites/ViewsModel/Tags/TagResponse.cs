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

        public int TagID { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        //public Tag ToTag()
        //{
        //    return new Tag()
        //    {
        //        TagID = this.TagID,

        //        Name = this.Name,
        //        DisplayName = this.DisplayName
        //    };
        //}
    }
}
