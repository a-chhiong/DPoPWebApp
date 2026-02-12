namespace JoshAuthorization;

public class JwtAuthEnvironmentOption
{
    public string BaseUrl { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string PublicKey { get; init; }
    public string PrivateKey { get; init; }
}