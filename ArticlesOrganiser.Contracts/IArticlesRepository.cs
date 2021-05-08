using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticlesOrganiser.Contracts.Interfaces
{
    public interface IArticlesRepository
    {
        Task<Article> Single(Guid articleId);
        Task<ArticleViewModel> All(string paginationToken = "");
        Task<IEnumerable<Article>> Find(SearchRequest searchReq);
        Task Add(ArticleInputModel entity);
        Task Remove(Guid articleId);
        Task Update(Guid articleId, ArticleInputModel entity);
    }
}
