using BooksApi.Api.Models;
using System.Threading.Tasks;

namespace BooksApi.Api.Services
{
    public interface IAuthService
    {
        Task<(UserResponseDto? User, string? Error)> RegisterAsync(UserRegisterDto registerDto);
        Task<(string? Token, string? Error)> LoginAsync(UserLoginDto loginDto);
    }
}