using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ecommerse.Models;
using ecommerse.ViewModel;
using System.Linq;

namespace ecommerse.Controllers
{
    public class RetailerController : Controller
    {
        private readonly shoppingDB1Context db;

        public RetailerController(shoppingDB1Context context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RetailerRegister()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RetailerRegister(Retailer r)
        {
            db.Retailers.Add(r);
            db.SaveChanges();
            return View("RetailerLogIn");
        }

        public IActionResult RetailerLogIn()
        {
            return View();
        }

        public IActionResult LogInValidations(LogInVM log)
        {
            if (ModelState.IsValid)
            {
                var user = db.Retailers.Where(query => query.RetailerEmail.Equals(log.Email) && query.Password.Equals(log.password)).SingleOrDefault();

                if (user == null)
                    return View("Index");

                else
                    TempData["Retailer"] = JsonConvert.SerializeObject(user);
                TempData.Keep();
                return RedirectToAction("HomeIndex", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult forgotPwd()
        {
            return View();
        }
        [HttpPost]

        public IActionResult forgotPwd(ForgotPwd pwd)
        {
            if (ModelState.IsValid)
            {
                if (pwd.NewPassword == pwd.ConfirmPassword)
                {
                    var retailer = db.Retailers.Where(query => query.RetailerEmail.Equals(pwd.Email)).SingleOrDefault();

                    if (retailer == null)
                        return View("Index");
                    else
                    {
                        retailer.Password = pwd.NewPassword;
                        db.SaveChanges();
                        return View("RetailerLogIn");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return View();
            }

        }
       

        public IActionResult RetailerDetails()
        {
            var retailer = JsonConvert.DeserializeObject<Retailer>((string)TempData["Retailer"]);
            return View(retailer);
        }
        [HttpGet]
        public IActionResult EditRetailer(int id)
        {
            Retailer ret = db.Retailers.Find(id);
            if (ret == null)
                return Content("Not found");
            return View(ret);
        }

        [HttpPost]
        public IActionResult EditRetailer(Retailer r)
        {
            Retailer retailer = db.Retailers.Find(r.RetailerId);
            retailer.RetailerName = r.RetailerName;
            retailer.RetailerPhoneNum = r.RetailerPhoneNum;
            retailer.RetailerEmail = r.RetailerEmail;
            retailer.City = r.City;
            retailer.State = r.State;
            retailer.Country = r.Country;
            retailer.Pincode = r.Pincode;
            retailer.ProductType = r.ProductType;
            db.SaveChanges();
            return View("RetailerDetails", retailer);
        }
    }
}
