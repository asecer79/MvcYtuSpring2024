namespace WebAPI.Utils
{
    public class UserResponseModel
    {
        public string? Email { get; set;}
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }

    }
}
