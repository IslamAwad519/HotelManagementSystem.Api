using AutoMapper;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;

namespace HotelManagementSystem.Api.Services.Reservations;

public class ReservationService
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IMapper _mapper;
    public ReservationService(
        IRepository<Reservation> reservationRepository, 
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }




    private async Task<bool> IsRoomBooked(int roomId, DateTime checkIn, DateTime checkOut)
    {
        return  await _reservationRepository
            .AnyAsync(b => b.RoomId == roomId &&
             (
                 (checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
                 (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
                 (checkIn <= b.CheckIn && checkOut >= b.CheckOut)
            ));
    }
}
