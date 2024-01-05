
using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Dapper;
using System.Net;

namespace CompanyDetailsWebAPI.Repository
{
    public class UserRegRepository : IUserRegRepository
    {
        private readonly DapperContext _dapperContext;
       
        public UserRegRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
           
        }
        public async Task<int> AddNewUser(UserRegModel userRegModel)
        {
            int result = 0;
            var query = @"insert into tbl_UserReg 
                               (UserName,MobileNumber,PANCard,Email,Address,DateOfBirth,
                               ReferenceName,GenderId,LeadStatusId,LeadSourceId,OccupationId,
                               OccupationName,OccupationDescription,IsDeleted,CreatedBy,CreatedDate,ModifiedBy,ModifieDate)
                        values (@UserName,@MobileNumber,@PANCard,@Email,@Address,@DateOfBirth,
                                @ReferenceName,@GenderId,@LeadStatusId,@LeadSourceId,@OccupationId,
                                @OccupationName,@OccupationDescription,0,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifieDate)
                                SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = _dapperContext.CreateConnection())
            {
                var existingUserQuery = @"SELECT * FROM tbl_UserReg WHERE PANCard = @PANCard AND MobileNumber = @MobileNumber";
                var existingEmployee = await connection.QueryAsync<UserRegModel>(existingUserQuery, userRegModel);

                if (existingEmployee.Count() == 0)
                {
                    result = await connection.QuerySingleAsync<int>(query, userRegModel);
                }
                else
                {
                    result = -1;
                }

                return result;
            }
        }


        //public async Task<int> AddNewUser(UserRegModel userRegModel, MailSender mailSender)
        //{
        //    int result = 0;
        //    var query = @"INSERT INTO tbl_UserReg 
        //               (UserName,MobileNumber,PANCard,Email,Address,DateOfBirth,
        //               ReferenceName,GenderId,LeadStatusId,LeadSourceId,OccupationId,
        //               OccupationName,OccupationDescription,IsDeleted,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
        //        VALUES (@UserName,@MobileNumber,@PANCard,@Email,@Address,@DateOfBirth,
        //                @ReferenceName,@GenderId,@LeadStatusId,@LeadSourceId,@OccupationId,
        //                @OccupationName,@OccupationDescription,0,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)
        //                SELECT CAST(SCOPE_IDENTITY() as int)";

        //    using (var connection = _dapperContext.CreateConnection())
        //    {
        //        var existingUserQuery = @"SELECT * FROM tbl_UserReg WHERE PANCard = @PANCard AND MobileNumber = @MobileNumber";
        //        var existingUser = await connection.QueryAsync<UserRegModel>(existingUserQuery, userRegModel);

        //        if (existingUser.Count() == 0)
        //        {
        //            result = await connection.QuerySingleAsync<int>(query, userRegModel);

        //            if (result > 0)
        //            {
        //                // User registration successful, now send the registration email
        //                var mailModel = new MailModel
        //                {
        //                    Email = userRegModel.Email,
        //                    UserName = userRegModel.UserName,
        //                    Subject = "Registration Successful",
        //                    Body = "Thank you for registering with us!",
        //                    Link = "https://example.com" // Add the appropriate link if needed
        //                };

        //                string emailSendingResult = mailSender.SendUserRegistrationMail(mailModel);

        //                if (emailSendingResult != "Email sent successfully.")
        //                {
        //                    // Handle email sending failure
        //                    // You might want to log the error or take appropriate action
        //                }
        //            }
        //        }
        //        else
        //        {
        //            result = -1;
        //        }

        //        return result;
        //    }
        //}


        public async Task<int> UpdateUser(UserRegModel userRegModel)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var existingPANUser = await connection.QuerySingleOrDefaultAsync<UserRegModel>(
                    "SELECT PANCard FROM tbl_UserReg WHERE PANCard = @PANCard AND Id <> @Id",
                    new { PANCard = userRegModel.PANCard, Id = userRegModel.Id });

                var existingMobileNumberUser = await connection.QuerySingleOrDefaultAsync<UserRegModel>(
                    "SELECT MobileNumber FROM tbl_UserReg WHERE MobileNumber = @MobileNumber AND Id <> @Id",
                    new { MobileNumber = userRegModel.MobileNumber, Id = userRegModel.Id });

                if (existingPANUser != null)
                {
                    return -2; 
                }
                else if (existingMobileNumberUser != null)
                {
                    return -3; 
                }

                //UserName,MobileNumber,PANCard,Email,Address,DateOfBirth,
              //  ReferenceName,GenderId,LeadStatusId,LeadSourceId,OccupationId,
                 //OccupationName,OccupationDescription,IsDeleted,CreatedBy,CreatedDate,ModifiedBy,ModifieDate
                var updatequery = @"UPDATE tbl_UserReg
                SET UserName = @UserName,
                    MobileNumber = @MobileNumber,
                    PANCard = @PANCard,
                    Email = @Email,
                    Address = @Address,
                    DateOfBirth = @DateOfBirth,
                    ReferenceName = @ReferenceName,
                    GenderId = @GenderId,
                    LeadStatusId = @LeadStatusId,
                    LeadSourceId = @LeadSourceId,
                    OccupationId = @OccupationId, 
                    OccupationName = @OccupationName,
                     OccupationDescription = @OccupationDescription,
                     IsDeleted = @IsDeleted,
                    ModifiedBy = @ModifiedBy,
                    ModifieDate = @ModifieDate
                WHERE Id = @Id";

                return await connection.ExecuteAsync(updatequery, new
                {
                    Id = userRegModel.Id,
                    UserName = userRegModel.UserName,
                    MobileNumber = userRegModel.MobileNumber,
                    PANCard = userRegModel.PANCard,
                    Email = userRegModel.Email,
                    Address = userRegModel.Address,
                    DateOfBirth = userRegModel.DateOfBirth,
                    ReferenceName = userRegModel.ReferenceName,
                    GenderId = userRegModel.GenderId,
                    LeadStatusId = userRegModel.LeadStatusId,
                    LeadSourceId = userRegModel.LeadSourceId,
                    OccupationId = userRegModel.OccupationId,
                    OccupationName = userRegModel.OccupationName,
                    OccupationDescription = userRegModel.OccupationDescription,
                    IsDeleted = userRegModel.IsDeleted,
                    ModifiedBy = userRegModel.ModifiedBy,
                    ModifieDate = userRegModel.ModifieDate
                });
            }

        }

        public async  Task<int> DeleteUser(int Id)
        {
            int result = 0;

            var query = @"update tbl_UserReg set Isdeleted =1 WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, new { Id });

                return result;
            }
        }

        public async Task<BaseResponseModel> GetAllUsers(int pageno, int pagesize, string? Textsearch)
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
                       select Usr.Id,Usr.UserName,Usr.MobileNumber,Usr.PANCard,Usr.Email,Usr.Address,Usr.DateOfBirth,Usr.ReferenceName,
		            Usr.GenderId,g.Gender,Usr.LeadStatusId,l.Name as StatusName,Usr.LeadSourceId,ls.Name as SourceName,Usr.OccupationId,Usr.OccupationName,
		            Usr.OccupationDescription,Usr.IsDeleted
		            from tbl_UserReg Usr
		            LEFT JOIN tbl_Gender g ON Usr.GenderId = g.Id
		            LEFT JOIN tbl_Occupation o ON Usr.OccupationId=o.Id
		            LEFT JOIN  tbl_LeadSource ls ON Usr.LeadSourceId=ls.Id
			        LEFT JOIN tbl_LeadStatus l ON Usr.LeadStatusId=l.ID
                    WHERE Usr.IsDeleted = 0 
                        AND (UserName LIKE '%' + @Textsearch + '%') 
                    ORDER BY Usr.Id DESC

                    OFFSET @OffsetVal ROWS FETCH NEXT @pagesize ROWS ONLY;
                    SELECT @pageno AS PageNo, COUNT(Usr.Id) AS TotalPages

                     from tbl_UserReg Usr
		            LEFT JOIN tbl_Gender g ON Usr.GenderId = g.Id
		            LEFT JOIN tbl_Occupation o ON Usr.OccupationId=o.Id
		            LEFT JOIN  tbl_LeadSource ls ON Usr.LeadSourceId=ls.Id
			        LEFT JOIN tbl_LeadStatus l ON Usr.LeadStatusId=l.ID
                    WHERE Usr.IsDeleted = 0 
                        AND (UserName LIKE '%' + @Textsearch + '%')";

            List<UserRegModel> users = new List<UserRegModel>();
            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryMultipleAsync(query, new
                {
                    pageno = pageno,
                    pagesize = pagesize,
                    Textsearch = Textsearch,
                    OffsetVal = OffsetVal
                });


                var dataList = await result.ReadAsync<UserRegModel>();
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

        public async Task<BaseResponseModel> GetAllUsersClaimList(int pageno, int pagesize, string? Textsearch, int SerarchByLeadStatusId, int SerarchByLeadSourceId)
        {
            PaginationModel pagination1 = new PaginationModel();                                         
            BaseResponseModel baseResponseModel = new BaseResponseModel();

            if (pageno <= 0)
            {
                pageno = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            if (Textsearch == null)
            {
                Textsearch = "";
            }

            int OffsetVal = (pageno - 1) * pagesize;

            var query = @"
                        SELECT
                            Usr.Id,
                            Usr.UserName,
                            Usr.MobileNumber,
                            Usr.PANCard,
                            Usr.LeadStatusId,
                            l.Name AS StatusName,
                            Usr.LeadSourceId,
                            ls.Name AS SourceName,
                            Usr.IsDeleted
                        FROM
                            tbl_UserReg Usr
                        LEFT JOIN
                            tbl_LeadSource ls ON Usr.LeadSourceId = ls.Id
                        LEFT JOIN
                            tbl_LeadStatus l ON Usr.LeadStatusId = l.ID
                        WHERE
                    Usr.IsDeleted = 0 
                    AND ((Usr.UserName LIKE '%' + @Textsearch + '%' OR Usr.MobileNumber LIKE '%' + @Textsearch + '%'))
                    AND (Usr.LeadStatusId = @SerarchByLeadStatusId OR @SerarchByLeadStatusId = 0)
                    AND (Usr.LeadSourceId = @SerarchByLeadSourceId OR @SerarchByLeadSourceId = 0)

                        ORDER BY
                            Usr.Id DESC
                        OFFSET @OffsetVal ROWS FETCH NEXT @pagesize ROWS ONLY;

                        SELECT
                            @pageno AS PageNo,
                            COUNT(Usr.Id) AS TotalPages
                        FROM
                            tbl_UserReg Usr
                        LEFT JOIN
                            tbl_LeadSource ls ON Usr.LeadSourceId = ls.Id
                        LEFT JOIN
                            tbl_LeadStatus l ON Usr.LeadStatusId = l.ID
                        WHERE
                        Usr.IsDeleted = 0 
                        AND ((Usr.UserName LIKE '%' + @Textsearch + '%' OR Usr.MobileNumber LIKE '%' + @Textsearch + '%'))
                        AND (Usr.LeadStatusId = @SerarchByLeadStatusId OR @SerarchByLeadStatusId = 0)
                        AND (Usr.LeadSourceId = @SerarchByLeadSourceId OR @SerarchByLeadSourceId = 0)  ";
                  

            List<UserClaimListModel> users = new List<UserClaimListModel>();
            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryMultipleAsync(query, new
                {
                    pageno = pageno,
                    pagesize = pagesize,
                    Textsearch = Textsearch,
                    SerarchByLeadStatusId = SerarchByLeadStatusId,
                    SerarchByLeadSourceId = SerarchByLeadSourceId,
                    OffsetVal = OffsetVal
                });

                var dataList = await result.ReadAsync<UserClaimListModel>();
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

    }
}
