using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class Data_TagController : BaseApiController
    {
        #region DI

        public Data_TagController(IData_TagBusiness data_TagBus)
        {
            _data_TagBus = data_TagBus;
        }

        IData_TagBusiness _data_TagBus { get; }

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
        public async Task<AjaxResult<List<Data_Tag>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _data_TagBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Data_Tag> GetTheData(string id)
        {
            return await _data_TagBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Data_Tag data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _data_TagBus.AddDataAsync(data);
            }
            else
            {
                await _data_TagBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _data_TagBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}