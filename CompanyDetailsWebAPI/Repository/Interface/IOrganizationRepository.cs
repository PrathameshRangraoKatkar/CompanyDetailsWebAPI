using CompanyDetailsWebAPI.Models;
using static CompanyDetailsWebAPI.Models.CommonDropDownModel;

namespace CompanyDetailsWebAPI.Repository.Interface
{
    public interface IOrganizationRepository
    {
        Task<int> AddOrganization(OrganizationAddUpdateModel organizationAddUpdateModel);
        //  Task<int> AddNewUser(UserRegModel userRegModel, MailSender mailSender);
        Task<int> EditOrganization(OrganizationAddUpdateModel organizationAddUpdateModel);
        Task<int> BlockOrganization(int Id);

        Task<BaseResponseModel> GetAllOrganization(int pageno, int pagesize, string? Textsearch);
        Task<IEnumerable<OrganizationAddUpdateModel>> GetOrganization();




       
    }



}
