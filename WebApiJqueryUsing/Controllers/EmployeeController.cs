using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiJqueryUsing.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDBEntities db = new EmployeeDBEntities();

        public HttpResponseMessage Get(string gender = "all", int? top = 0) // query string ile filtrenebilir hale getirdik
        {
            IQueryable<Employees> query = db.Employees; // daha select işlemi yapılmadı şuan queryable

            gender = gender.ToLower();

            switch (gender)
            {
                case "all":
                    break;
                case "male":
                case "female":
                    query = query.Where(e => e.Gender.ToLower() == gender);
                    break;
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"{gender} is a not gender. Please use all,male or female");
            }
            if (top > 0)
            {
                query = query.Take(top.Value); // nullable değerlerde value alınır
            }

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList()); // işte tam bu anda veri tabanına select atılacak
        }
        public HttpResponseMessage Get(int id)
        {
            Employees employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Id si {id} olan kayıt bulunamadı.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }
        public HttpResponseMessage Post(Employees emp)
        {
            try
            {
                db.Employees.Add(emp);
                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + emp.Id); // insert edilen emp'nin detayını gösteren url response'un header'ına yazılacak

                    return message;
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veri ekleme yapılamadı");
            }
            catch (Exception error)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error.Message.ToString());
            }
        }
        public HttpResponseMessage Put([FromBody] MyBodyType mytype, [FromUri]Employees emp)
        {
            try
            {
                Employees employee = db.Employees.FirstOrDefault(x => x.Id == mytype.id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee Id:" + emp.Id);
                }
                else
                {
                    employee.Name = emp.Name;
                    employee.Surname = emp.Surname;
                    employee.Salary = emp.Salary;
                    employee.Gender = emp.Gender;
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Güncelleme yapılamadı");
                }
            }
            catch (Exception error)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Employees employee = db.Employees.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee Id:" + id);
                }
                else
                {
                    db.Employees.Remove(employee);
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Employee id:" + id);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Silinme yapılamadı");
                }
            }
            catch (Exception error)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
            }
        }
    }
    public class MyBodyType // request body'den aynı anda iki tane parametre gönderemediğimiz için bu kompleks classı yazdık
    {
        public int id { get; set; }
        public string second { get; set; }
    }
}
