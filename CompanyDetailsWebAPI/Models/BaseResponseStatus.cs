namespace CompanyDetailsWebAPI.Models
{
    internal class BaseResponseStatus
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object ResponseData { get; set; }
    }
}