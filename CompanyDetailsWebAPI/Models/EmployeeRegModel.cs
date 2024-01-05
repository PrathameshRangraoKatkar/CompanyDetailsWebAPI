using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDetailsWebAPI.Models
{
    //[Table("tbl_EmployeeReg ")]

    public class EmployeeRegModel
    {
        //      Id EmployeeName    Email MobileNumber    DateOfBirth GenderId    Password  
        // Address StateId DistId  City TalukaId    CreatedBy IsDeleted   CreatedDate ModifiedBy  ModifieDate

        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int DistId { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int TalukaId { get; set; }
        public string Taluka { get; set; }
        
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }
        public string MSG { get; set; }
    }
    public class EmployeeAddupdateModel
    {

        //      Id EmployeeName    Email MobileNumber    DateOfBirth GenderId    Password  
        // Address StateId DistId  City TalukaId    CreatedBy IsDeleted   CreatedDate ModifiedBy  ModifieDate

        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public int DistId { get; set; }
        public string City { get; set; }
        public int TalukaId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }
        public string MSG { get; set; }

    }
    public class LoginModel
    {

        
        public string MobileNumber { get; set; }
        public string Password { get; set; }

    }

   
    public class ChangepasswordModel
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

    }



}
