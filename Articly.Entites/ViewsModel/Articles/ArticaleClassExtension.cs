using Entities.Domain;
using Entities.ViewsModel.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articly.Entites.ViewsModel.Articles
{
    public static class ArticaleClassExtension
    {

        public static Article ToArticle(this UpdateArticleRequest updateArticle)
        {
            return new Article()
            {
                ArticleId = updateArticle.ArticleId,

                Heading = updateArticle.Heading,

                PageTitle = updateArticle.PageTitle,

                Contnet = updateArticle.Contnet,

                ShortDescription = updateArticle.ShortDescription,

                FeaturedImaageUrl = updateArticle.FeaturedImaageUrl,

                Visible = updateArticle.Visible,
                Tags = updateArticle.Tags,

            };
        }



        public static ArticleResponse ToResponse(this Article article)

        {

            return new ArticleResponse()
            {
                ArticleId = article.ArticleId,


                Heading = article.Heading,


                PageTitle = article.PageTitle,


                Contnet = article.Contnet,

                ShortDescription = article.ShortDescription,


                FeaturedImaageUrl = article.FeaturedImaageUrl,

                PublishDate = article.PublishDate,

                Author = article.Author,

                Visible = article.Visible,

                Tags = article.Tags
            };
        }

        public static Article ToArticle(this ArticleResponse article)

        {

            return new Article()
            {
                ArticleId = article.ArticleId,


                Heading = article.Heading,


                PageTitle = article.PageTitle,


                Contnet = article.Contnet,

                ShortDescription = article.ShortDescription,


                FeaturedImaageUrl = article.FeaturedImaageUrl,

                PublishDate = article.PublishDate,

                Author = article.Author,

                Visible = article.Visible,

                Tags = article.Tags
            };
        }


        public static UpdateArticleRequest ToUpdateArticleRequest(this ArticleResponse article)

        {

            return new UpdateArticleRequest()
            {
                ArticleId = article.ArticleId,


                Heading = article.Heading,


                PageTitle = article.PageTitle,


                Contnet = article.Contnet,

                ShortDescription = article.ShortDescription,


                FeaturedImaageUrl = article.FeaturedImaageUrl,


                Visible = article.Visible,

                Tags = article.Tags
            };
        }






    }
}
