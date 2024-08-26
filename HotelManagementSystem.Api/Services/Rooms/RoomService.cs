using AutoMapper;
using HotelManagementSystem.Api.DTOs.Rooms;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Image = HotelManagementSystem.Api.Models.Image;

namespace HotelManagementSystem.Api.Services.Rooms;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _roomRepository;
    private readonly IRepository<Image> _imageRepository;
    private readonly IRepository<RoomFacility> _roomFacilityRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RoomService(
        IRepository<Room> roomRepository, 
        IMapper mapper,
        IRepository<Image> imageRepository, 
        IRepository<RoomFacility> roomFacilityRepository, 
        IWebHostEnvironment webHostEnvironment)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
        _roomFacilityRepository = roomFacilityRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<RoomDto>> GetAllRooms()
    {
        var rooms = await _roomRepository.GetAll().ToListAsync();
        return _mapper.Map<List<RoomDto>>(rooms);
    }

    public async Task<List<RoomDto>> GetAvailableRooms()
    {
        var rooms = await _roomRepository.GetAll(r => r.Status && !r.Deleted).ToListAsync();
        return _mapper.Map<List<RoomDto>>(rooms);
    }

    //public async Task<List<RoomDto>> GetAvailableRooms()
    //{
    //   var rooms =  await _roomRepository.GetAll(r => !r.Status).ToListAsync();
    //   return _mapper.Map<List<RoomDto>>(rooms);
    //}

    public async Task<RoomDto?> GetRoom(int roomId)
    {
        var room = await _roomRepository.GetById(roomId);
        return _mapper.Map<RoomDto>(room);
    }

    public async Task<RoomDto?> AddRoom(CreateUpdateRoomDto createRoomDto, List<IFormFile> images)
    {
        try
        {
            var room = _mapper.Map<Room>(createRoomDto);
            //Begin Trans
            await _roomRepository.BeginTransactionAsync();

            await _roomRepository.AddAsync(room);
            await UploadRoomImages(room.Id, images);
            await AddRoomFacilitiesAsync(room.Id, createRoomDto.Facilities);

            //End Trans
            await _roomRepository.CommitTransactionAsync();
            return _mapper.Map<RoomDto>(room);
        }
        catch (Exception e)
        {
            await _roomRepository.RollbackTransactionAsync();

            throw;
        }

        return null;
    }

    public async Task<RoomDto> UpdateRoom(int roomId, CreateUpdateRoomDto createUpdateRoomDto)
    {
       var room = await _roomRepository.GetById(roomId);
       if (room is null)
       {
           return null;
       }
       _mapper.Map(createUpdateRoomDto, room);
       await _roomRepository.Update(room);

       return _mapper.Map<RoomDto>(room);
    }

    public async Task<bool> DeleteRoom(int roomId)
    {
        var room = await _roomRepository.GetById(roomId);
        if (room is null)
        {
            return false;
        }
        await _roomRepository.Delete(room);
        return true;
    }

    //Private
    private async Task UploadRoomImages(int roomId ,IEnumerable<IFormFile> images)
    {
        var imageList = new List<Image>();

        foreach (var formFile in images)
        {
             var filePath = await UploadFileAsync(formFile);
                var image = new Image
                {
                    RoomId = roomId,
                    Name = formFile.FileName,
                    FilePath = filePath,
                };

                imageList.Add(image);
        }
        await _imageRepository.AddRange(imageList);
    }
    private async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        //wwwroot path
        var wwwroot = _webHostEnvironment.WebRootPath;
        var uploadsDir = Path.Combine(wwwroot, "images", "rooms");

        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        var filePath = Path.Combine(uploadsDir, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // relative path
        var imagePath = Path.Combine("images", "rooms", fileName);
        return imagePath.Replace("\\", "/");
    }

    private async Task AddRoomFacilitiesAsync(int roomId, IEnumerable<int> facilitiesId)
    {
        var roomFacilities = facilitiesId.Select(facilityId => new RoomFacility
        {
            RoomId = roomId,
            FacilityId = facilityId

        }).ToList();

        if (roomFacilities.Any())
        {
            await _roomFacilityRepository.AddRange(roomFacilities);
        }
    }
    //high performance
    //private async Task UploadRoomImages(int roomId, IEnumerable<IFormFile> images)
    //{
    //    var imageList = await Task.WhenAll(images.Select(async formFile =>
    //    {
    //        var filePath = await UploadFileAsync(formFile);
    //        return new Image
    //        {
    //            RoomId = roomId,
    //            Name = formFile.FileName,
    //            FilePath = filePath,
    //        };
    //    }));

    //    await _imageRepository.AddRange(imageList.ToList());
    //}

    //private async Task<string> UploadFileAsync(IFormFile file)
    //{
    //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

    //    root path
    //    var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    //    var wwwroot = _webHostEnvironment.WebRootPath;
    //    var uploads = Path.Combine(wwwroot, "images", "rooms");

    //    var uploads = Path.Combine(@"images\rooms");
    //    var rootPath = _webHostEnvironment.WebRootPath;
    //    var fullPath = Path.Combine(rootPath, uploads);

    //    if (!Directory.Exists(uploads))
    //    {
    //        Directory.CreateDirectory(uploads);
    //    }
    //    using (var fileStream = new FileStream(uploads, FileMode.Create))
    //    {
    //        await file.CopyToAsync(fileStream);
    //    }
    //    relative path
    //    var imageFullPath = Path.Combine("images", "rooms", fileName);
    //    return imageFullPath.Replace("\\", "/");
    //}
}
