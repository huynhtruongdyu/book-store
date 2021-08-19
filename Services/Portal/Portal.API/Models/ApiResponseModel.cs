using System.Collections.Generic;

namespace Portal.API.Models
{
    public class ApiResponseModel
    {
        public bool Status { get; set; }
        public List<string> LocaleParams { get; set; }
    }

    public class SuccessResponseModel<T> : ApiResponseModel
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ErrorResponseModel : ApiResponseModel
    {
        public List<string> Message { get; set; }
        public int? StatusCode { get; set; }
    }
}