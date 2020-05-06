using Coldairarrow.Business.ArticleManage;
using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.ArticleManage.Article_InfoBusiness;

namespace Coldairarrow.Api.Controllers.ArticleManage
{
    [Route("/ArticleManage/[controller]/[action]")]
    public class Article_InfoController : BaseApiController
    {
        #region DI

        public Article_InfoController(IArticle_InfoBusiness article_InfoBus)
        {
            _article_InfoBus = article_InfoBus;
        }

        IArticle_InfoBusiness _article_InfoBus { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="condition">查询字段</param>
        /// <param name="keyword">关键字</param>
        /// <param name="isall">是否要分页</param>
        /// <param name="isdelete">是否删除</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<Article_InfoDTO>>> GetDataList(Pagination pagination, string condition, string keyword, bool isall, int state = 1, bool isdelete = false)
        {
            var dataList = await _article_InfoBus.GetDataListAsync(pagination, condition, keyword, isall, state, isdelete);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Article_Info> GetTheData(string id)
        {
            return await _article_InfoBus.GetTheDataAsync(id);
        }
        /// <summary>
        /// 获取图片数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public object GetThePicData(string id)
        {
            return _article_InfoBus.GetThePicData(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Article_Info data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _article_InfoBus.AddDataAsync(data);
            }
            else
            {
                await _article_InfoBus.UpdateDataAsync(data);
            }
        }
        /// <summary>
        /// 添加文章（包括图片）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bannerpic"></param>
        /// <param name="CategoryList"></param>
        /// <param name="TagList"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddData(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList)
        {
            await _article_InfoBus.AddDataAsync(data, bannerpic, CategoryList, TagList);
            //data.InitEntity();

            //var bannerid = new Data_FileBusiness().AddBannerPic(data.Id, bannerpic);//插入导航图并返回图片id
            //data.ReleaseTime = DateTime.Now;
            //data.LastModifiedTime = data.ReleaseTime;
            //await _article_InfoBus.UpdateDataAsync(data);//修改文章

        }
        [HttpPost]
        public async Task UpdateData(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList)
        {
            await _article_InfoBus.UpdateArticleDataAsync(data, bannerpic, CategoryList, TagList);
        }
        //[HttpPost]
        //public async Task AddData(List<PicDTO> bannerpic)
        //{
        //    bannerpic.ForEach(x =>
        //    {
        //        var a = x.size;
        //        var b = x.path;
        //        var c = x.name;

        //    });
        //}
        //public async Task AddData(string Author,string dd)
        //{
        //    //await _article_InfoBus.AddDataAsync(Author);
        //}


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _article_InfoBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}