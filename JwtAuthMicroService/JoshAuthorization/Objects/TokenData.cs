using JoshAuthorization.Models;

namespace JoshAuthorization.Objects;

public class TokenData : IJwtResultData
{
    public TokenPayload? Payload { get; init; }
}