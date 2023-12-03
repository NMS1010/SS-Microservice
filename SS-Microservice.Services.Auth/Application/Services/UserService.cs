using AutoMapper;
using green_craze_be_v1.Application.Specification.User;
using Microsoft.AspNetCore.Identity;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Dto;
using SS_Microservice.Services.Auth.Application.Features.Staff.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Commands;
using SS_Microservice.Services.Auth.Application.Features.User.Queries;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Application.Model.User;
using SS_Microservice.Services.Auth.Application.Specifications.User;
using SS_Microservice.Services.Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SS_Microservice.Services.Auth.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UserService(UserManager<AppUser> userManager, IMapper mapper,
            IUploadService uploadService, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<bool> ChangePassword(ChangePasswordCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId)
                ?? throw new NotFoundException("Cannot find this user");

            if (command.NewPassword != command.ConfirmPassword)
                throw new ValidationException("Confirm password does not match");

            var res = await _userManager.ChangePasswordAsync(user, command.OldPassword, command.NewPassword);
            if (!res.Succeeded)
                throw new ValidationException("Cannot change your password, your OldPassword may be incorrect");

            return true;
        }

        public async Task<string> CreateStaff(CreateStaffCommand command)
        {
            var user = _mapper.Map<AppUser>(command);
            user.UserName = Regex.Replace(command.Email, "[^A-Za-z0-9 -]", "");
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = _currentUserService.UserId;
            if (command.Avatar != null)
            {
                var url = await _uploadService.UploadFile(command.Avatar);
                user.Avatar = url;
            }
            var staff = new Staff()
            {
                Type = command.Type,
                Code = command.Code,
            };
            user.Staff = staff;
            var res = await _userManager.CreateAsync(user, command.Password);

            if (res.Succeeded)
            {
                List<string> roles = new()
                    {
                       USER_ROLE.STAFF,
                       USER_ROLE.USER
                    };
                await _userManager.AddToRolesAsync(user, roles);

                return user.Id;
            }

            string error = "";
            res.Errors.ToList().ForEach(x => error += x.Description + "/n");
            throw new Exception(error);
        }

        public async Task<bool> DisableListUserStatus(DisableListUserCommand command)
        {
            foreach (var userId in command.UserIds)
            {
                var user = await _userManager.FindByIdAsync(userId)
                    ?? throw new NotFoundException("Cannot find this user");
                user.Status = 0;
                user.UpdatedAt = DateTime.Now;
                user.UpdatedBy = _currentUserService.UserId;
                var res = await _userManager.UpdateAsync(user);
                if (!res.Succeeded)
                    throw new Exception("Cannot toggle status of user");
            }

            return true;
        }

        public async Task<PaginatedResult<StaffDto>> GetListStaff(GetListStaffQuery query)
        {
            var staffSpec = new StaffSpecification(query, isPaging: true);

            var countSpec = new StaffSpecification(query);

            var staffList = await _unitOfWork.Repository<Staff>().ListAsync(staffSpec);
            var count = await _unitOfWork.Repository<Staff>().CountAsync(countSpec);
            var listDto = new List<StaffDto>();
            foreach (var staff in staffList)
            {
                var staffDto = _mapper.Map<StaffDto>(staff);
                staffDto.User.Roles = (await _userManager.GetRolesAsync(staff.User)).ToList();

                listDto.Add(staffDto);
            }

            return new PaginatedResult<StaffDto>(listDto, query.PageIndex, count, query.PageSize);
        }

        public async Task<PaginatedResult<UserDto>> GetListUser(GetListUserQuery query)
        {
            var userSpec = new UserSpecification(query, isPaging: true);

            var countSpec = new UserSpecification(query);

            var userList = await _unitOfWork.Repository<AppUser>().ListAsync(userSpec);
            var count = await _unitOfWork.Repository<AppUser>().CountAsync(countSpec);
            var listDto = new List<UserDto>();
            foreach (var user in userList)
            {
                var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = userRoles;
                listDto.Add(userDto);
            }

            return new PaginatedResult<UserDto>(listDto, query.PageIndex, count, query.PageSize);
        }

        public async Task<StaffDto> GetStaff(GetStaffQuery query)
        {
            var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(new StaffSpecification(query.StaffId))
                ?? throw new NotFoundException("Cannot find this staff");

            var staffRoles = (await _userManager.GetRolesAsync(staff.User)).ToList();
            var staffDto = _mapper.Map<StaffDto>(staff);
            staffDto.User.Roles = staffRoles;

            return staffDto;
        }

        public async Task<UserDto> GetUser(GetUserQuery query)
        {
            var user = await _userManager.FindByIdAsync(query.UserId)
                ?? throw new NotFoundException("Cannot find this user");
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = userRoles;

            return userDto;
        }

        public async Task<bool> ToggleUserStatus(ToggleUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId)
                ?? throw new NotFoundException("Cannot find this user");
            user.Status = user.Status == 1 ? 0 : 1;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = _currentUserService.UserId;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
                throw new Exception("Cannot toggle status of user");

            return true;
        }

        public async Task<bool> UpdateStaff(UpdateStaffCommand command)
        {
            var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(new StaffSpecification(command.Id))
                ?? throw new NotFoundException("Cannot find this staff");

            await UpdateProperty(command, staff.User);
            staff.Type = command.Type;
            staff.Code = command.Code;
            staff.UpdatedAt = DateTime.Now;
            staff.UpdatedBy = _currentUserService.UserId;
            if (!string.IsNullOrEmpty(command.Password))
            {
                staff.User.PasswordHash = _userManager.PasswordHasher.HashPassword(staff.User, command.Password);
            }
            _unitOfWork.Repository<Staff>().Update(staff);
            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
                throw new Exception("Cannot update staff information");

            return true;
        }

        private async Task UpdateProperty(UpdateUserRequest request, AppUser user)
        {
            var url = user.Avatar;
            _mapper.Map(request, user);
            if (request.Avatar != null)
            {
                var imageUrl = user.Avatar;
                user.Avatar = await _uploadService.UploadFile(request.Avatar);
                await _uploadService.DeleteFile(imageUrl);
            }
            else
            {
                user.Avatar = url;
            }
        }

        public async Task<bool> UpdateUser(UpdateUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId)
                ?? throw new NotFoundException("Cannot find this user");
            await UpdateProperty(command, user);

            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = _currentUserService.UserId;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
                throw new ValidationException("Cannot update of user");

            return true;
        }

        public async Task<bool> ToggleStaffStatus(ToggleStaffCommand command)
        {
            var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(new StaffSpecification(command.StaffId))
                ?? throw new InvalidRequestException("Unexpected staffId");

            await ToggleUserStatus(new ToggleUserCommand() { UserId = staff.UserId });
            staff.UpdatedAt = DateTime.Now;
            staff.UpdatedBy = _currentUserService.UserId;
            _unitOfWork.Repository<Staff>().Update(staff);
            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess)
                throw new Exception("Cannot handle to toggle status of staff, an error has occured");

            return true;
        }

        public async Task<bool> DisableListStaffStatus(DisableListStaffCommand command)
        {
            List<string> userIds = new();
            foreach (var staffId in command.StaffIds)
            {
                var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(new StaffSpecification(staffId))
                    ?? throw new InvalidRequestException("Unexpected staffId");
                userIds.Add(staff.UserId);
            }

            await DisableListUserStatus(new DisableListUserCommand() { UserIds = userIds });

            foreach (var staffId in command.StaffIds)
            {
                var staff = await _unitOfWork.Repository<Staff>().GetEntityWithSpec(new StaffSpecification(staffId))
                    ?? throw new InvalidRequestException("Unexpected staffId");
                staff.UpdatedAt = DateTime.Now;
                staff.UpdatedBy = _currentUserService.UserId;
                _unitOfWork.Repository<Staff>().Update(staff);
            }

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess)
                throw new Exception("Cannot handle to toggle status of staff, an error has occured");

            return true;
        }
    }
}