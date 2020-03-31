using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Models;

namespace Notes.Repositories
{
    public interface IBaseRepository
    {
        Task<DataResult<T>> Create<T>(T model) where T : BaseModel;
        Task<DataResult<T>> Update<T>(T model) where T : BaseModel;
        Task<DataResult> Delete<T>(T model) where T : BaseModel;

        Task<DataResult> CreateBatch<T>(List<T> model) where T : BaseModel;
        Task<DataResult> UpdateBatch<T>(List<T> model) where T : BaseModel;
        Task<DataResult> DeleteBatch<T>(List<T> model) where T : BaseModel;

        Task<DataResult> DeleteUpdate<TDelete, TUpdate>(TDelete dModel, TUpdate uModel) where TDelete : BaseModel where TUpdate : BaseModel;
        Task<DataResult> DeleteSave<TDelete, TSave>(TDelete dModel, TSave sModel) where TDelete : BaseModel where TSave : BaseModel;

        Task<DataResult> DeleteBatchUpdate<TDelete, TUpdate>(List<TDelete> dModel, TUpdate uModel) where TDelete : BaseModel where TUpdate : BaseModel;
        Task<DataResult> DeleteBatchSave<TDelete, TSave>(List<TDelete> dModel, TSave sModel) where TDelete : BaseModel where TSave : BaseModel;

        IEnumerable<T> GetAll<T>() where T : BaseModel;
        IQueryable<T> GetAllAsync<T>() where T : BaseModel;
        Task<T> GetById<T, TKey>(TKey key) where T : BaseModel;
    }
}
