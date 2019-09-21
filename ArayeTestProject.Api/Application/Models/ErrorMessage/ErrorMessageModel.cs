namespace ArayeTestProject.Api.Application.Models.ErrorMessage
{
     public class ErrorMessageModel
    {
        public ErrorData Data { get; set; }
    }
    public class ErrorData
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}