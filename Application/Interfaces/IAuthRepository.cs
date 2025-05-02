using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.Users.Commands.RegisterUser;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<OperationResult<string>> AuthenticateUser(string username, string password);
        Task<OperationResult<string>> RegisterUserAsync(User user);
    }
}
