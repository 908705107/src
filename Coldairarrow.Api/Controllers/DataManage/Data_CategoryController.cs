using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.DataManage.Data_CategoryBusiness;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class Data_CategoryController : BaseApiController
    {
        #region DI

        public Data_CategoryController(IData_CategoryBusiness data_CategoryBus)
        {
            _data_CategoryBus = data_CategoryBus;
        }

        IData_CategoryBusiness _data_CategoryBus { get; }

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
        public async Task<AjaxResult<List<Data_Category>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _data_CategoryBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }
        /// <summary>
        /// 查询分类的树结构
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="keyword"></param>
        /// <param name="isall">是否查询全部树结构</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<Data_CategoryTreeDTO>>> GetDataTreeListAsync(string condition, string keyword, bool isall)
        {
            var dataList = await _data_CategoryBus.GetDataTreeListAsync(condition, keyword,isall);

            return DataTable(dataList);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Data_Category> GetTheData(string id)
        {
            return await _data_CategoryBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Data_Category data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _data_CategoryBus.AddDataAsync(data);
            }
            else
            {
                await _data_CategoryBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _data_CategoryBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}