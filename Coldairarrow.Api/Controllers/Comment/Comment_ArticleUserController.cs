using Coldairarrow.Business.Comment;
using Coldairarrow.Entity.Comment;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.Comment.Comment_ArticleUserBusiness;

namespace Coldairarrow.Api.Controllers.Comment
{
    [Route("/Comment/[controller]/[action]")]
    public class Comment_ArticleUserController : BaseApiController
    {
        #region DI

        public Comment_ArticleUserController(IComment_ArticleUserBusiness comment_ArticleUserBus)
        {
            _comment_ArticleUserBus = comment_ArticleUserBus;
        }

        IComment_ArticleUserBusiness _comment_ArticleUserBus { get; }

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
        public async Task<AjaxResult<List<Comment_ArticleUserDTO>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _comment_ArticleUserBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Comment_ArticleUser> GetTheData(string id)
        {
            return await _comment_ArticleUserBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Comment_ArticleUser data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _comment_ArticleUserBus.AddDataAsync(data);
            }
            else
            {
                await _comment_ArticleUserBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _comment_ArticleUserBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}