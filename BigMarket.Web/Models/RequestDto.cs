using static BigMarket.Web.Utility.SD;

namespace BigMarket.Web.Models
{
    public sealed class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
