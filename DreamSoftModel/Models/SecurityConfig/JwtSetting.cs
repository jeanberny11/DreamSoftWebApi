﻿namespace DreamSoftModel.Models.SecurityConfig;

public class JwtSetting
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
    public string Subject { get; set; } = null!;
}