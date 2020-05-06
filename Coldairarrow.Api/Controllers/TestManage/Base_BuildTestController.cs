using Coldairarrow.Business.TestManage;
using Coldairarrow.Entity.TestManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.TestManage
{
    [Route("/TestManage/[controller]/[action]")]
    public class Base_BuildTestController : BaseApiController
    {
        #region DI

        public Base_BuildTestController(IBase_BuildTestBusiness base_BuildTestBus)
        {
            _base_BuildTestBus = base_BuildTestBus;
        }

        IBase_BuildTestBusiness _base_BuildTestBus { get; }

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
        public async Task<AjaxResult<List<Base_BuildTest>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _base_BuildTestBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Base_BuildTest> GetTheData(string id)
        {
            return await _base_BuildTestBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Base_BuildTest data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _base_BuildTestBus.AddDataAsync(data);
            }
            else
            {
                await _base_BuildTestBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _base_BuildTestBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}