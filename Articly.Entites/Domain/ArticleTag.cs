using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class ArticleTag
    {


        public int TagId { get; set; }
        public int ArticleId { get; set; }


        public Tag Tag { get; set; }

        public Article Article { get; set; }

    }
}
