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
    public class BadRequestErrorException : ApiErrorException
    {
        public BadRequestErrorException(string message) : base(message, 400) { }
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
    public class ConflictErrorException : ApiErrorException
    {
        public ConflictErrorException(string message) : base(message, 409) { }
    }
    public class UnprocessableErrorException : ApiErrorException
    {
        public UnprocessableErrorException(string message) : base(message, 422) { }
    }
}
