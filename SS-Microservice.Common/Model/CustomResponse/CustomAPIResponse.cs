using System.Collections.Generic;

namespace SS_Microservice.Services.Auth.Application.Model.CustomResponse
{
    public class CustomAPIResponse<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }

        public CustomAPIResponse(T data, int status)
        {
            Data = data;
            Status = status;
        }

        public static CustomAPIResponse<T> Success(T data, int status)
        {
            return new CustomAPIResponse<T>(data, status);
        }

        public static CustomAPIResponse<NoContentAPIResponse> Success(int status)
        {
            return new CustomAPIResponse<NoContentAPIResponse>(new NoContentAPIResponse(), status);
        }
    }
}