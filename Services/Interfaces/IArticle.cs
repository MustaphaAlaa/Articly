using Entities.Domain;
using Entities.ViewsModel.Articles;

namespace ServicesInterfaces;

public interface IArticle

{
    public Task<ArticleResponse> AddArticleAsync(AddArticleRequest Article);

    public Task<ArticleResponse?> UpdateArticleAsync(UpdateArticleRequest Article);

    public Task<bool> DeleteArticleAsync(int Id);

    public Task<ArticleResponse?> GetArticleAsync(int id);

    public Task<List<ArticleResponse>> GetAllAsync();
}

