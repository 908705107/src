using Coldairarrow.Business.DataManage;
using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ArticleManage
{
    public class Article_InfoBusiness : BaseBusiness<Article_Info>, IArticle_InfoBusiness, IDependency
    {

        #region 外部接口

        public async Task<List<Article_InfoDTO>> GetDataListAsync(Pagination pagination, string condition, string keyword, bool isall = false, int state = 1, bool isdelete = false)
        {
            Expression<Func<Article_Info, Data_File, Article_InfoDTO>> select = (a, b) => new Article_InfoDTO
            {
                //CategoryID = d.CategoryID,
                FileName = b.Name,
                FilePath = b.FilePath,
                //TagID = e.TagID
            };
            select = select.BuildExtendSelectExpre();
            //筛选
            var where = LinqHelper.True<Article_Info>();
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Article_Info, bool>(
                    ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
                where = where.And(newWhere);
            }
            if (state != 1)
            {
                if (state == 0)//草稿箱
                {
                    where = where.And(x => x.State == 0);
                }
                if (state == -1)//回收站
                {
                    //GlobalSwitch.DeleteMode = DeleteMode.Physic;
                    where = where.And(x => x.State == -1);
                }
            }
            IQueryable<Article_Info> l;
            if (isdelete)
            {
                l = GetIQueryableNoLogic();
                //GlobalSwitch.DeleteMode = DeleteMode.Physic;
                where = where.And(x => x.Deleted == true);
            }
            else
            {
            l = GetIQueryable();
            }
            if (isall)
            {
                l = l.Where(where);

            }

            else
                l = l.Where(where).GetPagination(pagination).AsExpandable();
            //l = l.Where(where).GetPagination(pagination);
            var q = from a in l
                    join b in Service.GetIQueryable<Data_File>() on a.BannerPic equals b.Id into a_b
                    from ab in a_b.DefaultIfEmpty()
                    select @select.Invoke(a, ab);
            var list = q.ToList();
            await SetProperty(list);
            //GlobalSwitch.DeleteMode = DeleteMode.Logic;
            return list;
            async Task SetProperty(List<Article_InfoDTO> _list)
            {
                var allCategory = from a in Service.GetIQueryable<Article_ArticleCateGory>()
                                  join b in Service.GetIQueryable<Data_Category>() on a.CategoryID equals b.Id
                                  select new
                                  {
                                      a.ArtileID,
                                      b.Id,
                                      b.CategoryName
                                  };

                //.Select(x => new { x.Id, x.CategoryName }).ToListAsync();
                var allTag = from a in Service.GetIQueryable<Article_TagArticle>()
                             join b in Service.GetIQueryable<Data_Tag>() on a.TagID equals b.Id
                             select new
                             {
                                 a.ArticleID,
                                 b.Id,
                                 b.TagName
                             };
                var ids = _list.Select(x => x.Id).ToList();
                //var roleActions = await Service.GetIQueryable<Article_ArticleCateGory>()
                //    .Where(x => ids.Contains(x.ArtileID))
                //    .ToListAsync();
                _list.ForEach(aData =>
               {
                   aData.CategoryList = allCategory.Where(x => x.ArtileID == aData.Id).Select(x => x.Id + ',' + x.CategoryName).ToList();
                   aData.TagList = allTag.Where(x => x.ArticleID == aData.Id).Select(x => x.Id + ',' + x.TagName).ToList();
               });
            }
        }

        public async Task<Article_Info> GetTheDataAsync(string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            else
                return (await GetDataListAsync(new Pagination(), "id", id)).FirstOrDefault();
        }

        public object GetThePicData(string id)
        {
            var filelist = Service.GetIQueryable<Data_File>().Where(x => x.ArticleID == id && x.Deleted == false);
            var l = from a in GetIQueryable()
                    join b in filelist on a.BannerPic equals b.Id
                    select new
                    {
                        name = b.Name,
                        type = b.FileExtension,
                        thumbUrl = b.FilePath,
                        path = b.FilePath,
                        size = b.Size
                    };

            return l.FirstOrDefault();

        }


        public async Task AddDataAsync(Article_Info data)
        {
            await InsertAsync(data);
        }

        public async Task AddDataAsync(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList)
        {
            Data_FileBusiness filebll = new Data_FileBusiness();
            data.InitEntity();
            await InsertAsync(data);
            await ProcessDataAsync(data, bannerpic, CategoryList, TagList);//处理数据的复杂逻辑
            await UpdateAsync(data);//修改该文章
        }
        public async Task ProcessDataAsync(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList)
        {
            Data_FileBusiness filebll = new Data_FileBusiness();
            await Service.DeleteAsync<Article_ArticleCateGory>(x => x.ArtileID == data.Id);//删除文章分类表下包含该文章id的所有数据
            await Service.DeleteAsync<Article_TagArticle>(x => x.ArticleID == data.Id);//删除标签分类表下包含该文章id的所有数据
            var aclist = CategoryList.Select(x => new Article_ArticleCateGory
            {
                Id = IdHelper.GetId(),
                Deleted = false,
                CategoryID = x,
                CreateTime = DateTime.Now,
                ArtileID = data.Id
            }).ToList();
            var tagdata = Service.GetIQueryable<Data_Tag>().Select(x => x.Id);
            string tagid;
            TagList.ForEach(x =>
            {
                var tamodel = new Article_TagArticle();
                tamodel.InitEntity();
                tamodel.ArticleID = data.Id;
                if (!tagdata.Any(t => t == x))//假如这个tag是tag表里没有的,就往tag表里新增
                {
                    var datatag = new Data_Tag();
                    datatag.InitEntity();
                    datatag.TagName = x;
                    datatag.TabAlias = x;
                    Service.Insert<Data_Tag>(datatag);
                    tagid = datatag.Id;
                }
                else
                    tagid = x;
                tamodel.TagID = tagid;
                Service.Insert<Article_TagArticle>(tamodel);
            });
            //var atlist = TagList.Select(x => new Article_TagArticle
            //{
            //    Id = IdHelper.GetId(),
            //    Deleted = false,
            //    TagID = x,
            //    CreateTime = DateTime.Now,
            //    ArticleID = data.Id
            //}).ToList();
            await Service.InsertAsync<Article_ArticleCateGory>(aclist);//往文章分类表里插入数据
            var bannerid = filebll.AddBannerPic(data.Id, bannerpic);//往图片表里插入导航图数据，并返回该导航图的id
            data.BannerPic = bannerid;
            if (!data.ReleaseTime.HasValue)
            {
                data.ReleaseTime = DateTime.Now;
            }
            data.LastModifiedTime = DateTime.Now;
        }
        /// <summary>
        /// 修改文章(包括文章的标签和分类等)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bannerpic"></param>
        /// <param name="CategoryList"></param>
        /// <param name="TagList"></param>
        /// <returns></returns>
        public async Task UpdateArticleDataAsync(Article_Info data, List<PicDTO> bannerpic, List<string> CategoryList, List<string> TagList)
        {
            await ProcessDataAsync(data, bannerpic, CategoryList, TagList);//处理数据的复杂逻辑
            await UpdateAsync(data);
        }
        public async Task UpdateDataAsync(Article_Info data)
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
        [MapFrom(typeof(Article_Info))]
        [MapTo(typeof(Article_Info))]
        public class Article_InfoDTO : Article_Info
        {
            public string CategoryID { get; set; }
            public string TagID { get; set; }
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public string StateName { get => State.HasValue ? typeof(EnumType.IsRelease).GetEnumName(State) : "未发布"; }
            public string IsTop
            {
                get => IsToporQuality ? typeof(EnumType.IsTopEnum).GetEnumName(IsToporQuality) : "无";
            }
            /// <summary>
            /// 分类列表
            /// </summary>
            public List<string> CategoryList { get; set; }
            public List<string> TagList { get; set; }

        }

        #endregion
    }
}