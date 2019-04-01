using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class ForgetController : Controller
    {
        lightsDBEntities DB = new lightsDBEntities();
        private static string confirmCode;
        private static string GuestEmail;


        public ActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPass(string receiver)
        {
            if (getEmails(receiver))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var senderEmail = new MailAddress("younissaiedfcih@gmail.com", "Lights Hotel");
                        var receiverEmail = new MailAddress(receiver, "Receiver");
                        GuestEmail = receiver;
                        var password = "younis20172018";
                        var sub = "Reset Your Password";
                        confirmCode = RandomString(8, true);
                        var body = "use this code: " + confirmCode;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                        return RedirectToAction("ResetCodeConfirmation");
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Some Error";
                }
            }
            else return RedirectToAction("NotMatch");
            return View();
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private bool getEmails(String email)
        {
            var usr = DB.users.Where(y => y.email == email).SingleOrDefault();
            if (usr != null)
                return true;
            else
                return false;
        }

        public ActionResult ResetCodeConfirmation()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ResetCodeConfirmation(string code)
        {
            var usr = DB.users.Where(y => y.email == GuestEmail).SingleOrDefault();

            if (confirmCode.Equals(code))
            {
                // get into as 'wating user' until change the password
                return RedirectToAction("UpdatePassword/" + usr.id + "");
            }
            else { return RedirectToAction("InvalidCode"); }
           
        }


        public ActionResult NotMatch()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpdatePassword(int id)
        {
            var us = DB.users.SingleOrDefault(y => y.id == id);
            return View(us);
        }
        [HttpPost]
        public ActionResult UpdatePassword(user usr)
        {
            var us = DB.users.SingleOrDefault(u => u.id == usr.id);
            us.Pasword = usr.Pasword;
            DB.SaveChanges();
            return RedirectToAction("ListUsers");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var us = DB.users.SingleOrDefault(y => y.id == id);
            return View(us);
        }

        public ActionResult InvalidCode()
        {
            return View();
        }


    }
}