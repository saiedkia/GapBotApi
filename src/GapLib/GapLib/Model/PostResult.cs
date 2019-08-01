namespace GapLib.Model
{
    // TODO : error message
    public class PostResult
    {
        public StatusCode StatusCode { get; set; }
        public string RawBody { get; set; }
        public string Id { get; set; }
        public string ErrorMessage { get; set; }

        public PostResult() { }
        public PostResult(string body)
        {
            StatusCode = StatusCode.Genereic;
            RawBody = body;
            Id = Utils.Deserialize<MessageId>(RawBody).id;
            ErrorMessage = Utils.Deserialize<ErrorMessage>(RawBody).error;
        }

        public PostResult(StatusCode statusCode, string body)
        {
            StatusCode = statusCode;
            RawBody = body;
            Id = Utils.Deserialize<MessageId>(RawBody).id;
            ErrorMessage = Utils.Deserialize<ErrorMessage>(RawBody).error;
        }


    }

    public class PostResult<T> : PostResult
    {
        public T Data { get; set; }
    }


    public enum StatusCode
    {
        Success = 200,
        InvalidChatIdOrToken = 403,
        Genereic = 400,
        InvalidFileTypeOrSize = 500
    }


    public static class PostResultExtension
    {
        public static PostResult<T> ToType<T>(this PostResult result)
        {
            PostResult<T> _result = new PostResult<T>
            {
                Id = result.Id,
                ErrorMessage = result.ErrorMessage,
                RawBody = result.RawBody,
                StatusCode = result.StatusCode,
                Data = Utils.Deserialize<T>(result.RawBody)
            };

            return _result;
        }
    }
}
