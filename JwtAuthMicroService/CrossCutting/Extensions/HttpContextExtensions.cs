using Microsoft.AspNetCore.Http;

namespace CrossCutting.Extensions;

public static class HttpContextExtensions
{
    // Generic setter
    public static void SetMetadata<T>(this HttpContext context, T value)
    {
        context.Items[typeof(T)] = value;
    }

    // Generic getter
    public static T? GetMetadata<T>(this HttpContext context)
    {
        if (context.Items.TryGetValue(typeof(T), out var value) && value is T typed)
        {
            return typed;
        }
        return default;
    }
}