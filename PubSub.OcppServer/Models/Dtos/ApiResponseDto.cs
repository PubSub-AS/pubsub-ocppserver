using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Models.Dtos
{
    public class ApiResponseDto
    {
        public ApiResponseDto() { }
        public ApiResponseStatusEnum Status { get; set; }
        public string StatusMessage { get; set; }
        public object? Data { get; set; }

        public static ApiResponseDto CreateApiResponseObject(OcppResponseOrError ocppResponseOrError)
        {
            ApiResponseDto apiResponseObject = new ApiResponseDto();
            if (ocppResponseOrError != null) { return  apiResponseObject; }
            if (ocppResponseOrError.OcppResponse == null)
            {
                return new ApiResponseDto()
                {
                    // map error codes like NotImplemented etc
                    Status = ApiResponseStatusEnum.Faulted,
                    StatusMessage = ocppResponseOrError.ErrorCode + " " + ocppResponseOrError.ErrorMessage
                };
            }
            return new ApiResponseDto()
            {
                Status = ApiResponseStatusEnum.OK
            };

        }
    }
}
