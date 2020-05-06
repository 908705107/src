using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public interface IData_FileBusiness
    {
        Task<List<Data_File>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Data_File> GetTheDataAsync(string id);
        Task AddDataAsync(Data_File data);
        Task UpdateDataAsync(Data_File data);
        Task DeleteDataAsync(List<string> ids);
    }
}