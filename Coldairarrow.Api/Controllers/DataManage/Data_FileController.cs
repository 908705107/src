using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.DataManage
{
    [Route("/DataManage/[controller]/[action]")]
    public class Data_FileController : BaseApiController
    {
        #region DI

        public Data_FileController(IData_FileBusiness data_FileBus)
        {
            _data_FileBus = data_FileBus;
        }

        IData_FileBusiness _data_FileBus { get; }

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
        public async Task<AjaxResult<List<Data_File>>> GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = await _data_FileBus.GetDataListAsync(pagination, condition, keyword);

            return DataTable(dataList, pagination);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Data_File> GetTheData(string id)
        {
            return await _data_FileBus.GetTheDataAsync(id);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="data">保存的数据</param>
        [HttpPost]
        public async Task SaveData(Data_File data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                data.InitEntity();

                await _data_FileBus.AddDataAsync(data);
            }
            else
            {
                await _data_FileBus.UpdateDataAsync(data);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [HttpPost]
        public async Task DeleteData(string ids)
        {
            await _data_FileBus.DeleteDataAsync(ids.ToList<string>());
        }

        #endregion
    }
}