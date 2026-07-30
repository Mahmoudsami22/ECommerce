using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IIdentityServices identityServices;
        private readonly ITokenServices tokenServices;

        public AuthenticationServices(IIdentityServices identityServices,ITokenServices tokenServices)
        {
            this.identityServices = identityServices;
            this.tokenServices = tokenServices;
        }


        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var UserResult = await identityServices.FindByEmailAsync(loginDto.Email, ct);

            if (!UserResult.IsSuccess)
            {
                return Result<UserDto>.Fail(UserResult.Errors);
            }
            var PasswordCheck = await identityServices.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);

            if (!PasswordCheck.IsSuccess)
            {
                return Result<UserDto>.Fail(Error.UnAuthorized("Invalid Email Or Password"));
            }

            var rolesResult = await identityServices.GetRolesAsync(UserResult.data.Email);
            var token = tokenServices.CreateToken(UserResult.data.Id, UserResult.data.Email, UserResult.data.UserName, rolesResult.data);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = UserResult.data.Email,
                DisplayName = UserResult.data.DisplayName,
                Token = token

            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await identityServices.CreateUser(registerDto, ct);
            if (!result.IsSuccess || result.data is null)
                return Result<UserDto>.Fail(result.Errors);

            var rolesResult = await identityServices.GetRolesAsync(result.data.Email);
            var token = tokenServices.CreateToken(result.data.Id, result.data.Email, result.data.UserName, rolesResult.data);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = result.data.Email,
                DisplayName = result.data.DisplayName,
                Token = token
            });
        }

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default)
        {
            return await identityServices.EmailExistsAsync(email, ct);
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var result = await identityServices.GetAddressByEmailAsync(email, ct);

            if (!result.IsSuccess)
                return Result<AddressDto>.Fail(result.Errors);

            return Result<AddressDto>.Ok(result.data);
        }
        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default)
        {
            return await identityServices.UpdateAddressAsync(email, addressDto, ct);
        }
        public async Task<Result<UserDto>> GetCurrentUser(string email, CancellationToken ct = default)
        {
            var UserResult = await identityServices.FindByEmailAsync(email, ct);

            if (!UserResult.IsSuccess)
                return Result<UserDto>.Fail(UserResult.Errors);

            var user = UserResult.data;

            var roleResult = await identityServices.GetRolesAsync(user.Email);

            if (!roleResult.IsSuccess)
                return Result<UserDto>.Fail(roleResult.Errors);

            var token = tokenServices.CreateToken(user.Id, user.Email, user.UserName, roleResult.data);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });
        }

    }
}
