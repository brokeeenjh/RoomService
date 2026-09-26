using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Command;
using RoomService.Application.DTO_s;
using RoomService.Application.Query;

namespace RoomService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly ISender _mediator;
    
    public  RoomController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id")]

    public async Task<ActionResult<RoomDTO>> GetRoomById(Guid id)
    {
        var room = await _mediator.Send(new GetRoomQuery(id));

        return Ok(room);
    }

    [HttpGet("userId")]

    public async Task<ActionResult<List<RoomDTO>>> GetRoomsByUserId(Guid userId)
    {
        var room = await _mediator.Send(new GetUserRoomQuery(userId));
        
        return Ok(room);
    }

    [HttpGet("allRooms")]
    public async Task<ActionResult<IEnumerable<RoomDTO>>> GetAllRooms()
    {
        var rooms = await _mediator.Send(new GetAllRoomsQuery());
        return Ok(rooms);
    }

    [HttpGet("getAllFreeRooms")]

    public async Task<ActionResult<List<RoomDTO>>> GetAllFreeRooms()
    {
        var rooms = await _mediator.Send(new GetFreeRoomsQuery());
        return Ok(rooms);
    }

    [HttpDelete("roomId")]

    public async Task<ActionResult> DeleteRoom(Guid id)
    {
        await _mediator.Send(new DeleteRoomCommand(id));
        return Ok();
    }

    [HttpPut("updateRoom")]

    public async Task<ActionResult> UpdateRoom(UpdateRoomDTO updateRoomDTO)
    {
        await _mediator.Send(new UpdateRoomCommand(updateRoomDTO.userId, updateRoomDTO.roomId, updateRoomDTO.bookId));

        return Ok();
    }
}