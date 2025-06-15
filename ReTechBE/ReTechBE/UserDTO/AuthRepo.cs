using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReTechApi.Data;
using ReTechApi.Models;
using ReTechBE.DTO;
using ReTechBE.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthRepo : IAuthRepo
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext context;

    public AuthRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration _configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        this._configuration = _configuration;
        this.context = context;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            UserType = model.UserType,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        var roleName = model.UserType.ToString();
        await _userManager.AddToRoleAsync(user, roleName);

        var authResponse = await GenerateTokenAsync(user);
        authResponse.UserId = user.Id;
        authResponse.Username = user.UserName;

        return authResponse;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return new AuthResponseDto
            {
                IsAuthenticated = false,
                Errors = new List<string> { "Email or password is incorrect" }
            };
        }

        var authResponse = await GenerateTokenAsync(user);
        authResponse.UserId = user.Id;
        authResponse.Username = user.UserName;

        return authResponse;
    }

    private async Task<AuthResponseDto> GenerateTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
         {
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
             new Claim(ClaimTypes.NameIdentifier, user.Id),
             new Claim(ClaimTypes.Name, user.UserName)
         };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryInMinutes"])),
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            Roles = roles.ToList()
        };
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Role = roles.FirstOrDefault(),
            Points = user.Points
        };
    }
    public async Task<bool> DeductPointsAsync(string userId, int pointsToDeduct)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.Points < pointsToDeduct)
            return false;

        user.Points -= pointsToDeduct;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}