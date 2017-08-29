using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiLearning.CircularRefHandling.Controllers
{
    public class CategoryController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();

        public IEnumerable<Category> Get()
        {
            db.Configuration.ProxyCreationEnabled = false; // Entity Framework ile alakalı bir durum , xml için

            return db.Categories.Include("Product").ToList(); // yukardaki işlemi yaparak lazyloading'i kapattığımız için Product'ların gelmesi için elle yazıyoruz.
        }
    }
}
