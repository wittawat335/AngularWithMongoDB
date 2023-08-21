using AutoMapper;
using Demo.Core.Interfaces;
using Demo.Domain.DTOs.User;
using Demo.Domain.Models;
using Demo.Domain.Models.Collections;
using Demo.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;

        public AuthenticateService(UserManager<User> userManager, RoleManager<Role> roleManager, IAppSettings appConfig, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appConfig = appConfig;
            _mapper = mapper;
        }
        public async Task<Response<UserDTO>> GetByIdAsync(string id)
        {
            var response = new Response<UserDTO>();
            try
            {
                response.Value = _mapper.Map<UserDTO>(await _userManager.FindByIdAsync(id));
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.UpdateComplete;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<Response<RoleDTO>> CreateRoleAsync(CreateRoleRequest request)
        {
            var response = new Response<RoleDTO>();
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
        public Response<List<UserDTO>> GetList()
        {
            var response = new Response<List<UserDTO>>();
            IQueryable<User> tbUser = _userManager.Users.AsQueryable();
            try
            {
                response.Value = _mapper.Map<List<UserDTO>>(tbUser);
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
        public Response<List<RoleDTO>> GetRoleList()
        {
            var response = new Response<List<RoleDTO>>();
            IQueryable<Role> tb = _roleManager.Roles.AsQueryable();
            try
            {
                response.Value = _mapper.Map<List<RoleDTO>>(tb);
                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.GetList;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
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
                var user = await _userManager.FindByNameAsync(request.UserName);
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
        public async Task<Response<UserDTO>> RegisterAsync(RegisterRequest request)
        {
            var response = new Response<UserDTO>();
            try
            {
                var userExists = await _userManager.FindByNameAsync(request.UserName);
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
                        UserName = request.UserName,
                        Role = request.Role,
                        IsActive = true
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
                        var addUserToRoleResult = await _userManager.AddToRoleAsync(userExists, request.Role);
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
        public async Task<Response<UserDTO>> UpdateUser(UserDTO model)
        {
            var response = new Response<UserDTO>();
            var findId = new User();
            try
            {
                findId = await _userManager.FindByIdAsync(model.Id);
                if (findId != null)
                {
                    if (findId.Role != model.Role)
                    {
                        foreach (var item in findId.Roles.ToList())
                        {
                            findId.Roles.Remove(item);
                        }
                        await _userManager.AddToRoleAsync(findId, model.Role);
                    }

                    findId.FullName = model.FullName;
                    findId.UserName = model.UserName;
                    findId.Email = model.Email;
                    findId.Role = model.Role;
                    findId.IsActive = model.IsActive == "A" ? true : false;

                    await _userManager.UpdateAsync(findId);
                    //await _userManager.UpdateAsync(_mapper.Map(model, findId));
                    response.IsSuccess = Constants.StatusData.True;
                    response.Message = Constants.Msg.UpdateComplete;
                }
                else
                    response.Message = "User is Not Found";

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<Response<UserDTO>> DeleteUser(string id)
        {
            var response = new Response<UserDTO>();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    await _userManager.DeleteAsync(user);
                else
                    response.Message = "User is Not Found";

                response.IsSuccess = Constants.StatusData.True;
                response.Message = Constants.Msg.DeleteComplete;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
    }
}
