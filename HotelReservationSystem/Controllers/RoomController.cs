using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class RoomController : Controller
    {
        lightsDBEntities db = new lightsDBEntities();
        // GET: Room
        //Diplays Rooms
        public ActionResult Index()
        {
            List<room> R = new List<room>();
            R = db.rooms.ToList();
            return View(R);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(room R)
        {
            if (ModelState.IsValid)
            {
                db.rooms.Add(R);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(R);
        }

        //GEt room by id
        public ActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room R = db.rooms.Find(ID);
            if (R == null)
            {
                return HttpNotFound();
            }

            return View(R);
        }

        //Delete room
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            room R = db.rooms.Find(ID);
            db.rooms.Remove(R);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get room by id
        public ActionResult Edit(int? ID)
        {
            room R = db.rooms.Find(ID);
            if (R == null)
            {
                return HttpNotFound();
            }

            return View(R);
        }

        //Edit room
        [HttpPost]
        public ActionResult Edit(room R)
        {
            if (ModelState.IsValid)
            {
                db.Entry(R).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(R);
        }


    }
}
