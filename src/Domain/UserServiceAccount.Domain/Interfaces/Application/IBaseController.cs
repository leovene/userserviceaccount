using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserServiceAccount.Domain.ViewModels;

namespace UserServiceAccount.Domain.Interfaces.Application
{
    public interface IBaseController<TViewModel> where TViewModel : BaseViewModel
    {
        Task<IEnumerable<TViewModel>> GetAll();

        Task<TViewModel> Get(string id);

        Task<string> Post([FromBody] TViewModel viewModel);

        Task<int> Put(string id, [FromBody] TViewModel viewModel);

        Task<int> Delete(string id);
    }
}
