using System;

namespace CompanyDetailsWebAPI.Models
{
    public class UserRegModel
    {
        // Id UserName    MobileNumber PANCard Email Address DateOfBirth ReferenceName
        // GenderId LeadStatusId    LeadSourceId OccupationId   
        //     OccupationName OccupationDescription   IsDeleted CreatedBy   CreatedDate ModifiedBy  ModifieDate
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string PANCard { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ReferenceName { get; set; }
        public int GenderId { get; set; }
        public int LeadStatusId { get; set; }
        public int LeadSourceId { get; set; }
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
        public string OccupationDescription { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }
    }


    public class UseAddUpdateModel
    {
        // Id UserName    MobileNumber PANCard Email Address DateOfBirth ReferenceName
        // GenderId LeadStatusId    LeadSourceId OccupationId   
        //     OccupationName OccupationDescription   IsDeleted CreatedBy   CreatedDate ModifiedBy  ModifieDate
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string PANCard { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ReferenceName { get; set; }
        public int GenderId { get; set; }
        public int LeadStatusId { get; set; }
        public int LeadSourceId { get; set; }
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
        public string OccupationDescription { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }
    }

    public class UserClaimListModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string PANCard { get; set; }
        public int LeadStatusId { get; set; }
        public int LeadSourceId { get; set; }


    }

    public class MailModel1
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }

    }
    // UserRegistrationModel.cs
    public class UserRegistrationModel
    {
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string PANCard { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        // Add other properties as per your table structure
    }

}
