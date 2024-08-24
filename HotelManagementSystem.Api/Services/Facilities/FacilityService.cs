using AutoMapper;
using HotelManagementSystem.Api.DTOs.Facilities;
using HotelManagementSystem.Api.Models;
using HotelManagementSystem.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Services.Facilities;

public class FacilityService : IFacilityService
{
    private readonly IRepository<Facility> _facilityRepository;
    private readonly IMapper _mapper;

    public FacilityService(
        IRepository<Facility> facilityRepository, 
        IMapper mapper )
    {
        _facilityRepository = facilityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FacilityDto>> GetAllFacilities()
    {
        var facilities = await  _facilityRepository.GetAll().ToListAsync();
        return  _mapper.Map<IEnumerable<FacilityDto>>(facilities);
    }

    public async Task<FacilityDto?> GetFacility(int facilityId)
    {
        var facility = await _facilityRepository.GetById(facilityId);
        return _mapper.Map<FacilityDto>(facility);
    }

    public async Task<FacilityDto> AddFacility(CreateUpdateFacilityDto createFacilityDto)
    {
        var facility = _mapper.Map<Facility>(createFacilityDto);
       await _facilityRepository.AddAsync(facility);

       return _mapper.Map<FacilityDto>(facility);
    }

    public async Task<FacilityDto> UpdateFacility(int facilityId, CreateUpdateFacilityDto createUpdateFacilityDto)
    {
        var facility = await _facilityRepository.GetById(facilityId);
        if (facility is null)
        {
            return null;
        }

        _mapper.Map(createUpdateFacilityDto, facility);
       await _facilityRepository.Update(facility);
       return _mapper.Map<FacilityDto>(facility);
    }

    public async Task<bool> DeleteFacility(int facilityId)
    {
        var facility = await _facilityRepository.GetById(facilityId);
        if (facility is null)
        {
            return false;
        }
        await _facilityRepository.Delete(facility);
        return true;
    }
}
