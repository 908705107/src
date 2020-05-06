using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public class Data_CategoryBusiness : BaseBusiness<Data_Category>, IData_CategoryBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Data_CategoryTreeDTO>> GetDataTreeListAsync(string condition, string keyword, bool isall = true)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Data_Category>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Data_Category, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            var treeList = await GetIQueryable().Where(where).Select(x => new Data_CategoryTreeDTO
            {
                Id = x.Id,
                Value = x.Id,
                Text = x.CategoryName,
                Type = x.Type,
                Pid = x.Pid,
                ParentId = x.Pid,
                Remark = x.Remark,
                CategoryName = x.CategoryName
            }).ToListAsync();
            if (isall)
                return TreeHelper.BuildTree(treeList);
            else
            {
                return treeList.Where(x => x.ParentId == "0").ToList();//只查询根类型，不查询子类型
            }
        }
        public async Task<List<Data_Category>> GetDataListAsync(Pagination pagination,string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Data_Category>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Data_Category, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            var treeList = await q.Where(where).GetPagination(pagination).ToListAsync();;
            return treeList;
        }

        public async Task<Data_Category> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Data_Category data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Data_Category data)
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
        public class Data_CategoryTreeDTO : TreeModel
        {
            public object children { get => Children; }
            public string title { get => Text; }
            public string value { get => Id; }
            public string key { get => Id; }
            public string CategoryName { get; set; }
            public string Type { get; set; }
            public string Pid { get; set; }
            public string Remark { get; set; }

        }
        #endregion
    }
}