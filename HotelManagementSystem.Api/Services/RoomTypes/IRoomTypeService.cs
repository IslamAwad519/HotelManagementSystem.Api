using HotelManagementSystem.Api.DTOs.RoomTypes;

namespace HotelManagementSystem.Api.Services.RoomTypes;

public interface IRoomTypeService
{
    Task<IEnumerable<RoomTypeDto>> GetAllRoomTypes();
    Task<RoomTypeDto?> GetRoomType(int roomTypeId);
    Task<RoomTypeDto> AddRoomType(CreateUpdateRoomTypeDto roomTypeDto);
    Task<RoomTypeDto> UpdateRoomType(int roomTypeId, CreateUpdateRoomTypeDto createUpdateRoomTypeDto);
    Task<bool> DeleteRoomType(int roomTypeId);
}
