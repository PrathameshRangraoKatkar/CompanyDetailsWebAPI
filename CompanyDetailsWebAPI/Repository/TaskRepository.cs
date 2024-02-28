using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace CompanyDetailsWebAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {

        private readonly DapperContext _dapperContext;

        public TaskRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;

        }
        public async Task<int> Add(TaskModel taskModel)
        {
            int result = 0;

            using (var connection = _dapperContext.CreateConnection())
            {
               
                result = await connection.ExecuteAsync("Sp_AddTask",
                    new
                    {
                        TaskTypeID = taskModel.TaskTypeID,
                        TaskName = taskModel.TaskName,
                        CustomerName = taskModel.CustomerName,
                        PlicyNumber = taskModel.PlicyNumber,
                        Assignee = taskModel.Assignee,
                        Remark = taskModel.Remark,
                        TaskDate = taskModel.TaskDate,
                        FollowUpDate = taskModel.FollowUpDate,
                        CreatedBy = taskModel.CreatedBy,
                        CreatedDate = taskModel.CreatedDate,
                        ModifiedBy = taskModel.ModifiedBy,
                        ModifieDate = taskModel.ModifieDate
                    },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }


        public async  Task<int> Delete(int Id)
        {
            int result = 0;

            var query = @"update tbl_Task set Isdeleted =1 WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, new { Id });

                return result;
            }
        }

        public async Task<int> Update(TaskModel taskModel)
        {
            int result = 0;

            var query = @"UPDATE tbl_Task
                  SET TaskTypeID = @TaskTypeID,
                      TaskName = @TaskName,
                      CustomerName = @CustomerName,
                      PlicyNumber = @PlicyNumber,
                      Assignee = @Assignee,
                      Remark = @Remark,
                      TaskDate = @TaskDate,
                      FollowUpDate = @FollowUpDate,
                      ModifiedBy = @ModifiedBy,
                      ModifieDate = @ModifieDate
                  WHERE Id = @Id;";

            using (var connection = _dapperContext.CreateConnection())
            {
                result = await connection.ExecuteAsync(query, taskModel);
                return result;
            }
        }

    }
}
