namespace ReTechBE.UserDTO
{
    public class AuthResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Errors { get; set; } = new();
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
