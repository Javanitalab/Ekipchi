using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Group;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IGroupService : IRepository<Group>
    {
        Task<Result<PagedList<GroupDto>>> List( FilterGroupQueryDto filterQueryDto);
        Task<Result> Update(UpdateGroupDto updateGroupDto);
        Task<Result<GroupDto>> Create(CreateGroupDto dto);
        Task<Result<GroupDto>> Get(Guid id);
        Task<Result> Delete(Guid id);
    }
}