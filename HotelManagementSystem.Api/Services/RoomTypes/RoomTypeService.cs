using AutoMapper;
using HotelManagementSystem.Api.DTOs.RoomTypes;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services.RoomTypes;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRepository<RoomType> _roomTypeRepository;
    private readonly IMapper _mapper;

    public RoomTypeService(
        IRepository<RoomType> roomTypeRepository, 
        IMapper mapper )
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomTypeDto>> GetAllRoomTypes()
    {
        var roomTypes = await  _roomTypeRepository.GetAll().ToListAsync();
        return  _mapper.Map<IEnumerable<RoomTypeDto>>(roomTypes);
    }

    public async Task<RoomTypeDto?> GetRoomType(int roomTypeId)
    {
        var roomType = await _roomTypeRepository.GetById(roomTypeId);
        return _mapper.Map<RoomTypeDto>(roomType);
    }

    public async Task<RoomTypeDto> AddRoomType(CreateUpdateRoomTypeDto createRoomTypeDto)
    {
        var roomType = _mapper.Map<RoomType>(createRoomTypeDto);
       await _roomTypeRepository.AddAsync(roomType);

       return _mapper.Map<RoomTypeDto>(roomType);
    }

    public async Task<RoomTypeDto> UpdateRoomType(int roomTypeId, CreateUpdateRoomTypeDto createUpdateRoomTypeDto)
    {
        var roomType = await _roomTypeRepository.GetById(roomTypeId);
        if (roomType is null)
        {
            return null;
        }

        _mapper.Map(createUpdateRoomTypeDto, roomType);
       await _roomTypeRepository.Update(roomType);
       return _mapper.Map<RoomTypeDto>(roomType);
    }

    public async Task<bool> DeleteRoomType(int roomTypeId)
    {
        var roomType = await _roomTypeRepository.GetById(roomTypeId);
        if (roomType is null)
        {
            return false;
        }
        await _roomTypeRepository.Delete(roomType);
        return true;
    }
}
