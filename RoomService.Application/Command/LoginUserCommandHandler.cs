using MediatR;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record LoginUserCommand(string email, string password) : IRequest<string>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserService  _userService;

    public LoginUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var token = await _userService.LoginUserAsync(request.email, request.password);
        
        return token;
    }
}