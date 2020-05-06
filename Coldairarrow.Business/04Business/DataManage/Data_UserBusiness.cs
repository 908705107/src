using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public class Data_UserBusiness : BaseBusiness<Data_User>, IData_UserBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Data_User>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Data_User>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Data_User, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPagination(pagination).ToListAsync();
        }

        public async Task<Data_User> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Data_User data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Data_User data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}