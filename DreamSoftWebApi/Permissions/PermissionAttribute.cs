namespace DreamSoftWebApi.Permissions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute(
    string? optionid = null,
    string? optionname = null,
    PermissionAction action = PermissionAction.Read)
    : Attribute
{
    public string? OptionId { get; } = optionid;
    public string? OptionName { get; } = optionname;
    public PermissionAction? Action { get; } = action;
}