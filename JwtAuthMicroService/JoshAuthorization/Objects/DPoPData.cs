using JoshAuthorization.Models;

namespace JoshAuthorization.Objects;

public class DPoPData : IJwtResultData
{
    public DPoPPayload?  Payload { get; init; }
    public JwkObject? Jwk { get; init; }
}