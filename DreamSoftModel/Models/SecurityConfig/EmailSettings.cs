namespace DreamSoftModel.Models.SecurityConfig;

public class EmailSettings
{
    public string ResendApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}