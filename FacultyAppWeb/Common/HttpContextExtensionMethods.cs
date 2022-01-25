using System.Security.Claims;

namespace FacultyAppWeb.Common
{
    public static class HttpContextExtensionMethods
    {
        public static bool HasRole(this HttpContext httpContext, string role)
        {
            return httpContext.User.IsInRole(role);
        }

        public static string GetEmail(this HttpContext httpContext)
        {
            var email = httpContext.User.FindFirstValue(ClaimTypes.Name);
            return email;
        }
    }
}
