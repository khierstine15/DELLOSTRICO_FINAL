using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using final_project3.Models;

namespace final_project3.Controllers
{
    public class HomeController : Controller
    {

        private kristineEntities1 db = new kristineEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create() {
            ViewBag.Message = "Add New User";

            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc) {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            int age = Convert.ToInt16(fc["age"]);
            String password = fc["password"];
            String address = fc["address"];

            user use = new user();
            use.first_name = firstName;
            use.last_name = lastName;
            use.email = email;
            use.password = password;
            use.address = address;
            use.age = age;
            use.role_id = 1;

            db.users.Add(use);
            db.SaveChanges();

            return RedirectToAction("User");
        }

        public ActionResult User()
        {
            ViewBag.Message = "User Lists";

            var userList = (from a in db.users
                            select a).ToList();

            ViewData["userList"] = userList;

            return View();
        }

        public ActionResult Edit(int id) {
            var selectedUser = (from a in db.users
                                where a.user_id == id
                                select a).ToList();

            ViewData["selectedUser"] = selectedUser;
            return View();
        }

        public ActionResult UpdateUser(FormCollection fc, int id) {
            user use = (from a in db.users
                        where a.user_id == id
                        select a).FirstOrDefault();
            db.users.Find(id);

            String new_first_name = fc["new_firstname"];
            String new_last_name = fc["new_lastname"];
            String new_email = fc["new_email"];
            String new_address = fc["new_address"];
            int new_age = Convert.ToInt16(fc["new_age"]);
            String new_password = fc["new_password"];

            use.first_name = new_first_name;
            use.last_name = new_last_name;
            use.email = new_email;
            use.password = new_password;
            use.address = new_address;
            use.age = new_age;

            db.SaveChanges();

            return RedirectToAction("User");
        }

        public ActionResult Delete(int id) {
            user use = (from a in db.users
                        where a.user_id == id
                        select a).FirstOrDefault();
            db.users.Remove(use);
            db.SaveChanges();

            return RedirectToAction("User");
        }
    }
}