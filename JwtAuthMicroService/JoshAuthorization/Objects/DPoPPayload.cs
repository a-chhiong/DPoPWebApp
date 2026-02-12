using JoshAuthorization.Models;

namespace JoshAuthorization.Objects;

public class DPoPPayload: IJwtPayload
{
    /// <summary>
    /// JWT ID
    /// </summary>
    public string jti { get; init; }

    /// <summary>
    /// HTTP Method (GET, POST, etc)
    /// </summary>
    public string htm { get; init; } 
    
    /// <summary>
    /// HTTP URL (without query strings)
    /// </summary>
    public string htu { get; init; } 
    
    /// <summary>
    /// Issued Time
    /// </summary>
    public long iat { get; init; }

    /// <summary>
    /// Access Token Hash.
    /// </summary>
    public string? ath { get; init; }
}