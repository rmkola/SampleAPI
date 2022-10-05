﻿using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.DTOs.User;
using SampleAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace SampleAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            UserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };

            //throw new UserCreateFailedException();
        }
    }
}
