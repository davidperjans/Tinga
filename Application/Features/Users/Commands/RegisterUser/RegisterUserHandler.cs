using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public RegisterUserHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
                return OperationResult<string>.Failure("Passwords do not match");

            var newUser = _mapper.Map<User>(request);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            newUser.PasswordHash = hashedPassword;

            var result = await _authRepository.RegisterUserAsync(newUser);
            return result;
        }
    }
}
