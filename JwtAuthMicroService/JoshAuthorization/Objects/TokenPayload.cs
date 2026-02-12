using JoshAuthorization.Models;

namespace JoshAuthorization.Objects;

public class TokenPayload: IJwtPayload
{
    /// <summary>
    /// JWT ID
    /// </summary>
    public string jti { get; init; }

    /// <summary>
    /// Issuer of the JWT
    /// </summary>
    public string iss { get; init; }
    
    /// <summary>
    /// Subject of the JWT
    /// </summary>
    public string sub { get; init; }
    
    /// <summary>
    /// Audience for which the JWT is intended
    /// </summary>
    public string aud { get; init; }
    
    /// <summary>
    /// Expiration
    /// </summary>
    public long exp { get; init; }
    
    /// <summary>
    /// Not Before
    /// </summary>
    public long? nbf { get; init; }
    
    /// <summary>
    /// Issued Time
    /// </summary>
    public long iat { get; init; }
    
    /// <summary>
    /// DPop Confirmation
    /// </summary>
    public CnfObject? cnf { get; init; }
    
    /// <summary>
    /// Metadata of the JWT (the user)
    /// </summary>
    public JwtMetadata meta { get; init; }

}