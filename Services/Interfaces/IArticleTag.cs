using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Microsoft.Extensions.Logging;
using Repository_Interfaces;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces
{
    public interface IArticleTag
    {

        public Task<int> AddFromAddArticleRequest(AddArticleRequest addArticleRequest, int ArticleId);

    }
}
