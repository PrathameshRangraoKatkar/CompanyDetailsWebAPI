namespace CompanyDetailsWebAPI.Models
{
    public class BaseResponseModel
    {
        public object ResponseData1 { get; set; }
        public object ResponseData2 { get; set; }
        public object ResponseData3 { get; set; }

        

    }
    public class BaseResponseModel1
    {
        public int TotalCount { get; set; }
        public int deletedCount { get; set; }


    }
}
