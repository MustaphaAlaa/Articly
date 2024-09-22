using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiory_Interfaces
{
    public interface IArticleTagRepository
    {

        public Task<ArticleTag> AddAsync(ArticleTag article);

        public Task<ArticleTag?> UpdateAsync(ArticleTag article);

        public Task<bool> DeleteAsync(int Id);

        public Task<ArticleTag?> GetByIdAsync(int Id);

        public Task<List<ArticleTag>> GetAllAsync();

    }
}
