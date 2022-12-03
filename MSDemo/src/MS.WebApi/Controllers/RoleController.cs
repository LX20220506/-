using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS.DbContexts;
using MS.Entities;
using MS.Models.Core;
using MS.Models.ViewModel;
using MS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : AuthorizeController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<ExecuteResult> Post(RoleViewModel viewModel) {
            return await _roleService.Create(viewModel);
        }

        [HttpDelete]
        public async Task<ExecuteResult> Delete(RoleViewModel viewModel) {
            return await _roleService.Delete(viewModel);
        }

        [HttpPut]
        public async Task<ExecuteResult> Update(RoleViewModel viewModel) {
            return await _roleService.Update(viewModel);
        }
    }
}
