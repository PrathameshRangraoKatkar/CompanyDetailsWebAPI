using CompanyDetailsWebAPI.Models;

namespace CompanyDetailsWebAPI.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<int> Add(TaskModel taskModel);
        Task<int> Update(TaskModel taskModel);
        Task<int> Delete(int Id);
    }
}
