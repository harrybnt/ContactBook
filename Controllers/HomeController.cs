 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactBookProject.Models;
namespace ContactBookProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetContacts()
        {
            using (ContactBookEntities dc = new ContactBookEntities())
            {
                var contact = dc.ContactBooks.OrderBy(a => a.FirstName).ToList();
                return Json(new { data = contact }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (ContactBookEntities dc = new ContactBookEntities())
            {
                var v = dc.ContactBooks.Where(a => a.ContactID == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(ContactBook con)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ContactBookEntities dc = new ContactBookEntities())
                {
                    if (con.ContactID > 0)
                    {
                        //Edit 
                        var v = dc.ContactBooks.Where(a => a.ContactID == con.ContactID).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = con.FirstName;
                            v.LastName = con.LastName;
                            v.EmailID = con.EmailID;
                            v.City = con.City;
                            v.Country = con.Country;
                        }
                        dc.SaveChanges();
                    }
                    else
                    {
                        //Save
                        try
                        {
                            dc.ContactBooks.Add(con);
                            dc.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            return new JavaScriptResult() { Script = "alert('Duplicate EmailID Found.');" };
                        }
                    }
                    
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult DeleteContact(int id)
        {
            using (ContactBookEntities dc = new ContactBookEntities())
            {
                var v = dc.ContactBooks.Where(a => a.ContactID == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("DeleteContact")]
        public ActionResult Delete(int id)
        {
            bool status = false;
            using (ContactBookEntities dc = new ContactBookEntities())
            {
                var v = dc.ContactBooks.Where(a => a.ContactID == id).FirstOrDefault();
                if (v != null)
                {
                    dc.ContactBooks.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}