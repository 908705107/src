using Coldairarrow.Entity.Comment;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.Comment.Comment_BoardBusiness;

namespace Coldairarrow.Business.Comment
{
    public interface IComment_BoardBusiness
    {
        Task<List<Comment_BoardDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Comment_Board> GetTheDataAsync(string id);
        Task AddDataAsync(Comment_Board data);
        Task UpdateDataAsync(Comment_Board data);
        Task DeleteDataAsync(List<string> ids);
    }
}