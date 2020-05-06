using Coldairarrow.Entity.Comment;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.Comment.Comment_ArticleUserBusiness;

namespace Coldairarrow.Business.Comment
{
    public interface IComment_ArticleUserBusiness
    {
        Task<List<Comment_ArticleUserDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Comment_ArticleUser> GetTheDataAsync(string id);
        Task AddDataAsync(Comment_ArticleUser data);
        Task UpdateDataAsync(Comment_ArticleUser data);
        Task DeleteDataAsync(List<string> ids);
    }
}