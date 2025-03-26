using Entities.Context.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PL.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NeedsPermissionAttribute : ActionFilterAttribute
    {
        private readonly string _requiredPermission;

        public NeedsPermissionAttribute(string requiredPermission)
        {
            _requiredPermission = requiredPermission;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices.GetService(typeof(IUserContext)) as IUserContext;

            if (userContext == null || !userContext.Permissions.Contains(_requiredPermission))
            {
                context.Result = new ForbiddenObjectResult(new { message = $"Missing required permission: {_requiredPermission}" });
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