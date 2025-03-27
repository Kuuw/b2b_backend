using Entities.Context.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PL.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NeedsPermissionAttribute : ActionFilterAttribute
    {
        private readonly string[] _applicablePermissions;

        public NeedsPermissionAttribute(params string[] applicablePermissions)
        {
            _applicablePermissions = applicablePermissions;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices.GetService(typeof(IUserContext)) as IUserContext;

            if (userContext == null)
            {
                context.Result = new ForbiddenObjectResult(new { message = "User context not available" });
                return;
            }

            bool hasPermission = _applicablePermissions.Any(permission => userContext.Permissions.Contains(permission));

            if (!hasPermission)
            {
                string requiredPermissions = string.Join(", ", _applicablePermissions);
                context.Result = new ForbiddenObjectResult(new { message = $"Missing required permissions. Need one of: {requiredPermissions}" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }

    public class ForbiddenObjectResult : ObjectResult
    {
        public ForbiddenObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}