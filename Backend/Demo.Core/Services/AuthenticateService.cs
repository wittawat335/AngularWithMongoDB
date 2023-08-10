using Demo.Core.Interfaces;
using Demo.Domain.DTOs.User;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAppSettings _appConfig;

        public AuthenticateService(UserManager<User> userManager, RoleManager<Role> roleManager, IAppSettings appConfig)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appConfig = appConfig;
        }

        public async Task<ResponseStatus> CreateRoleAsync(CreateRoleRequest request)
        {
            var response = new ResponseStatus();
            try
            {
                var appRole = new Role
                {
                    RoleCode = request.RoleCode,
                    Name = request.RoleName,
                    IsActive = request.IsActive
                };
                var createRole = await _roleManager.CreateAsync(appRole);

                response.Message = "role created succesfully";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<Response<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var response = new Response<LoginResponse>();
            var loginResponse = new LoginResponse();
            try
            {
                //var email = request.Email + "@example.com";
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid email";
                }
                else
                {
                    var verigyResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                    if (verigyResult == PasswordVerificationResult.Failed)
                    {
                        //handle invalid login credentials...
                        response.IsSuccess = false;
                        response.Message = "Invalid password";
                    }
                    else
                    {
                        //handle success login...
                        //all is well if ew reach here
                        var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };
                        var roles = await _userManager.GetRolesAsync(user);
                        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
                        claims.AddRange(roleClaims);

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var expires = DateTime.Now.AddMinutes(30);

                        var token = new JwtSecurityToken(
                            issuer: _appConfig.Client_URL,
                            audience: _appConfig.Client_URL,
                            claims: claims,
                            expires: expires,
                            signingCredentials: creds

                            );
                        loginResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        loginResponse.UserId = user.Id.ToString();
                        loginResponse.Email = user.Email;
                        loginResponse.fullName = user.FullName;
                        loginResponse.roleName = roles.FirstOrDefault();


                        response.Value = loginResponse;
                        response.IsSuccess = true;
                        response.Message = "Login Successful";
                        response.Url = "/Product/Index";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<ResponseStatus> RegisterAsync(RegisterRequest request)
        {
            var response = new ResponseStatus();
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists != null)
                {
                    response.IsSuccess = false;
                    response.Message = "user already exists";
                }
                else
                {
                    userExists = new User
                    {
                        FullName = request.FullName,
                        Email = request.Email,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        UserName = request.Username,
                        RoleCode = request.RoleCode
                    };
                    var createUserResult = await _userManager.CreateAsync(userExists, request.Password);
                    if (!createUserResult.Succeeded)
                    {
                        response.Message = $"Create user failed {createUserResult?.Errors?.First()?.Description}";
                        response.IsSuccess = false;
                    }
                    else
                    {
                        //user is created...
                        //then add user to a role...
                        var addUserToRoleResult = await _userManager.AddToRoleAsync(userExists, request.RoleName);
                        if (!addUserToRoleResult.Succeeded)
                        {
                            response.Message = $"Create user succeeded but could not add user to role {addUserToRoleResult?.Errors?.First()?.Description}";
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.Message = "User registered successfully";
                            response.IsSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
