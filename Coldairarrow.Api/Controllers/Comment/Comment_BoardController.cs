using Coldairarrow.Business.Comment;
using Coldairarrow.Entity.Comment;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.Comment.Comment_BoardBusiness;

namespace Coldairarrow.Api.Controllers.Comment
{
    [Route("/Comment/[controller]/[action]")]
    public class Comment_BoardController : BaseApiController
    {
        #region DI

        public Comment_BoardController(IComment_BoardBusiness comment_BoardBus)
        {
            _comment_BoardBus = comment_BoardBus;
        }

        IComment_BoardBusiness _comment_BoardBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="condition">查询字段</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<Comment_BoardDTO>>> GetTreeDataList(Pagination pagination, string condition, string keyword)
        {
               var dataList = await _comment_BoardBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable<Comment_BoardDTO>(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Comment_Board> GetTheData(string id)
        {
            return await _comment_BoardBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Comment_Board data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _comment_BoardBus.AddDataAsync(data);
            }
            else
            {
                await _comment_BoardBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _comment_BoardBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}