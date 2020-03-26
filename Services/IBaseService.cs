using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models;

namespace Notes.Services
{
    public interface IBaseService<TModel, TViewModel, TKey> where TModel : class where TViewModel : class
    {
        Task<DataResult> Create(TViewModel viewModel);
        Task<DataResult> Update(TViewModel viewModel);
        Task<DataResult> Delete(TKey id);
        Task<IReadOnlyList<TViewModel>> GetAll();
        Task<TViewModel> GetById(TKey key);
    }
}
