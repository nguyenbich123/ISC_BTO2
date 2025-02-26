using AuthorizationAPI.DTOs.Request;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ApiResponse<ICollection<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return new ApiResponse<ICollection<User>>("success", "Lấy người dùng thành công", users);
        }


        public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new ApiResponse<User?>("error", "Không tìm thấy người dùng",null);
            }

            return new ApiResponse<User?>( "success",  "Lấy người dùng thành công",  user);
        }

        public async Task<ApiResponse<User>> AddUserAsync(UserRequest request)
        {
            try
            {
                var newUser = new User
                {
                    UserName = request.UserName,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    RoleId = request.RoleId
                };
                var user = await _userRepository.AddUserAsync(newUser);
                return new ApiResponse<User>("success",  "Thêm người dùng thành công",user);
            }
            catch (Exception ex) 
            {
                return new ApiResponse<User>( "error","Lỗi thêm người dùng",null);
            }
        }

        public async Task<ApiResponse<User>> UpdateUserAsync(int id, UserRequest request)
        {
            try
            {
                var existUser = await _userRepository.GetUserByIdAsync(id);
                if (existUser == null)
                {
                    return new ApiResponse<User>("error", "Không tìm thấy người dùng", null);
                }

                if (!string.IsNullOrEmpty(request.Password))
                {
                    request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
                    existUser.UserName = request.UserName;
                }

                existUser.FullName = request.FullName;
                existUser.DateOfBirth = request.DateOfBirth;
                existUser.Email = request.Email;
                existUser.RoleId = request.RoleId;
                var updateUser = await _userRepository.UpdateUserAsync(existUser);
                   
                return new ApiResponse<User>("success", "Cập nhật người dùng thành công", updateUser);
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>("error", "Lỗi cập nhật người dùng", null);
            }  
        }

        public async Task<ApiResponse<User>> DeleteUserAsync(int id)
        {
            try{
                var existUser = await _userRepository.GetUserByIdAsync(id);
            if (existUser == null)
            {
                return new ApiResponse<User>("error", "Không tìm thấy người dùng", null);
            }
            await _userRepository.DeleteUserAsync(id);
            return new ApiResponse<User>("success", "Xóa người dùng thành công", null);
            }catch (Exception ex)
            {
                return new ApiResponse<User>("error", "Lỗi xóa người dùng", null);
            }
            
        }

        public async Task<string?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("RoleId", user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
