using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public class Data_FileBusiness : BaseBusiness<Data_File>, IData_FileBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Data_File>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Data_File>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Data_File, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            return await q.Where(where).GetPagination(pagination).ToListAsync();
        }

        public async Task<Data_File> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Data_File data)
        {
            await InsertAsync(data);
        }
        public string AddBannerPic(string articleId, List<PicDTO> list)
        {
            Delete(x => x.ArticleID == articleId);
            string picid = "";
            if (list.Count > 0)
            {
                var bannerpic = list.Select(x => new Data_File
                {
                    Id = IdHelper.GetId(),
                    Size = x.size,
                    Name = x.name,
                    FilePath = x.path,
                    FileExtension = x.type,
                    IsFolder = 0,
                    CreateTime = DateTime.Now,
                    ArticleID = articleId
                }).First();
                Insert(bannerpic);
                picid = bannerpic.Id;
            }
            return picid;

        }
        public void AddContentPiclist(string articleId, string htmlstr)
        {
            var piclist = new List<string>();
            piclist = ImgHelper.getImgStr(htmlstr);
        }

        public async Task UpdateDataAsync(Data_File data)
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