using System.Net;
using System.Text.Json;
using DreamSoftLogic.Exceptions.Security;
using DreamSoftModel.Models.Exception;
using Microsoft.AspNetCore.Http;

namespace DreamSoftLogic.Config;

public class ApiExceptionsMapper(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResponse = new ErrorResponse();

            switch (error)
            {
                case UnAuthorizedUserException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.ErrorCode = "40101";
                    errorResponse.ErrorMessage = e.Message;
                    errorResponse.ErrorType = "UNAUTHORIZED USER";
                    break;
                case TokenGenerationException e:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ErrorCode = "50001";
                    errorResponse.ErrorMessage = e.Message;
                    errorResponse.ErrorType = "TOKEN ERROR";
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.ErrorCode = "40401";
                    errorResponse.ErrorMessage = e.Message;
                    errorResponse.ErrorType = "NOT FOUND";
                    break;
                case InvalidOperationException e:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ErrorCode = "50002";
                    errorResponse.ErrorMessage = e.Message;
                    errorResponse.ErrorType = "DATABASE ERROR";
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ErrorCode = nameof(HttpStatusCode.InternalServerError);
                    errorResponse.ErrorMessage = error.Message;
                    errorResponse.ErrorType = "UNKNOWN ERROR";
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}