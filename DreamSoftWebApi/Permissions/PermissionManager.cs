using System.Net;
using System.Security.Claims;
using System.Text.Json;
using DreamSoftLogic.Exceptions.Security;
using DreamSoftModel.Models.Exception;
using DreamSoftModel.Models.Public;

namespace DreamSoftWebApi.Permissions;

public class PermissionManager(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var endPoint = context.GetEndpoint();
        var permissionAttributes = endPoint?.Metadata.GetOrderedMetadata<PermissionAttribute>() ?? [];

        string? optionId = null;
        string? optionName = null;
        PermissionAction? action = null;

        foreach (var attr in permissionAttributes)
        {
            if (!string.IsNullOrEmpty(attr.OptionId)) optionId = attr.OptionId;
            if (!string.IsNullOrEmpty(attr.OptionName)) optionName = attr.OptionName;
            if (attr.Action.HasValue) action = attr.Action;
        }

        //var permissionAttr = endPoint?.Metadata.GetMetadata<PermissionAttribute>();
        if (!string.IsNullOrEmpty(optionId) && !string.IsNullOrEmpty(optionName) && action.HasValue)
        {
            var permissionAttr = new PermissionAttribute(optionId, optionName, action.Value);
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var errorResponse = new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    ErrorCode = "40101",
                    ErrorMessage = "Unauthorized user",
                    ErrorType = "Unauthorized"
                };
                var result = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(result);
                return;
            }

            try
            {
                var rolestr = context.User.FindFirstValue(ClaimTypes.Role);
                if (string.IsNullOrEmpty(rolestr))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    var errorResponse = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        ErrorCode = "40101",
                        ErrorMessage = "Invalid user role",
                        ErrorType = "Unauthorized"
                    };
                    var result = JsonSerializer.Serialize(errorResponse);
                    await context.Response.WriteAsync(result);
                    return;
                }

                var role = JsonSerializer.Deserialize<Role>(rolestr);
                if (!role!.SuperUser)
                {
                    var optionsstr = context.User.FindFirstValue("permissions") ??
                                     throw new PermissionException(
                                         "This user does not have permission to perform that action.");
                    var options = JsonSerializer.Deserialize<List<RoleOption>>(optionsstr);
                    if (options == null || !HavePermissionTo(permissionAttr, options))
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        var errorResponse = new ErrorResponse
                        {
                            StatusCode = (int)HttpStatusCode.Forbidden,
                            ErrorCode = "40301",
                            ErrorMessage = "You do not have permission to perform this action.",
                            ErrorType = "Forbidden"
                        };
                        var result = JsonSerializer.Serialize(errorResponse);
                        await context.Response.WriteAsync(result);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errorResponse = new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorCode = "50000",
                    ErrorMessage = ex.Message,
                    ErrorType = "Unknown"
                };
                var result = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(result);
                return;
            }
        }

        await next(context);
    }

    private bool HavePermissionTo(PermissionAttribute permissionAttribute, List<RoleOption> options)
    {
        var accessoptions = options.Where(o =>
            o.MenuOption.Name == permissionAttribute.OptionName &&
            o.MenuOption.MenuOptionId == Convert.ToInt32(permissionAttribute.OptionId)).ToList();
        if (accessoptions.Count <= 0) return false;
        var option = accessoptions.First();
        return permissionAttribute.Action switch
        {
            PermissionAction.Create => option.CanCreate,
            PermissionAction.Read => option.CanRead,
            PermissionAction.Update => option.CanUpdate,
            PermissionAction.Delete => option.CanDelete,
            _ => false
        };
    }
}