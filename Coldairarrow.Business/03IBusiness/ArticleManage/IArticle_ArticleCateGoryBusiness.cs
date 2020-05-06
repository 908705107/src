using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ArticleManage
{
    public interface IArticle_ArticleCateGoryBusiness
    {
        Task<List<Article_ArticleCateGory>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Article_ArticleCateGory> GetTheDataAsync(string id);
        Task AddDataAsync(Article_ArticleCateGory data);
        Task UpdateDataAsync(Article_ArticleCateGory data);
        Task DeleteDataAsync(List<string> ids);
    }
}