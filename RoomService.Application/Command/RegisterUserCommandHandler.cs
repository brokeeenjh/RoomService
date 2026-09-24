using MediatR;
using RoomService.Application.Interfaces.Services;

namespace RoomService.Application.Command;

public record RegisterUserCommand(string email, string name, string password) : IRequest<Task>;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Task>
{
    private readonly IUserService _userService;
    public RegisterUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<Task> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.RegisterUserAsync(request.email, request.name, request.password);
        
        return Task.CompletedTask;
    }
}