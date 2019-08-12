namespace GapLib.Model
{
    // TODO : error message
    public class PostResult
    {
        public string Id { get; set; }
        public StatusCode StatusCode { get; set; }
        public string RawBody { get; set; }
        public string ErrorMessage { get; set; }

        public PostResult() { }
        public PostResult(string body)
        {
            StatusCode = StatusCode.Genereic;
            RawBody = body;
            Id = Utils.Deserialize<MessageId>(RawBody)?.Id;
            ErrorMessage = Utils.Deserialize<ErrorMessage>(RawBody)?.Error;
        }

        public PostResult(StatusCode statusCode, string body)
        {
            StatusCode = statusCode;
            RawBody = body;
            Id = Utils.Deserialize<MessageId>(RawBody)?.Id;
            ErrorMessage = Utils.Deserialize<ErrorMessage>(RawBody)?.Error;
        }


    }

    public class PostResult<T> : PostResult
    {
        public T Data { get; set; }


        public PostResult() { }
        public PostResult(string body) : base(body)
        {
            Data = Utils.Deserialize<T>(body);
        }

        public PostResult(StatusCode statusCode, string body) : base(statusCode, body)
        {
            Data = Utils.Deserialize<T>(body);
        }
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
