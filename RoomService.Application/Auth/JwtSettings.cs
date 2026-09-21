namespace RoomService.Application.Auth;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public TimeSpan Expires { get; set; }
}