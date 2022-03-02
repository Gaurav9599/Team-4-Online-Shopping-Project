using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ecommerse.Models;
using ecommerse.ViewModel;

namespace ecommerse.Controllers
{
    public class CustomerDetailsController : Controller
    {
        private readonly shoppingDB1Context db;

        public CustomerDetailsController(shoppingDB1Context context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CustomerRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegister(CustomerDetail cust)
        {
            db.CustomerDetails.Add(cust);
            db.SaveChanges();
            return View("CustomerLogIn");
        }

        public IActionResult CustomerLogIn()
        {
            return View();
        }

        public IActionResult LogInValidations(LogInVM log)
        {

            var user = db.CustomerDetails.Where(query => query.Email.Equals(log.Email) && query.Password.Equals(log.password)).SingleOrDefault();

            if (user == null)
                return View("Index");

            else
                TempData["Customer"] = JsonConvert.SerializeObject(user);
                TempData.Keep();
                return RedirectToAction("HomeIndex", "Home");
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
                    var customer = db.CustomerDetails.Where(query => query.Email.Equals(pwd.Email)).SingleOrDefault();

                    if (customer == null)
                        return View("Index");
                    else
                    {
                        customer.Password = pwd.NewPassword;
                        db.SaveChanges();
                        return View("CustomerLogIn");
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


       
        
        public IActionResult CustomerDetails2()
        {
            var cust = JsonConvert.DeserializeObject<CustomerDetail>((string)TempData["Customer"]);

            return View(cust);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CustomerDetail cust = db.CustomerDetails.Find(id);
            if (cust == null)
                return Content("Not found");
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(CustomerDetail cust)
        {
            CustomerDetail customer = db.CustomerDetails.Find(cust.CustId);
            customer.CustName = cust.CustName;
            customer.PhoneNum = cust.PhoneNum;
            customer.Email = cust.Email;
            db.SaveChanges();
            return View("CustomerDetails2", customer);
        }       
    }
}
