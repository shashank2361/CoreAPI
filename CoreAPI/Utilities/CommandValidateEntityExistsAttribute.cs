using CoreAPI.Data;
using CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CoreAPI.Utilities
{
    public class CommandValidateEntityExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly CommandContext _context;

    public CommandValidateEntityExistsAttribute(CommandContext context)
    {
        _context = context;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        int id = 0;

        if (context.ActionArguments.ContainsKey("id"))
        {
            id = (int)context.ActionArguments["id"];
        }
        else
        {
            context.Result = new BadRequestObjectResult("Bad id parameter");
            return;
        }

        var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));
        if (entity == null)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            context.HttpContext.Items.Add("entity", entity);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    { }
}
}