
using CompanyDetailsWebAPI.Models;

namespace CompanyDetailsWebAPI.Repository.Interface
{
    public interface IUserRegRepository
    {

        Task<int> AddNewUser(UserRegModel userRegModel);
        //  Task<int> AddNewUser(UserRegModel userRegModel, MailSender mailSender);
        Task<int> UpdateUser(UserRegModel userRegModel);
        Task<int> DeleteUser(int Id); 
        Task<BaseResponseModel> GetAllUsers(int pageno, int pagesize,  string? Textsearch);
        Task<BaseResponseModel> GetAllUsersClaimList(int pageno, int pagesize,  string? Textsearch, int SerarchByLeadStatusId, int SerarchByLeadSourceId);
    }
}
