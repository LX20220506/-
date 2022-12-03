using AutoMapper;
using MS.Common.IDCode;
using MS.DbContexts;
using MS.Entities;
using MS.Models.Core;
using MS.Models.ViewModel;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker) : base(unitOfWork, mapper, idWorker)
        {
        }

        public async Task<ExecuteResult<Role>> Create(RoleViewModel viewModel)
        {
            // 创建一个返回结果
            ExecuteResult<Role> result=new ExecuteResult<Role>();

            // 判断操作是否合法
            if (viewModel.CheckField(ExecuteType.Create,_unitOfWork) is ExecuteResult checkResult // 判断创建角色是否合法，并通过is接受返回值
                && !checkResult.IsSucceed)// 判断是否有执行结果
            {
                // 操作不合法时，将错误信息赋值给result
                return result.SetFailMessage(checkResult.Message);
            }

            // 开启事务
            using var tran = _unitOfWork.BeginTransaction();

            // 创建对象
            Role role = _mapper.Map<Role>(viewModel);
            role.Id = _idWorker.NextId(); // 获取一个雪花Id
            role.Creator = 1219490056771866624; // 由于暂时还没有做登录，所以拿不到登录者信息，先随便写一个后面再完善
            role.CreateTime = DateTime.Now;

            // 添加对象
            _unitOfWork.GetRepository<Role>().Inster(role);
            // 异步保存
            var SS = await _unitOfWork.SaveChangesAsync();

            // 提交事务
            await tran.CommitAsync();


            // 将刚才添加的实体返回回去
            result.SetData(role);


            return result;
        }

        public async Task<ExecuteResult> Delete(RoleViewModel viewModel)
        {
            ExecuteResult result = new ExecuteResult();

            // 检查是否合法
            if (viewModel.CheckField(ExecuteType.Delete,_unitOfWork) is ExecuteResult checkResult
                && !checkResult.IsSucceed)
            {
                return result.SetFailMessage(checkResult.Message);
            }
            // 开启一个事务
            using var tran = _unitOfWork.BeginTransaction(); // 这里可以不用事务
            _unitOfWork.GetRepository<Role>().Delete(viewModel.Id);

            await _unitOfWork.SaveChangesAsync();// 保存
            await tran.CommitAsync(); // 提交

            return result;
        }


        public async Task<ExecuteResult> Update(RoleViewModel viewModel)
        {
            ExecuteResult result = new ExecuteResult();

            // 检查是否合法
            if (viewModel.CheckField(ExecuteType.Update,_unitOfWork) is ExecuteResult checkResult
                && !checkResult.IsSucceed)
            {
                return result.SetFailMessage(checkResult.Message);
            }

            Role role = await _unitOfWork.GetRepository<Role>().FindAsync(viewModel.Id);

            role.Name = viewModel.Name;
            role.Remark = viewModel.Remark;
            role.DisplayName = viewModel.DisplayName;

            _unitOfWork.GetRepository<Role>().Update(role);

            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
