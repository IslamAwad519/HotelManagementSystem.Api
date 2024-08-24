using HotelManagementSystem.Api.DTOs.Rooms;

namespace HotelManagementSystem.Api.Services.Rooms;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllRooms();
    Task<List<RoomDto>> GetAvailableRooms();
    Task<RoomDto?> GetRoom(int roomId);
    Task<RoomDto> AddRoom(CreateUpdateRoomDto roomDto,List<IFormFile> images);
    Task<RoomDto> UpdateRoom(int roomId, CreateUpdateRoomDto createUpdateRoomDto);
    Task<bool> DeleteRoom(int roomId);
}
