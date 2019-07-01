namespace GapLib.Model
{
    public class PostResult
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public PostResult() { }
        public PostResult(string errorMessage)
        {
            StatusCode = StatusCode.Genereic;
            Message = errorMessage;
        }

        public PostResult(StatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            Message = errorMessage;
        }
    }


    public enum StatusCode
    {
        Success = 200,
        InvalidChatIdOrToken = 403,
        Genereic = 400,
        InvalidFileTypeOrSize = 500
    }
}
