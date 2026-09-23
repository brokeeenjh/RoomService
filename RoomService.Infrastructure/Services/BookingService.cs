using Microsoft.AspNetCore.Identity;
using RoomService.Application.DTO_s;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Application.Interfaces.Services;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly UserManager<UserEntity>  _userManager;
    private readonly IRoomRepository _roomRepository;
    
    public BookingService(IBookingRepository bookingRepository, UserManager<UserEntity> userManager, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _userManager = userManager;
        _roomRepository = roomRepository;
    }

    public async Task CreateBookAsync(CreateBookingDTO booking)
    {
        var user = await _userManager.FindByEmailAsync(booking.Email);
        
        if (user == null)
            throw new Exception("User not found");
        
        var room = await _roomRepository.GetRoomByIdAsync(booking.roomId);
        
        if (room == null)
            throw new Exception("Room not found");
        
        if(booking.startDate > booking.endDate)
            throw new Exception("Start date must be before end date");
        
        var result = await _bookingRepository.IsOverlapped(booking.roomId, booking.startDate, booking.endDate);
        
        if (result)
            throw new Exception("Booking already exists");

        var bookingEntity = new BookingEntity()
        {
            Id = Guid.NewGuid(),
            UserEntityId = user.Id,
            RoomEntityId = booking.roomId,
            StartDate = booking.startDate,
            EndDate = booking.endDate,
        };
        await _bookingRepository.CreateBook(bookingEntity);
    }

    public async Task<BookingDTO> GetBookingAsync(Guid bookingId)
    {
        var book = await _bookingRepository.GetBook(bookingId);
        
        if (book == null)
            throw new Exception("Book not found");

        var bookToDTO = new BookingDTO()
        {
            roomId = book.RoomEntityId,
            startDate = book.StartDate,
            endDate = book.EndDate,
            userId = book.UserEntityId,
        };
        return bookToDTO;
    }

    public async Task<BookingDTO> GetBookByRoomAsync(Guid roomId)
    {
        var book = await _bookingRepository.GetBookByRoomId(roomId);
        
        if (book == null)
            
            throw new Exception("Book not found");

        var bookToDTO = new BookingDTO()
        {
            roomId = book.RoomEntityId,
            startDate = book.StartDate,
            endDate = book.EndDate,
            userId = book.UserEntityId,
        };

        return bookToDTO;
    }

    public async Task<BookingDTO> GetBookByUserIdAsync(Guid userId)
    {
        var book = await _bookingRepository.GetBookByUserId(userId);
        
        if (book == null)
            throw new Exception("Book not found");

        var bookToDTO = new BookingDTO()
        {
            roomId = book.RoomEntityId,
            startDate = book.StartDate,
            endDate = book.EndDate,
            userId = book.UserEntityId,
        };
        return bookToDTO;
    }

    public async Task UpdateBookingAsync(UpdateBookingDTO booking)
    {
        var book = await _bookingRepository.GetBook(booking.bookingId);
        
        if (book == null)
            throw new Exception("Book not found");
        
        var result = await _bookingRepository.IsOverlapped(booking.roomId, booking.startDate, booking.endDate, excludeBookingId:booking.bookingId);

        if (result)
        {
            throw new Exception("Booking already exists");
        }
       
        await _bookingRepository.UpdateBook(booking.bookingId, booking.roomId, booking.userId, booking.startDate, booking.endDate);
    }

    public async Task DeleteBookingAsync(Guid bookingId)
    {
        await _bookingRepository.DeleteBook(bookingId);
    }
}