namespace EagleBank.Models.APIException
{
    public class ApiErrorException : Exception
    {
        public int StatusCode { get; }

        public ApiErrorException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
    public class UnAuthorisedErrorException : ApiErrorException
    {
        public UnAuthorisedErrorException(string message) : base(message, 401) { }
    }
    public class NotFoundErrorException : ApiErrorException
    {
        public NotFoundErrorException(string message) : base(message, 404) { }
    }

    public class ForbiddenErrorException : ApiErrorException
    {
        public ForbiddenErrorException(string message) : base(message, 403) { }
    }
}
