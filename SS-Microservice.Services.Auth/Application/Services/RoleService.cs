using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Role.Queries;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Specifications.Role;
using SS_Microservice.Services.Auth.Domain.Entities;

namespace SS_Microservice.Services.Auth.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<RoleDto>> GetListRole(GetListRoleQuery query)
        {
            var roles = await _unitOfWork.Repository<AppRole>().ListAsync(new RoleSpecification(query, isPaging: true));
            var count = await _unitOfWork.Repository<AppRole>().CountAsync(new RoleSpecification(query));

            return new PaginatedResult<RoleDto>(roles.Select(x => _mapper.Map<RoleDto>(x)).ToList(), query.PageIndex, count, query.PageSize);
        }

        public async Task<RoleDto> GetRole(GetRoleQuery query)
        {
            var role = await _unitOfWork.Repository<AppRole>().GetById(query.Id);

            return _mapper.Map<RoleDto>(role);
        }
    }
}
