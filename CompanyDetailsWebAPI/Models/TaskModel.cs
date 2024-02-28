namespace CompanyDetailsWebAPI.Models
{
    public class TaskModel
    {
        // Id	TaskTypeID	TaskName	CustomerName	PlicyNumber	Assignee	Remark	TaskDate
        // 	FollowUpDate	IsDeleted	CreatedBy	CreatedDate	ModifiedBy	ModifieDate

        public int Id { get; set; }
        public int TaskTypeID { get; set; }
        public string TaskName { get; set; }
        public string CustomerName { get; set; }
        public string PlicyNumber { get; set; }
        public string Assignee { get; set; }
        public string Remark { get; set; }
        public DateTime TaskDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }
    }
}
