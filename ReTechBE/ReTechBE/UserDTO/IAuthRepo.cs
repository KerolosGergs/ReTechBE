using Microsoft.AspNetCore.Identity;
using ReTechBE.DTO;

namespace ReTechBE.UserDTO
{
    public interface IAuthRepo
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto model);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> DeductPointsAsync(string userId, int pointsToDeduct);
    }
}
