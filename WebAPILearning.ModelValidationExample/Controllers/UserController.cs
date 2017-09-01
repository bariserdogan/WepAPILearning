using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPILearning.ModelValidationExample.Filters;
using WebAPILearning.ModelValidationExample.Models;

namespace WebAPILearning.ModelValidationExample.Controllers
{
    [ModelValidationFilter]
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] User model)
        {
            try
            { 
                return Request.CreateResponse(HttpStatusCode.Created,"Kullanıcı oluşturuldu");
                
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Beklenmedik bir hata oluştu");

            }
        }
    }
}
