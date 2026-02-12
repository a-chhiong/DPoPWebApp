using JoshAuthorization.Models;

namespace JoshAuthorization.Objects;

public class AccessData : IJwtResultData
{
    public TokenPayload? Payload { get; init; }
    public string? DPoPJti { get; init; }
}