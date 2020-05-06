using Coldairarrow.Entity.ArticleManage;
using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class UploadController : BaseController
    {
        [HttpPost]
        public IActionResult UploadFileByForm()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(new { status = "error" }.ToJson());

            string path = $"/Upload/{DateTime.Now.ToString("yyyyMMdd")}/{file.FileName}";
            //string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = PathHelper.GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{GlobalSwitch.WebRootUrl}{path}";
            var res = new
            {
                name = file.FileName,
                type = file.ContentType,
                status = "done",
                thumbUrl = url,
                url = url,
                path = path
            };

            return JsonContent(res.ToJson());
        }
        //public async Task UploadBannerPic(string articleId, List<PicDTO> piclist)
        //{
        //    //var filelist=(piclist ?? new List<string>())
        //    //    .Select(x => new Data_File
        //    //    {
        //    //        Id = IdHelper.GetId(),
        //    //        ActionId = x,
        //    //        CreateTime = DateTime.Now,
        //    //        RoleId = roleId
        //    //    }).ToList();
        //    //var q = GetIQueryable();
        //    //var where = LinqHelper.True<Data_Category>();

        //    ////筛选
        //    //if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
        //    //{
        //    //    var newWhere = DynamicExpressionParser.ParseLambda<Data_Category, bool>(
        //    //        ParsingConfig.Default, false, $@"{condition}.Contains(@0)", keyword);
        //    //    where = where.And(newWhere);
        //    //}
        //    //var treeList = await q.Where(where).GetPagination(pagination).ToListAsync(); 
        //    //return treeList;
        //}

    }
    public class PicDTO
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int? size { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string thumbUrl { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string path { get; set; }
    }

}
