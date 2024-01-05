using System.Security.Cryptography.X509Certificates;

namespace CompanyDetailsWebAPI.Models
{
    public class OrganizationAddUpdateModel
    {

        // Id	OrganizationId	UnitId	TallyCompanyName	TallyCompanyAdress	CountryName	
        // StateName	AdressLine1	AdressLine2	AdressLine3	AdressLine4	RoundOffLedgerName
        // 	CGSTLedgerName	SGSTLedgerName	IGSTLedgerName	GSTLedgerName	
        // 	IsDeleted CreatedBy	CreatedDate	ModifiedBy	ModifieDate

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UnitId { get; set; }
        public string TallyCompanyName { get; set; }
        public string TallyCompanyAdress { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string AdressLine1 { get; set; }
        public string AdressLine2 { get; set; }
        public string AdressLine3 { get; set; }
        public string AdressLine4 { get; set; }
        public string RoundOffLedgerName { get; set; }
        public string CGSTLedgerName { get; set; }
        public string SGSTLedgerName { get; set; }
        public string IGSTLedgerName { get; set; }
        public string GSTLedgerName { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }

    }

    public class OrganizationModel
    {

        // Id	OrganizationId	UnitId	TallyCompanyName	TallyCompanyAdress	CountryName	
        // StateName	AdressLine1	AdressLine2	AdressLine3	AdressLine4	RoundOffLedgerName
        // 	CGSTLedgerName	SGSTLedgerName	IGSTLedgerName	GSTLedgerName	
        // 	IsDeleted CreatedBy	CreatedDate	ModifiedBy	ModifieDate

        public int Id { get; set; }
        //  public int OrganizationId { get; set; }
        //  public int UnitId { get; set; }
        public string OrganizationName { get; set; }
        public string unitName { get; set; }
        public string TallyCompanyName { get; set; }

        //   public string TallyCompanyAdress { get; set; }
        //  public string CountryName { get; set; }
        //   public string StateName { get; set; }
        //  public string AdressLine1 { get; set; }
        //  public string AdressLine2 { get; set; }
        //  public string AdressLine3 { get; set; }
        //  public string AdressLine4 { get; set; }
        // public string RoundOffLedgerName { get; set; }
        // public string CGSTLedgerName { get; set; }
        //  public string SGSTLedgerName { get; set; }
        //  public string IGSTLedgerName { get; set; }
        //  public string GSTLedgerName { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifieDate { get; set; }

    }

    
}
