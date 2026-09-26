using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Command;

namespace RoomService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;
    
    public  UserController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]

    public async Task<ActionResult> Register(string Email, string name, string password)
    {
        await _mediator.Send(new RegisterUserCommand(Email, name, password));
        return Ok();
    }

    [HttpPost("login")]

    public async Task<ActionResult<string>> Login(string email, string password)
    {
        var token = await _mediator.Send(new LoginUserCommand(email, password));
        
        HttpContext.Response.Cookies.Append("tasty-cookie", token);

        return Ok(token);
    }

}