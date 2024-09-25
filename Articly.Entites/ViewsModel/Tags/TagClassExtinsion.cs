using Entities.Domain;
using Entities.ViewsModel.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Articly.Entites.ViewsModel.Tags
{
    public static class TagClassExtinsion
    {

        public static TagResponse ToResponse(this Tag tag)
        {
            return new TagResponse() { TagId = tag.TagId, Name = tag.Name, DisplayName = tag.DisplayName };

        }

        public static Tag ToTag(this TagResponse tag)
        {
            return new Tag() { TagId = tag.TagId, Name = tag.Name, DisplayName = tag.DisplayName };

        }

    }
}
