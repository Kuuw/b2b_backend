using Entities.Context.Abstract;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PL.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserContext userContext)
        {
            var user = context.User;

            var userIdClaim = user.FindFirst("UserId");
            var roleClaim = user.FindFirst("Role");
            var emailClaim = user.FindFirst("Email");
            var firstNameClaim = user.FindFirst("FirstName");
            var lastNameClaim = user.FindFirst("LastName");
            var permissionsClaim = user.FindFirst("Permissions")?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                userContext.UserId = userId;
            }

            userContext.Role = roleClaim?.Value ?? "";
            userContext.Email = emailClaim?.Value ?? "";
            userContext.FirstName = firstNameClaim?.Value ?? "";
            userContext.LastName = lastNameClaim?.Value ?? "";

            if (permissionsClaim != null)
            {
                try
                {
                    // Parse the JSON to handle the reference-preserving format
                    using (JsonDocument document = JsonDocument.Parse(permissionsClaim))
                    {
                        // Check if it contains the $values property
                        if (document.RootElement.TryGetProperty("$values", out var valuesElement))
                        {
                            userContext.Permissions = JsonSerializer.Deserialize<List<string>>(
                                valuesElement.GetRawText(),
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            ) ?? new List<string>();
                        }
                        else
                        {
                            // Fallback to direct deserialization if it's a simple array
                            userContext.Permissions = JsonSerializer.Deserialize<List<string>>(
                                permissionsClaim,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                            ) ?? new List<string>();
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing permissions: {ex.Message}");
                    userContext.Permissions = new List<string>();
                }
            }
            else
            {
                userContext.Permissions = new List<string>();
            }

            await _next(context);
        }
    }
}
