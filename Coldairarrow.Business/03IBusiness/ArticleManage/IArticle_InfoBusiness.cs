using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.ArticleManage.Article_InfoBusiness;

namespace Coldairarrow.Business.ArticleManage
{
    public interface IArticle_InfoBusiness
    {
        Task<List<Article_InfoDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword, bool isall,int state, bool isdelete);
        Task<Article_Info> GetTheDataAsync(string id);
        object GetThePicData(string id);
        Task AddDataAsync(Article_Info data);
        Task AddDataAsync(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList);
        Task UpdateArticleDataAsync(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList);

        Task UpdateDataAsync(Article_Info data);
        Task DeleteDataAsync(List<string> ids);
    }
}