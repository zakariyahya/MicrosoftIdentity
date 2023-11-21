namespace MicrosoftIdentity.Models.Base
{
    public class ApiResponse
    {
        public bool Result { set; get; }
        public int StatusCode { get; set; }
        public string Message { set; get; }
        public object Data { set; get; }
    }
}
