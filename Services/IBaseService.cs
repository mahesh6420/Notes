using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models;
using Notes.ViewModels;

namespace Notes.Services
{
    public interface IBaseService<TModel, TViewModel, TKey> where TModel : class where TViewModel : class
    {
        Task<int> GetTotalCount();
        Task<DataResult<TModel>> Create(TViewModel viewModel);
        Task<DataResult<TModel>> Update(TViewModel viewModel);
        Task<DataResult> Delete(TKey id);
        Task<IReadOnlyList<TViewModel>> GetAll(QueryParamViewModel queryParam);
        Task<TViewModel> GetById(TKey key);
    }
}
