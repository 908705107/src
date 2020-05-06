using Coldairarrow.Entity.TestManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.TestManage
{
    public class Base_BuildTestBusiness : BaseBusiness<Base_BuildTest>, IBase_BuildTestBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Base_BuildTest>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_BuildTest>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_BuildTest, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPagination(pagination).ToListAsync();
        }

        public async Task<Base_BuildTest> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Base_BuildTest data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Base_BuildTest data)
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