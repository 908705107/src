using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public interface IData_TagBusiness
    {
        Task<List<Data_Tag>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Data_Tag> GetTheDataAsync(string id);
        Task AddDataAsync(Data_Tag data);
        Task UpdateDataAsync(Data_Tag data);
        Task DeleteDataAsync(List<string> ids);
    }
}