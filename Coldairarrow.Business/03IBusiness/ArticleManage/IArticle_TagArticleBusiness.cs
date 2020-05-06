using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ArticleManage
{
    public interface IArticle_TagArticleBusiness
    {
        Task<List<Article_TagArticle>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Article_TagArticle> GetTheDataAsync(string id);
        Task AddDataAsync(Article_TagArticle data);
        Task UpdateDataAsync(Article_TagArticle data);
        Task DeleteDataAsync(List<string> ids);
    }
}