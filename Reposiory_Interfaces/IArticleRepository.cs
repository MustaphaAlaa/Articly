using Entities.Domain;
using Entities.ViewsModel;
using Entities.ViewsModel.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Interfaces
{
    public interface IArticleRepository
    {
        public Task<Article> AddAsync(Article article);

        public Task<Article?> UpdateAsync(Article article);

        public Task<bool> DeleteAsync(int Id);

        public Task<Article?> GetByIdAsync(int Id);

        public Task<List<Article>> GetAllAsync();
    }
}
