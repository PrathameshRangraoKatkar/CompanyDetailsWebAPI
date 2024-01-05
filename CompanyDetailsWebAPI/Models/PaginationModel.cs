namespace CompanyDetailsWebAPI.Models
{
    public class PaginationModel
    {
        public int PageNo { get; set; }
        public int TotalPages { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
    }
}
