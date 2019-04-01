using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

        lightsDBEntities db = new lightsDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveToDb(contact model)
        {
            try
            {
                contact con = new contact();
                con.c_name = model.c_name;
                con.email = model.email;
                con.subjct = model.subjct;
                con.msage = model.msage;
                con.spam_flag = 0;

                db.contacts.Add(con);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Display()
        {
            IQueryable<contact> model = db.contacts.Where(m => m.spam_flag == 0);
            return View(model);
        }

        public ActionResult SpamMessage(int id)
        {
            try
            {
                List<contact> model = db.contacts.ToList();
                foreach (var m in model)
                {
                    if (m.id == id)
                    {
                        m.spam_flag = 1;
                        db.Entry(m).State = EntityState.Modified; 
                        db.SaveChanges();

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Display");

        }


        public ActionResult DisplaySpam()
        {
            IQueryable<contact> model = db.contacts.Where(m => m.spam_flag == 1);
            return View(model);
        }
    }
}