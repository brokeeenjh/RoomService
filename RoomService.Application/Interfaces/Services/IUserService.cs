using RoomService.Domain.Entities;

namespace RoomService.Application.Interfaces.Services;

public interface IUserService
{
    public Task RegisterUserAsync(string email, string name, string password);
    public Task LoginUserAsync(string email, string password);
}