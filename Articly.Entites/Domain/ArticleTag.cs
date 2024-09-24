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
        [Key]
        public int Id { get; set; }

        public int TagID { get; set; }
        public int ArticleID { get; set; }


        public Tag Tag { get; set; }

        public Article Article { get; set; }

    }
}
