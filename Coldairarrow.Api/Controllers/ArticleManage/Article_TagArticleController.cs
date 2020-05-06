using Coldairarrow.Business.ArticleManage;
using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ArticleManage
{
    [Route("/ArticleManage/[controller]/[action]")]
    public class Article_TagArticleController : BaseApiController
    {
        #region DI

        public Article_TagArticleController(IArticle_TagArticleBusiness article_TagArticleBus)
        {
            _article_TagArticleBus = article_TagArticleBus;
        }

        IArticle_TagArticleBusiness _article_TagArticleBus { get; }

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
        public async Task<AjaxResult<List<Article_TagArticle>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _article_TagArticleBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Article_TagArticle> GetTheData(string id)
        {
            return await _article_TagArticleBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Article_TagArticle data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _article_TagArticleBus.AddDataAsync(data);
            }
            else
            {
                await _article_TagArticleBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _article_TagArticleBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}