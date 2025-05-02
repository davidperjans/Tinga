using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.Users.Commands.RegisterUser;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthRepository(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<OperationResult<string>> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return OperationResult<string>.Failure("Invalid credentials");

            var tokenResult = _tokenService.GenerateJwtToken(user);
            return tokenResult;
        }

        public async Task<OperationResult<string>> RegisterUserAsync(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email || u.Username == user.Username))
                return OperationResult<string>.Failure("Username or email already taken");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return OperationResult<string>.Success("User registered successfully");
        }
    }
}
