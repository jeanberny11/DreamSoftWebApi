namespace DreamSoftModel.Models.Exception;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorCode { get; set; } = null!;
    public string ErrorType { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
}