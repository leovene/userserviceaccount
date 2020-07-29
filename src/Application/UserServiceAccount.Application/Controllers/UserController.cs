using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Application;
using UserServiceAccount.Domain.Interfaces.Business;
using UserServiceAccount.Domain.ViewModels;

namespace UserServiceAccount.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IBaseController<UserViewModel>
    {
        private readonly IUserLogic _logic;
        private readonly IMapper _mapper;

        public UserController(IUserLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/Model
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAll() => (await _logic.GetAll().ConfigureAwait(false)).Select(m => _mapper.Map<UserViewModel>(m));

        // GET: api/Model/5
        [HttpGet("{id}")]
        public async Task<UserViewModel> Get(string id) => _mapper.Map<UserViewModel>(await _logic.Get(Guid.Parse(id)).ConfigureAwait(false));

        // POST: api/Model
        [HttpPost]
        public async Task<string> Post([FromBody] UserViewModel viewModel)
        {
            var entityId = await _logic.Post(_mapper.Map<UserEntity>(viewModel)).ConfigureAwait(false);

            return entityId.ToString();
        }

        // PUT: api/Model/5
        [HttpPut("{id}")]
        public async Task<int> Put(string id, [FromBody] UserViewModel viewModel)
        {
            if (viewModel.Id != id)
                throw new ArgumentException(nameof(id));

            return await _logic.Put(_mapper.Map<UserEntity>(viewModel)).ConfigureAwait(false);
        }

        // DELETE: api/Model/5
        [HttpDelete("{id}")]
        public async Task<int> Delete(string id)
        {
            return await _logic.Delete(Guid.Parse(id)).ConfigureAwait(false);
        }
    }
}
