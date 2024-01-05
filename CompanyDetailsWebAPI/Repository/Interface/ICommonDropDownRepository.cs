using CompanyDetailsWebAPI.Models;
using static CompanyDetailsWebAPI.Models.CommonDropDownModel;

namespace CompanyDetailsWebAPI.Repository.Interface
{
    public interface ICommonDropDownRepository
    {
        Task<List<StateModel>> GetAllState();
        Task<List<StateModel>> GetAllDistrict(int StateId);
        Task<List<StateModel>> GetAllTaluka(int StateId, int DistrictId);


        Task<List<LeadStatusModel>> GetAllLeadStatus();
        Task<List<LeadSourceModel>> GetAllLeadSource();
        Task<List<OccupationModel>> GetAllOccupations();



        Task<List<CommOrgModel>> GetAllOrganization();
        Task<List<UnitModel>> GetAllUnits();


    }
}
