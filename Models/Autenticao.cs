using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoPixelPlace.Models
{
    public class Autenticao : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("user") == null)
            {
                context.Result = new RedirectResult("~/Usuario/Logar");
            }
        }
    }
}
