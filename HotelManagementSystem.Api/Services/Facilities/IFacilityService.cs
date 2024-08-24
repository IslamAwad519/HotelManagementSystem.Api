using HotelManagementSystem.Api.DTOs.Facilities;

namespace HotelManagementSystem.Api.Services.Facilities;

public interface IFacilityService
{
    Task<IEnumerable<FacilityDto>> GetAllFacilities();
    Task<FacilityDto?> GetFacility(int facilityId);
    Task<FacilityDto> AddFacility(CreateUpdateFacilityDto facilityDto);
    Task<FacilityDto> UpdateFacility(int facilityId, CreateUpdateFacilityDto createUpdateFacilityDto);
    Task<bool> DeleteFacility(int facilityId);
}
