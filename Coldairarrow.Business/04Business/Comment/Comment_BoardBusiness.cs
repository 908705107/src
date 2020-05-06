using Coldairarrow.Business.Base_Manage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Entity.Comment;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Coldairarrow.Business.ArticleManage.Article_InfoBusiness;

namespace Coldairarrow.Business.Comment
{
    public class Comment_BoardBusiness : BaseBusiness<Comment_Board>, IComment_BoardBusiness, IDependency
    {
        #region 外部接口

        //public async Task<List<Comment_Board>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        //{
        //    var q = GetIQueryable();
        //    var where = LinqHelper.True<Comment_Board>();

        //    //筛选
        //    if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
        //    {
        //        var newWhere = DynamicExpressionParser.ParseLambda<Comment_Board, bool>(
        //            ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
        //        where = where.And(newWhere);
        //    }

        //    return await q.Where(where).GetPagination(pagination).ToListAsync();
        //}
        public async Task<List<Comment_BoardDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            Expression<Func<Comment_Board, Data_User, Comment_BoardDTO>> select = (a, b) => new Comment_BoardDTO
            {
                Id = a.Id,
                ParentId = a.CommentPID,
                CommentInfo = a.CommentInfo,
                Supports = a.Supports.GetValueOrDefault(),
                UserID = b.Id,
                UserName = b.UserName,
                CreateTime = a.CreateTime
            };
            //select = select.BuildExtendSelectExpre();
            var where = LinqHelper.True<Comment_Board>();

            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Comment_Board, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            var q = GetIQueryable().Where(where).AsExpandable();
            var userlist =await Service.GetIQueryable<Data_User>().ToListAsync();
            var treeList = from a in q
                           join b in userlist on a.UserID equals b.Id into a_b
                           from ab in a_b.DefaultIfEmpty()
                           select @select.Invoke(a, ab);
            //var treeList =q.Select(x => new Comment_BoardDTO
            //{
            //    Id = x.Id,
            //    ParentId = x.CommentPID,
            //    CommentInfo = x.CommentInfo,
            //    Supports = x.Supports
            //    //UserNmae = userapp.GetEntity(x.Id).UserName
            //}).ToList();

            return TreeHelper.BuildTree(treeList.ToList());
        }

        public async Task<Comment_Board> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Comment_Board data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Comment_Board data)
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
        //[MapFrom(typeof(Comment_Board))]
        //[MapTo(typeof(Comment_Board))]
        public class Comment_BoardDTO : TreeModel
        {
            /// <summary>
            /// 子节点
            /// </summary>
            public object children { get => Children; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }

            /// <summary>
            /// 创建人Id
            /// </summary>
            public String CreatorId { get; set; }

            /// <summary>
            /// 否已删除
            /// </summary>
            public Boolean Deleted { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            public String UserID { get; set; }
            /// <summary>
            /// 用户名称
            /// </summary>
            public String UserName { get; set; }

            /// <summary>
            /// 留言ID
            /// </summary>
            public String CommentID { get; set; }

            /// <summary>
            /// 父留言ID
            /// </summary>
            public String CommentPID { get; set; }

            /// <summary>
            /// 留言内容
            /// </summary>
            public String CommentInfo { get; set; }

            /// <summary>
            /// 点赞数
            /// </summary>
            public Int32? Supports { get; set; }
        }
        #endregion
    }
}