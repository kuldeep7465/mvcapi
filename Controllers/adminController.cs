using kuldeep.db_content;
using kuldeep.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kuldeep.Controllers
{
    public class adminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44315/");

        HttpClient Client;


        public adminController()
        {

            Client = new HttpClient();

            Client.BaseAddress = baseAddress;

        }
        // GET: admin
    
        [Authorize]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
         
        }
       
        [HttpGet]
         public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Class2 obj1)
        {
            kuldeepEntities1 obj = new kuldeepEntities1();
            var log = obj.Tables.Where(a => a.email == obj1.email).FirstOrDefault();
            if(log==null)
            {
                TempData["email"] = "email invalid";
            }
            else
            {
                if(log.email==obj1.email && log.password==obj1.password)
                {
                    FormsAuthentication.SetAuthCookie(log.email, false);
                    return RedirectToAction("indexdasbord");
                }
                else
                {
                    TempData["pass"] = "password invalid";
                }
            }
            return View();
        }
        [Authorize]
        public ActionResult Indexdasbord()
        {
            return View();
        }
        [Authorize]
        public ActionResult list()
        {

            List<Class1> obj = new List<Class1>();
            List<my_table> obj1 = new List<my_table>();

            HttpResponseMessage emp = Client.GetAsync(Client.BaseAddress + "emp/getemployee").Result;

            if (emp.IsSuccessStatusCode)
            {

                string data = emp.Content.ReadAsStringAsync().Result;


                obj1 = JsonConvert.DeserializeObject<List<my_table>>(data);

            }
            //var res = obj.my_table.ToList();
            return View(obj1);
           
        }
        [Authorize]
        public ActionResult form()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public List<my_table> form(class1 obj2)
        {
            List<my_table> obj = new List<my_table>();
            string data = JsonConvert.SerializeObject(obj);

            StringContent content = new StringContent(data,Encoding.UTF8, "application/json");

            HttpResponseMessage res = Client.PostAsync(Client.BaseAddress + "Emp/SaveEmployee", content).Result;

            if (res.IsSuccessStatusCode)
            {


            }

            //kuldeepEntities1 obj = new kuldeepEntities1();
            //my_table obj3 = new my_table();
            //obj3.id = obj2.id;
            //obj3.name = obj2.name;
            //obj3.email = obj2.email;
            //if (obj2.id == 0)
            //{

            //    obj.my_table.Add(obj3);
            //    obj.SaveChanges();
            //    return RedirectToAction("list");
            //}
            //else
            //{
            //    obj.Entry(obj3).State = System.Data.Entity.EntityState.Modified;
            //    obj.SaveChanges();
            //    return RedirectToAction("list");

            //}

            return obj;
        }
        [Authorize]
        public ActionResult delete(int id)
        {


            kuldeepEntities1 obj = new kuldeepEntities1();
            var delete = obj.my_table.Where(n=> n.id == id).First();
            obj.my_table.Remove(delete);
            obj.SaveChanges();

            return RedirectToAction("list");
        }
        [Authorize]
        public ActionResult edit(int id)
        {


            kuldeepEntities1 obj = new kuldeepEntities1();
            my_table obj3 = new my_table();
            var edit = obj.my_table.Where(n => n.id == id).First();
            obj3.name = edit.name;
            obj3.email = edit.email;


            return View("form", obj3);
        }
        [Authorize]
        public ActionResult Log()
        {
            return View();
        }
        
    }
}