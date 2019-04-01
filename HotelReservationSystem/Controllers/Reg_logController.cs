using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers.Account
{
    public class Reg_logController : Controller
    {
        lightsDBEntities db = new lightsDBEntities();
        // GET: Reg_log
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }
        public ActionResult Rgister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rgister(user reg)
        {
            if (ModelState.IsValid)

            {
                reg.typeID = 2;
                db.users.Add(reg);


            db.SaveChanges();
                return RedirectToAction("login");
            }
                
            
            return View(reg);
        }
       /* public ActionResult listEmp()
        {
            var emp = getusers();
            return View(emp);
        }
        public IEnumerable<user>  getusers()
        {
            var usr = db.users.ToList();
            return usr;
        }*/
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(user reg)
        {
            if (ModelState.IsValid)
            {
                var details = (from users in db.users
                               where users.username == reg.username && users.Pasword == reg.Pasword
                               select new
                               {
                                   users.typeID,
                                   users.username
                               }
                    ).ToList();
                if (details.FirstOrDefault() != null)
                {
                    Session["username"] = details.FirstOrDefault().username;
                    Session["TypeId"] = details.FirstOrDefault().typeID;
                    return RedirectToAction("welcome");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }

            }
            return View(reg);
        }
        public ActionResult welcome()
        {
            return View();
        }

    }
}
