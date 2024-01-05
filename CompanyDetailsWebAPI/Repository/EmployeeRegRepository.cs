using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Net;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace CompanyDetailsWebAPI.Repository
{
    public class EmployeeRegRepository : IEmployeeRegRepository
    {
        private readonly DapperContext _dapperContext;

        public EmployeeRegRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;

        }


        public async Task<int> AddnewEmployee(EmployeeAddupdateModel employeeRegModel)
        {

            //      Id EmployeeName    Email MobileNumber    DateOfBirth GenderId    Password  
            // Address StateId DistId  City TalukaId    CreatedBy IsDeleted   CreatedDate ModifiedBy  ModifieDate


            int result = 0;

            var query = @"INSERT INTO tbl_EmployeeReg            
                  (EmployeeName, Email, MobileNumber, DateOfBirth, GenderId, Password, Address, StateId, DistId, City, TalukaId, CreatedBy, IsDeleted, CreatedDate)
                  VALUES(@EmployeeName, @Email, @MobileNumber, @DateOfBirth, @GenderId, @Password, @Address, @StateId, @DistId, @City, @TalukaId, @CreatedBy, 0, @CreatedDate);
                  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _dapperContext.CreateConnection())
            {
                var existingEmployeeQuery = @"SELECT * FROM tbl_EmployeeReg WHERE Email = @Email AND MobileNumber = @MobileNumber";
                var existingEmployee = await connection.QueryAsync<EmployeeAddupdateModel>(existingEmployeeQuery, employeeRegModel);

                if (existingEmployee.Count() == 0)
                {
                    result = await connection.QuerySingleAsync<int>(query, employeeRegModel);
                }
                else
                {
                    result = -1;
                }

                return result;
            }

        }

        public async Task<int> UpdateEmployee(EmployeeAddupdateModel employeeRegModel)
        {
            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    var existingEmailEmployee = await connection.QuerySingleOrDefaultAsync<EmployeeAddupdateModel>(
                        "SELECT Email FROM tbl_EmployeeReg WHERE Email = @Email AND Id <> @Id",
                        new { Email = employeeRegModel.Email, Id = employeeRegModel.Id });

                    var existingMobileNumberEmployee = await connection.QuerySingleOrDefaultAsync<EmployeeAddupdateModel>(
                        "SELECT MobileNumber FROM tbl_EmployeeReg WHERE MobileNumber = @MobileNumber AND Id <> @Id",
                        new { MobileNumber = employeeRegModel.MobileNumber, Id = employeeRegModel.Id });

                    if (existingEmailEmployee != null)
                    {
                        return -2; // Email already exists for another employee
                    }
                    else if (existingMobileNumberEmployee != null)
                    {
                        return -3; // Mobile number already exists for another employee
                    }

                    var updatequery = @"UPDATE tbl_EmployeeReg
                SET EmployeeName = @EmployeeName,
                    Email = @Email,
                    MobileNumber = @MobileNumber,
                    DateOfBirth = @DateOfBirth,
                    GenderId = @GenderId,
                    Password = @Password,
                    Address = @Address,
                    StateId = @StateId,
                    DistId = @DistId,
                    City = @City,
                    TalukaId = @TalukaId, 
                    IsDeleted = @IsDeleted,
                    ModifiedBy = @ModifiedBy,
                    ModifieDate = @ModifieDate
                WHERE Id = @Id";

                    return await connection.ExecuteAsync(updatequery, new
                    {
                        Id = employeeRegModel.Id,
                        EmployeeName = employeeRegModel.EmployeeName,
                        Email = employeeRegModel.Email,
                        MobileNumber = employeeRegModel.MobileNumber,
                        DateOfBirth = employeeRegModel.DateOfBirth,
                        GenderId = employeeRegModel.GenderId,
                        Password = employeeRegModel.Password,
                        Address = employeeRegModel.Address,
                        StateId = employeeRegModel.StateId,
                        DistId = employeeRegModel.DistId,
                        City = employeeRegModel.City,
                        TalukaId = employeeRegModel.TalukaId,
                        IsDeleted = employeeRegModel.IsDeleted,
                        ModifiedBy = employeeRegModel.ModifiedBy,
                        ModifieDate = employeeRegModel.ModifieDate
                    });
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }





        public async Task<int> DeleteEmployee(int Id)
        {
            int result = 0;

            var query = @"update tbl_EmployeeReg set Isdeleted =1 WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, new { Id });

                return result;
            }
        }
        public async Task<BaseResponseModel> GetAllEmployee(int pageno, int pagesize, DateTime? fromDate, DateTime? toDate, string? Textsearch)
        {
            PaginationModel pagination1 = new PaginationModel();

            BaseResponseModel baseResponseModel = new BaseResponseModel();

            if (pageno == 0)
            {
                pageno = 1;
            }
            if (pagesize == 0)
            {
                pagesize = 10;
            }
            if (String.IsNullOrEmpty(Textsearch)) 
            {
                Textsearch = "";
            }

            int OffsetVal = (pageno - 1) * pagesize;

            var query = @"
                SELECT Emp.Id, Emp.EmployeeName, Emp.Email, Emp.MobileNumber,
                   Emp.DateOfBirth, Emp.CreatedDate, Emp.GenderId, g.Gender, Emp.Password, Emp.Address, Emp.StateId,
                   s.Name AS State, Emp.DistId, d.Name AS District, Emp.City, Emp.TalukaId, t.Name AS Taluka, Emp.IsDeleted
                    FROM tbl_EmployeeReg Emp
                    LEFT JOIN tbl_Gender g ON Emp.GenderId = g.Id
                    LEFT JOIN tbl_States s ON Emp.StateId = s.Id
                    LEFT JOIN tbl_Districts d ON Emp.DistId = d.Id
                    LEFT JOIN tbl_Taluka t ON Emp.TalukaId = t.Id 
                    WHERE Emp.IsDeleted = 0 
                        AND (EmployeeName LIKE '%' + @Textsearch + '%') 
                        AND (@FromDate is null or CreatedDate >= @FromDate )
                        AND (@ToDate is null or CreatedDate <= @ToDate)
                    ORDER BY Emp.Id DESC

                    OFFSET @OffsetVal ROWS FETCH NEXT @pagesize ROWS ONLY;
                    SELECT @pageno AS PageNo, COUNT(Emp.Id) AS TotalPages

                    FROM tbl_EmployeeReg Emp
                    LEFT JOIN tbl_Gender g ON Emp.GenderId = g.Id
                    LEFT JOIN tbl_States s ON Emp.StateId = s.Id
                    LEFT JOIN tbl_Districts d ON Emp.DistId = d.Id
                    LEFT JOIN tbl_Taluka t ON Emp.TalukaId = t.Id 
                    WHERE Emp.IsDeleted = 0 
                    AND (EmployeeName LIKE '%' + @Textsearch + '%') 
                    AND (@FromDate is null or CreatedDate >= @FromDate )
                    AND (@ToDate is null or CreatedDate <= @ToDate)";
            List<EmployeeRegModel> users = new List<EmployeeRegModel>();
            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryMultipleAsync(query, new
                {
                    pageno = pageno,
                    pagesize = pagesize,
                    Textsearch = Textsearch,
                    fromDate = fromDate,
                    toDate = toDate,
                    OffsetVal = OffsetVal
                });

                var dataList = await result.ReadAsync<EmployeeRegModel>();
                var paginationData = await result.ReadAsync<PaginationModel>();

                users = dataList.ToList();
                pagination1 = paginationData.FirstOrDefault();
                pagination1.PageCount = pagination1.TotalPages;
                int PageCount = 0;
                int last = 0;
                last = pagination1.TotalPages % pagesize;
                PageCount = pagination1.TotalPages / pagesize;
                pagination1.TotalPages = PageCount;
                if (last > 0)
                {
                    pagination1.TotalPages = PageCount + 1;
                }
                baseResponseModel.ResponseData1 = users;
                baseResponseModel.ResponseData2 = pagination1;
                return baseResponseModel;
            }
        }

        public async Task<EmployeeRegModel> GetEmployeeById(int Id)
        {

            using (var connection = _dapperContext.CreateConnection())
            {
                var query = @"SELECT Emp.Id, Emp.EmployeeName, Emp.Email, Emp.MobileNumber,
                     Emp.DateOfBirth, Emp.GenderId, g.Gender, Emp.Password, Emp.Address, Emp.StateId,
                     s.Name as State, Emp.DistId, d.Name as District, Emp.City, Emp.TalukaId, t.Name as Taluka, Emp.IsDeleted
                     FROM tbl_EmployeeReg Emp
                     LEFT JOIN tbl_Gender g ON Emp.GenderId = g.Id
                     LEFT JOIN tbl_States s ON Emp.StateId = s.Id
                     LEFT JOIN tbl_Districts d ON Emp.DistId = d.Id
                     LEFT JOIN tbl_Taluka t ON Emp.TalukaId = t.Id
                     WHERE Emp.Id = @Id AND Emp.IsDeleted = 0";

                var employee = await connection.QueryFirstOrDefaultAsync<EmployeeRegModel>(query, new { Id });
                return employee;
            }

        }
        public async Task<EmployeeRegModel> Login(LoginModel loginModel)
        {
            EmployeeRegModel empResponse = new EmployeeRegModel();
            using (var connection = _dapperContext.CreateConnection())
            {
                var username = await connection.QueryFirstOrDefaultAsync<string>(
                @"SELECT MobileNumber 
                    FROM tbl_EmployeeReg
                    WHERE MobileNumber = @MobileNumber AND IsDeleted = 0;",
                new { MobileNumber = loginModel.MobileNumber });

                var password = await connection.QueryFirstOrDefaultAsync<string>(
                    @"SELECT Password FROM tbl_EmployeeReg WHERE Password = @Password AND IsDeleted = 0",
                    new { Password = loginModel.Password });

                if (username != loginModel.MobileNumber)
                {
                    empResponse.MSG = "Invalid UserName.";
                    return empResponse;
                }
                else if (password != loginModel.Password)
                {
                    empResponse.MSG = "Invalid Password.";
                    return empResponse;
                }
                else
                {
                    empResponse = await connection.QueryFirstOrDefaultAsync<EmployeeRegModel>(
                         @"SELECT  Emp.Id, Emp.EmployeeName, Emp.Email, Emp.MobileNumber,
                               Emp.DateOfBirth, Emp.CreatedDate, Emp.GenderId, g.Gender, Emp.Password, Emp.Address, Emp.StateId,
                               s.Name AS State, Emp.DistId, d.Name AS District, Emp.City, Emp.TalukaId, t.Name AS Taluka, Emp.IsDeleted
                                FROM tbl_EmployeeReg Emp
                                LEFT JOIN tbl_Gender g ON Emp.GenderId = g.Id
                                LEFT JOIN tbl_States s ON Emp.StateId = s.Id
                                LEFT JOIN tbl_Districts d ON Emp.DistId = d.Id
                                LEFT JOIN tbl_Taluka t ON Emp.TalukaId = t.Id 
                         WHERE Emp.MobileNumber = @MobileNumber
                         AND Emp.Password = @Password AND Emp.IsDeleted = 0",

                         new { MobileNumber = loginModel.MobileNumber, Password = loginModel.Password });

                }

                return empResponse;
            }
        }


        public async Task<EmployeeRegModel> ChangePassword(ChangepasswordModel obj)
        {
            int result1 = 0;
            int result2 = 0;
            int result3 = 0;

            using (var connection = _dapperContext.CreateConnection())
            {
                result1 = await connection.QueryFirstOrDefaultAsync<int>(
                    @"select id from tbl_EmployeeReg where id=@Id and IsDeleted=0 ", new { Id = obj.Id });

                if (obj.OldPassword == obj.NewPassword)
                {
                    return null; 
                }
                else if (obj.NewPassword != obj.ConfirmNewPassword)
                {
                    return null; 
                }
                else
                {
                    if (result1 > 0) 
                    {
                        result3 = await connection.QueryFirstOrDefaultAsync<int>(
                            @"select id from tbl_EmployeeReg where id=@Id and IsDeleted=0 
                      and Password=@OldPassword", new { Id = obj.Id, OldPassword = obj.OldPassword });

                        if (result3 > 0)
                        {
                            result2 = await connection.ExecuteAsync(
                                @"update tbl_EmployeeReg set Password=@NewPassword where Id=@Id and Password=@OldPassword",
                                new { Id = obj.Id, OldPassword = obj.OldPassword, NewPassword = obj.NewPassword });

                            if (result2 > 0)
                            {
                                
                                var employeeDetails = await connection.QueryFirstOrDefaultAsync<EmployeeRegModel>(
                                    "SELECT * FROM tbl_EmployeeReg WHERE Id = @Id", new { Id = obj.Id });

                                return employeeDetails;
                            }
                            else
                            {
                                return null; 
                            }
                        }
                        else
                        {
                            return null; 
                        }
                    }
                    else
                    {
                        return null; 
                    }
                }
            }
        }


    }
}
