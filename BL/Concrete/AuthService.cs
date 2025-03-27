﻿using AutoMapper;
using BL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BL.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public AuthService(IConfiguration config, IUserRepository repository, IBcryptService bcrypt)
        {
            _config = config;
            _userRepository = repository;
            _bcryptService = bcrypt;
        }

        private string Generate(User user)
        {
            var key = Environment.GetEnvironmentVariable("JWT_KEY") ??
            throw new ApplicationException("JWT key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Role", user.Role.RoleName),
                new Claim("Permissions", JsonSerializer.Serialize(user.Role.Permissions.Select(x => x.PermissionName), options), JsonClaimValueTypes.JsonArray)
            };

            var token = new JwtSecurityToken(
              _config["JwtSettings:Issuer"],
              _config["JwtSettings:Audience"],
              claims,
              expires: DateTime.UtcNow.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ServiceResult<AuthenticateResponse?> Authenticate(UserLogin userLogin)
        {
            var user = _userRepository.GetByEmail(userLogin.Email);
            if (user == null) { return ServiceResult<AuthenticateResponse?>.NotFound(null); }

            if (_bcryptService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                var token = Generate(user);
                var response = new AuthenticateResponse(user, token);

                return ServiceResult<AuthenticateResponse?>.Ok(response);
            }
            return ServiceResult<AuthenticateResponse?>.BadRequest("Email or password is invalid.");
        }
    }
}
