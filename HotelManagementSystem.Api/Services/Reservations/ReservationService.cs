using AutoMapper;
using HotelManagementSystem.Api.DTOs.Reservations;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services.Reservations;

public class ReservationService :IReservationService
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IRepository<ReservationRoom> _reservationRoomRepository;
    private readonly IRepository<ReservationRoomFacility> _reservationRoomFacilityRepository;
    private readonly IRepository<Facility> _facilityRepository;
    private readonly IRepository<Room> _roomRepository;
    private readonly IMapper _mapper;
    public ReservationService(
        IRepository<Reservation> reservationRepository, 
        IRepository<ReservationRoom> reservationRoomRepository,
        IRepository<ReservationRoomFacility> reservationRoomFacilityRepository, 
        IRepository<Room> roomRepository,
        IRepository<Facility> facilityRepository, 
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _roomRepository = roomRepository;
        _facilityRepository = facilityRepository;
        _reservationRoomRepository = reservationRoomRepository;
        _reservationRoomFacilityRepository = reservationRoomFacilityRepository;
    }


    public async Task<List<RoomDto>> GetAvailableRooms()
    {
        var today = DateTime.Today;

        //get all rooms
        var allrooms =  _roomRepository.GetAll().ToList();

        //get reservation ids for for cuurent reservations
        var currentReservationsId = _reservationRepository.GetAll(e => e.CheckOut > today).Select(e=>e.Id).ToList();

        //get reserved rooms 
        var reservedRoomIds = _reservationRoomRepository.GetAll(sr => currentReservationsId.Contains(sr.ReservationId)).Select(e=>e.RoomId).ToList();

        var availableRooms = allrooms.Where(r => !reservedRoomIds.Contains(r.Id)).ToList();

        return _mapper.Map<List<RoomDto>>(availableRooms);

    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservation()
    {
        var reservations = await _reservationRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto?> GetReservation(int reservationId)
    {
            var facility = await _reservationRepository.GetById(reservationId);
            return _mapper.Map<ReservationDto>(facility);
    }

    public async Task<ReservationDto?> AddReservation(CreateReservationDto createReservationDto)
    {
        var reservation = _mapper.Map<Reservation>(createReservationDto);
        var rooms = createReservationDto.RoomFacilities.Keys.ToList();
        var isBooked = await IsBookedRoom(rooms, createReservationDto.CheckIn, createReservationDto.CheckOut);

        if (!isBooked)
        { 
            try
            {
                //Begin Trans
                await _reservationRepository.BeginTransactionAsync();

                await _reservationRepository.AddAsync(reservation);
             
                await AddReservationRoomFacilities(reservation.Id, createReservationDto.RoomFacilities);

                //End Trans
                await _reservationRepository.CommitTransactionAsync();

                //update reservation total amount
                await UpdateReservationTotalAmount(reservation.Id);
                return _mapper.Map<ReservationDto>(reservation);
            }
            catch (Exception e)
            {
                await _reservationRepository.RollbackTransactionAsync();
                throw;
            }
        }

        return null;
    }
   
    
    //PRIVATE
    private async Task<bool> IsBookedRoom(List<int> roomIds, DateTime checkIn, DateTime checkOut)
    {
        var reservationRooms = await _reservationRoomRepository.GetAll(e =>
                roomIds.Contains(e.RoomId) &&
                (
                    (checkIn >= e.Reservation.CheckIn && checkIn < e.Reservation.CheckOut) || // Check-in falls within an existing reservation
                    (checkOut > e.Reservation.CheckIn && checkOut <= e.Reservation.CheckOut) || // Check-out falls within an existing reservation
                    (checkIn <= e.Reservation.CheckIn && checkOut >= e.Reservation.CheckOut) // New reservation envelops an existing reservation
                )).ToListAsync();


        return reservationRooms.Any();
    }

    //private async Task AddReservationRoomFacilities(int reservationId, IEnumerable<int> facilitiesId)
    //{
    //    var reservationFacilities = facilitiesId.Select(roomFacilityId => new ReservationRoomFacility()
    //    {
    //        ReservationId = reservationId,
    //        RoomId = room
    //        FacilityId = facilityId

    //    }).ToList();

    //    await _reservationFacilityRepository.AddRange(reservationFacilities);
    //}
    private async Task AddReservationRoomFacilities(int reservationId, Dictionary<int, IEnumerable<int>> roomFacilitiesMap)
    {
        if (roomFacilitiesMap == null)
        {
            throw new ArgumentNullException(nameof(roomFacilitiesMap));
        }

        var reservationRoomFacilities = new List<ReservationRoomFacility>();

        foreach (var roomFacilityPair in roomFacilitiesMap)
        {
            int roomId = roomFacilityPair.Key;
            IEnumerable<int> facilityIds = roomFacilityPair.Value;

            if (facilityIds == null)
            {
                continue; // Skip if facilityIds is null
            }

            foreach (var facilityId in facilityIds)
            {
                reservationRoomFacilities.Add(new ReservationRoomFacility()
                {
                    ReservationId = reservationId,
                    RoomId = roomId,
                    FacilityId = facilityId
                });
            }
        }

        if (_reservationRoomFacilityRepository == null)
        {
            throw new InvalidOperationException("Repository not initialized.");
        }

        await _reservationRoomFacilityRepository.AddRange(reservationRoomFacilities);
    }

    private async Task AddReservationRooms(int reservationId, IEnumerable<int> roomsId)
    {
        var reservationRooms = roomsId.Select(roomId => new ReservationRoom()
        {
            ReservationId = reservationId,
            RoomId = roomId

        }).ToList();

        await _reservationRoomRepository.AddRange(reservationRooms);
    }
    private async Task<decimal> TotalFacilitiesCost(int reservationId)
    {
        var reservationFacilities = await _reservationRoomFacilityRepository.GetAll(r => r.ReservationId == reservationId).ToListAsync();
        var facilityIds = reservationFacilities.Select(rf => rf.FacilityId).ToList();

        var facilities = await _facilityRepository.GetAll(f => facilityIds.Contains(f.Id)).ToListAsync();
        var total = facilities.Sum(f => f.Price);
        return total;
    }
    private async Task<decimal> TotalRoomsCost(int reservationId)
    {
        var reservationRooms = await _reservationRoomFacilityRepository.GetAll(r => r.ReservationId == reservationId).ToListAsync();
        var roomsIds = reservationRooms.Select(rf => rf.RoomId).ToList();

        var rooms = await _roomRepository.GetAll(f => roomsIds.Contains(f.Id)).ToListAsync();
        var total = rooms.Sum(f => f.Price);
        return total;
    }

    private async Task UpdateReservationTotalAmount(int reservationId)
    {
        // Fetch the reservation
        var reservation = await _reservationRepository.GetById(reservationId);

        var totalfacilitiesCost = await TotalFacilitiesCost(reservationId);
        var totalroomsCost = await TotalRoomsCost(reservationId);
        var totalAmount = totalfacilitiesCost + totalroomsCost;

        // Update the reservation's total amount
        reservation.TotalAmount = totalAmount;

        // Save changes to the repository
        await _reservationRepository.Update(reservation);
    }

}
