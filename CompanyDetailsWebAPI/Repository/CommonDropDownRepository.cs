using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyDetailsWebAPI.Repository
{
    public class CommonDropDownRepository : ICommonDropDownRepository
    {
        private readonly DapperContext _dapperContext;

        public CommonDropDownRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<CommonDropDownModel.StateModel>> GetAllState()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id, Name FROM tbl_States";
                var result = await connection.QueryAsync<CommonDropDownModel.StateModel>(query);
                return result.AsList();
            }
        }
        public async Task<List<CommonDropDownModel.StateModel>> GetAllDistrict(int stateId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT id, StateId, Name FROM tbl_Districts WHERE StateId = @StateId";
                var result = await connection.QueryAsync<CommonDropDownModel.StateModel>(query, new { StateId = stateId });
                return result.AsList();
            }
        }


        public async Task<List<CommonDropDownModel.StateModel>> GetAllTaluka(int StateId, int DistId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id,StateId,DistId, Name FROM tbl_Taluka WHERE StateId = @StateId AND DistId = @DistId";
                var result = await connection.QueryAsync<CommonDropDownModel.StateModel>(query, new { StateId = StateId, DistId = DistId });
                return result.AsList();
            }
        }

        public async Task<List<CommonDropDownModel.LeadStatusModel>> GetAllLeadStatus()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id, Name FROM tbl_LeadStatus";
                var result = await connection.QueryAsync<CommonDropDownModel.LeadStatusModel>(query);
                return result.AsList();
            }
        }

        public async Task<List<CommonDropDownModel.LeadSourceModel>> GetAllLeadSource()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id, Name   FROM tbl_LeadSource";
                var result = await connection.QueryAsync<CommonDropDownModel.LeadSourceModel>(query);
                return result.AsList();
            }
        }

        public async Task<List<CommonDropDownModel.OccupationModel>> GetAllOccupations()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id, Name   FROM tbl_Occupation";
                var result = await connection.QueryAsync<CommonDropDownModel.OccupationModel>(query);
                return result.AsList();
            }
        }


        public async Task<List<CommonDropDownModel.CommOrgModel>> GetAllOrganization()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT OrgId, Name as OrganizationName,IsDeleted FROM tbl_Organization";
                var result = await connection.QueryAsync<CommonDropDownModel.CommOrgModel>(query);
                return result.AsList();
            }
        }

        public async Task<List<CommonDropDownModel.UnitModel>> GetAllUnits()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "SELECT Id, Name as UnitName,IsDeleted FROM tbl_Unit";
                var result = await connection.QueryAsync<CommonDropDownModel.UnitModel>(query);
                return result.AsList();
            }
        }
    }
}
