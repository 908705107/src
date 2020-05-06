using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ArticleManage
{
    public class Article_TagArticleBusiness : BaseBusiness<Article_TagArticle>, IArticle_TagArticleBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Article_TagArticle>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Article_TagArticle>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Article_TagArticle, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPagination(pagination).ToListAsync();
        }

        public async Task<Article_TagArticle> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Article_TagArticle data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Article_TagArticle data)
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