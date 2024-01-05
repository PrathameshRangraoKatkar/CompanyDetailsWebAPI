using CompanyDetailsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyDetailsWebAPI.Repository.Interface
{
    public interface IEmployeeRegRepository
    {
        Task<int> AddnewEmployee(EmployeeAddupdateModel employeeRegModel);
        Task<int> UpdateEmployee(EmployeeAddupdateModel employeeRegModel);
        Task<int> DeleteEmployee(int Id);
        Task<BaseResponseModel> GetAllEmployee(int pageno, int pagesize, DateTime? fromDate, DateTime? toDate, string? Textsearch);
        Task<EmployeeRegModel> GetEmployeeById(int Id);


        public Task<EmployeeRegModel> Login(LoginModel loginModel);


        Task<EmployeeRegModel> ChangePassword(ChangepasswordModel obj);

    }

}
