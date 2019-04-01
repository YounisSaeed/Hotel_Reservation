using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class EmployeeController : Controller
    {
        lightsDBEntities db = new lightsDBEntities();
        // GET: Employee
        public ActionResult Index()
        {
            List<user> u = new List<user>();
            u = db.users.ToList();
            return View(u);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user E)
        {
            if (ModelState.IsValid)
            {
                E.id = 3;
                db.users.Add(E);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(E);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Update(int? id)
        {
            user b = db.users.Find(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Update(user uer)
        {

            if (ModelState.IsValid)
            {

                db.Entry(uer).State = EntityState.Modified;

                db.SaveChanges();

            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(String name)
        {
            IQueryable<user> v = db.users.Where(x => x.email == name);
            db.users.RemoveRange(v);
            db.SaveChanges();
            return View();
        }
    }
}