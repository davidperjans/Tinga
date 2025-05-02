using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, OperationResult<string>>
    {
        private readonly IAuthRepository _authRepository;
        public LoginUserHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public Task<OperationResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = _authRepository.AuthenticateUser(request.Username, request.Password);
            return result;
        }
    }
}
