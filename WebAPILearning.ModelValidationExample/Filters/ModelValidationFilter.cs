using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPILearning.ModelValidationExample.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(actionContext.ModelState.IsValid)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var errorList = actionContext.ModelState.Values.SelectMany(v => v.Errors)
                                                                .Select(x => x.ErrorMessage).ToList(); // modeldeki error mesajları böyle aldık
                var errorMessage = string.Join(Environment.NewLine, errorList);
                actionContext.Response= actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage); // response olarak hatayı döndürüyoruz
            }
            
        }
    }
}