
using Articly_Services;
using Entities.Domain;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
using Microsoft.Extensions.Logging;
using Reposiory_Interfaces;
using Repository_Interfaces;
using ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ArticleTagServices : IArticleTag
    {

        private readonly IArticleTagRepository _articleRepository;
        private readonly ILogger<ArticleTagServices> _logger;
        private readonly ITag _tag;

        public ArticleTagServices(ITag tag, IArticleTagRepository ArticleTagRepository, ILogger<ArticleTagServices> logger)
        {
            _articleRepository = ArticleTagRepository;
            _logger = logger;
            _tag = tag;
        }

        public async Task<int> AddFromAddArticleRequest(AddArticleRequest addArticleRequest, int ArticleId)

        {
            List<TagResponse?> tagrepos = new();
            TagResponse? tagResponse;

            if (addArticleRequest.SelectedTags != null)
            {
                foreach (var tag in addArticleRequest.SelectedTags)
                {

                    tagResponse = await _tag.GetTagById(int.Parse(tag));

                    tagrepos.Add(tagResponse);
                }
            }
            List<Tag> tags = tagrepos.Select(t => t.ToTag()).ToList();

            foreach (var t in tags)
            {
                await _articleRepository.AddAsync(new ArticleTag()
                {
                    TagID = t.TagId,
                    ArticleID = ArticleId

                });
            }


            return tags.Count;
        }

    }
}
