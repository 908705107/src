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

namespace Coldairarrow.Business.Comment
{
    public class Comment_ArticleUserBusiness : BaseBusiness<Comment_ArticleUser>, IComment_ArticleUserBusiness, IDependency
    {
        #region 外部接口

        public async Task<List<Comment_ArticleUserDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword)
        {
            var where = LinqHelper.True<Comment_ArticleUser>();
            Expression<Func<Comment_ArticleUser, Data_User, Article_Info, Comment_ArticleUserDTO>> select = (a, b, c) => new Comment_ArticleUserDTO
            {
                Id=a.Id,
                ParentId = a.CommentPID,
                CommentInfo = a.CommentInfo,
                UserType = b.UserType,
                Supports = a.Supports,
                UserID = b.Id,
                UserName = b.UserName,
                CreateTime = a.CreateTime,
                Tittle =c.Tittle
                
            };
            //筛选
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Comment_ArticleUser, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            var q = GetIQueryable().Where(where).AsExpandable();
            var userlist = await Service.GetIQueryable<Data_User>().ToListAsync();
            var treeList = from a in q
                           join b in userlist on a.UserID equals b.Id into a_b
                           from ab in a_b.DefaultIfEmpty()
                           join c in Service.GetIQueryable<Article_Info>() on a.ArticleID equals c.Id into a_c
                           from ac in a_c.DefaultIfEmpty()
                           select @select.Invoke(a,ab,ac);
            return TreeHelper.BuildTree(treeList.ToList());
            //return await q.Where(where).GetPagination(pagination).ToListAsync();
        }

        public async Task<Comment_ArticleUser> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Comment_ArticleUser data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Comment_ArticleUser data)
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
        public class Comment_ArticleUserDTO : TreeModel
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
            /// 文章标题
            /// </summary>
            public string Tittle { get; set; }

            /// <summary>
            /// 评论ID
            /// </summary>
            public String CommentID { get; set; }

            /// <summary>
            /// 父评论ID
            /// </summary>
            public String CommentPID { get; set; }

            /// <summary>
            /// 评论内容
            /// </summary>
            public String CommentInfo { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            public String UserID { get; set; }
            /// <summary>
            /// 用户类型
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 用户类型
            /// </summary>
            public string UserType { get; set; }

            /// <summary>
            /// 文章ID
            /// </summary>
            public String ArticleID { get; set; }
            /// <summary>
            /// 点赞数量
            /// </summary>
            public int? Supports { get; set; }
        }
        #endregion
    }
}