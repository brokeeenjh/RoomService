using Microsoft.AspNetCore.Identity;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Application.Interfaces.Services;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity>  _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IJwtProvider  _jwtProvider;

    public UserService(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtProvider = jwtProvider;
    }

    public async Task RegisterUserAsync(string email, string name, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user != null)
            throw new Exception("User with the same email already exists");

        var createdUser = new UserEntity
        {
            Email = email,
            UserName = name,
            Id = Guid.NewGuid()
        };
        
        var result = await _userManager.CreateAsync(createdUser, password);

        if (!result.Succeeded)
        {
            throw new Exception("Error creating user");
        }
        
       var roleResult =  await _userManager.AddToRoleAsync(createdUser, "User");

       if (!roleResult.Succeeded)
       {
           throw new Exception("Error creating role");
       }
    }

    public async Task<string> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new Exception("there is no user with this email");

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
            throw new Exception("wrong password");

        var token = _jwtProvider.GenerateToken(user);
        
        return token;
    }
}