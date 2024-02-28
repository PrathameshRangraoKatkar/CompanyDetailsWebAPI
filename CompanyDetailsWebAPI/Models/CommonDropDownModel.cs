using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDetailsWebAPI.Models
{
    public class CommonDropDownModel
    {
        public class StateModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class DistrictModel
        {
            public int Id { get; set; }
            public int StateId { get; set; }
            public string Name { get; set; }
        }
        public class TalukaModel
        {
            public int Id { get; set; }
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public string Name { get; set; }
        }

        public class LeadStatusModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        public class LeadSourceModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class OccupationModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }



        public class CommOrgModel
        {
            public int OrgId { get; set; }
            public string OrganizationName { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class UnitModel
        {
            public int Id { get; set; }
            public string UnitName { get; set; }
            public bool IsDeleted { get; set; }
        }
        public class TaskTypeModel0
        {
            public int Id { get; set; }
            public string TaskType { get; set; }
        }
            public class TaskTypeModel
        {
            public int Id { get; set; }
            public string TaskType { get; set; }
            public string TaskName { get; set; }
            public string CustomerName { get; set; }
            public string PlicyNumber { get; set; }
            public string Assignee { get; set; }
            public string Remark { get; set; }
            public string TaskDate { get; set; }
           // public string FollowUpDate { get; set; }
           // public bool IsDeleted { get; set; }
        }
    }


}

