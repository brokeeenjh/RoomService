using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Command;
using RoomService.Application.DTO_s;
using RoomService.Application.Query;

namespace RoomService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ISender _mediator;
    
    public BookingController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id")]

    public async Task<ActionResult<BookingDTO>> GetRoomId(Guid id)
    {
        var book = await _mediator.Send(new GetBookingQuery(id));
        
        return Ok(book);
    }

    [HttpGet("bookByUser")]

    public async Task<ActionResult<BookingDTO>> GetBookingsByUser(Guid  userId)
    {
        var book = await _mediator.Send(new GetBookByUserQuery(userId));
        
        return Ok(book);
    }

    [HttpGet("bookByRoom")]

    public async Task<ActionResult<BookingDTO>> GetBookingsByRoom(Guid roomId)
    {
        var book = await _mediator.Send(new GetBookByRoomQuery(roomId));
        
        return Ok(book);
    }

    [HttpPost("createbook")]

    public async Task<ActionResult> CreateBook(CreateBookingDTO createBookingDTO)
    {
        await _mediator.Send(new CreateBookCommand(createBookingDTO.roomId, createBookingDTO.userId,
            createBookingDTO.Email, createBookingDTO.startDate, createBookingDTO.endDate));
        return Ok();
    }

    [HttpPut("updatebook")]

    public async Task<ActionResult> UpdateBook(UpdateBookingDTO updateBookingDTO)
    {
        await _mediator.Send(new UpdateBookingCommand(updateBookingDTO.bookingId, updateBookingDTO.roomId,
            updateBookingDTO.startDate, updateBookingDTO.endDate));
        
        return Ok();
    }

    [HttpDelete("deletebook")]

    public async Task<ActionResult> DeleteBook(Guid id)
    {
        await _mediator.Send(new DeleteBookCommand(id));
        
        return Ok();
    } 
}