using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.DTOs.User;
using SampleAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace SampleAPI.Application.Features.Commands.AppUser.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
    {
        readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            UserResponse response = await _userService.DeleteAsync(new()
            {
               
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
