using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.Interface.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            AuthenticationResponse response = await _accountService.Authentication(request);
            return response;
        }
        public async Task SignOut()
        {
            await _accountService.SignOut();
        }
        public async Task<RegisterResponse> Register(RegisterRequest request, string origin)
        {
            var response = await _accountService.Register(request, origin);
            return response;
        }
        public async Task UpdateUser(string Id, RegisterRequest request)
        {
            await _accountService.UpdateUser(Id, request);
        }

        public async Task<string> EmailConfirm(string userId, string token)
        {
            return await _accountService.ConfirmEmail(userId, token);
        }
        public async Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            return await _accountService.ForgotPassword(request, origin);
        }
        public async Task<GenericResponse> ResetPassword(ResetPasswordRequest request)
        {
            return await _accountService.ResetPassword(request);
        }
    }
}
