
using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Dapper;

namespace CompanyDetailsWebAPI.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {

        private readonly DapperContext _dapperContext;
        public OrganizationRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;

        }
        public async Task<int> AddOrganization(OrganizationAddUpdateModel organizationAddUpdateModel)
        {
            int result = 0;
            var query = @"insert into tbl_TallyOrganizationConfig 
                               (OrganizationId,UnitId,TallyCompanyName,TallyCompanyAdress,CountryName,StateName,AdressLine1,
                          AdressLine2,AdressLine3,AdressLine4,RoundOffLedgerName,CGSTLedgerName,SGSTLedgerName,IGSTLedgerName,
                          GSTLedgerName,IsDeleted,CreatedBy,CreatedDate,ModifiedBy,ModifieDate)
                        values (@OrganizationId,@UnitId,@TallyCompanyName,@TallyCompanyAdress,@CountryName,@StateName,@AdressLine1,
                          @AdressLine2,@AdressLine3,@AdressLine4,@RoundOffLedgerName,@CGSTLedgerName,@SGSTLedgerName,@IGSTLedgerName,
                          @GSTLedgerName,0,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifieDate)
                                SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = _dapperContext.CreateConnection())
            {

                if (organizationAddUpdateModel.Id == 0 || organizationAddUpdateModel.Id == null)
                {
                    result = await connection.QuerySingleAsync<int>(query, organizationAddUpdateModel);
                }
                else
                {
                    result = -1;
                }

                return result;
            }
        }


        public async Task<int> EditOrganization(OrganizationAddUpdateModel organizationAddUpdateModel)
        {
            int result = 0;

            // Your update query
            var updateQuery = @"UPDATE tbl_TallyOrganizationConfig
                        SET
                            OrganizationId=@OrganizationId,
                            UnitId = @UnitId,
                            TallyCompanyName = @TallyCompanyName,
                            TallyCompanyAdress = @TallyCompanyAdress,
                            CountryName = @CountryName,
                            StateName = @StateName,
                            AdressLine1 = @AdressLine1,
                            AdressLine2 = @AdressLine2,
                            AdressLine3 = @AdressLine3,
                            AdressLine4 = @AdressLine4,
                            RoundOffLedgerName = @RoundOffLedgerName,
                            CGSTLedgerName = @CGSTLedgerName,
                            SGSTLedgerName = @SGSTLedgerName,
                            IGSTLedgerName = @IGSTLedgerName,
                            GSTLedgerName = @GSTLedgerName,
                            IsDeleted = @IsDeleted,
                            ModifiedBy = @ModifiedBy,
                            ModifieDate = @ModifieDate
                        WHERE Id = @Id";

            // Fetch updated list query
            var fetchUpdatedListQuery = "SELECT * FROM tbl_TallyOrganizationConfig WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                // Check if the organizationAddUpdateModel.OrganizationId is not 0 or null
                if (organizationAddUpdateModel.Id != 0 && organizationAddUpdateModel.Id != null)
                {
                    // Update the organization
                    result = await connection.ExecuteAsync(updateQuery, organizationAddUpdateModel);

                    // Fetch the updated list
                    var updatedList = await connection.QueryAsync<OrganizationAddUpdateModel>(fetchUpdatedListQuery, new { Id = organizationAddUpdateModel.Id });

                    // You can do something with the updatedList here
                }
                else
                {
                    result = -1;
                }

                return result;
            }
        }

        public async Task<int> BlockOrganization(int id)
        {
            const string query = @"UPDATE tbl_TallyOrganizationConfig SET Isdeleted = 1 WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }




        public async Task<IEnumerable<OrganizationAddUpdateModel>> GetOrganization()
        {
            // Your query to fetch the organization list
            var query = "SELECT * FROM tbl_TallyOrganizationConfig";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<OrganizationAddUpdateModel>(query);
            }
        }


        public async Task<BaseResponseModel> GetAllOrganization(int pageno, int pagesize, string? Textsearch)
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
                       TOC.Id,TOC.OrganizationId, Org.name as OrganizationName, TOC.UnitId, Un.name as unitName, 
                       TOC.TallyCompanyName, TOC.TallyCompanyAdress, TOC.CountryName,	
                       TOC.StateName, TOC.AdressLine1, TOC.AdressLine2, TOC.AdressLine3, TOC.AdressLine4, 
                       TOC.RoundOffLedgerName, TOC.CGSTLedgerName, TOC.SGSTLedgerName, TOC.IGSTLedgerName,
                       TOC.GSTLedgerName, TOC.IsDeleted, TOC.CreatedBy, TOC.CreatedDate, TOC.ModifiedBy, TOC.ModifieDate
                   FROM
                       tbl_TallyOrganizationConfig TOC
                       LEFT JOIN tbl_Organization Org ON TOC.OrganizationId = Org.OrgId
                       LEFT JOIN tbl_Unit Un ON TOC.UnitId = Un.ID
                   WHERE
                       TOC.IsDeleted = 0 
                       AND (TOC.TallyCompanyName LIKE '%' + @Textsearch + '%')
                   ORDER BY
                       TOC.Id DESC
                   OFFSET @OffsetVal ROWS FETCH NEXT @pagesize ROWS ONLY;

                   SELECT
                       @pageno AS PageNo,
                       COUNT(TOC.Id) AS TotalPages
                   FROM
                       tbl_TallyOrganizationConfig TOC
                       LEFT JOIN tbl_Organization Org ON TOC.OrganizationId = Org.OrgId
                       LEFT JOIN tbl_Unit Un ON TOC.UnitId = Un.ID
                   WHERE
                       TOC.IsDeleted = 0 
                       AND (TOC.TallyCompanyName LIKE '%' + @Textsearch + '%')";

            List<OrganizationModel> users;
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

                var dataList = await result.ReadAsync<OrganizationModel>();
                var paginationData = await result.ReadAsync<PaginationModel>();

                users = dataList.ToList();
                pagination1 = paginationData.FirstOrDefault();
                pagination1.PageCount = pagination1.TotalPages;
                int PageCount = pagination1.TotalPages / pagesize + (pagination1.TotalPages % pagesize == 0 ? 0 : 1);
                pagination1.TotalPages = PageCount;

                baseResponseModel.ResponseData1 = users;
                baseResponseModel.ResponseData2 = pagination1;
            }

            return baseResponseModel;
        }


        
    }
}
