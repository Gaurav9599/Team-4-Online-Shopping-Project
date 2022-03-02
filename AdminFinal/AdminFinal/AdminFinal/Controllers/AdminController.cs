using AdminFinal.Models;
using AdminFinal.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminFinal.Controllers
{
    public class AdminController : Controller
    {
        private readonly shoppingDB1Context db;

        public AdminController(shoppingDB1Context context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminRegister(Admin ad)
        {
            db.Admins.Add(ad);
            db.SaveChanges();
            return View("AdLogIn");
        }

        public IActionResult AdLogIn()
        {
            return View();
        }

        public IActionResult LogInValidations(LogInVM log)
        {
            if (ModelState.IsValid)
            {
                var user = db.Admins.Where(query => query.AdminEmail.Equals(log.Email) && query.Adminpassword.Equals(log.password)).SingleOrDefault();

                if (user == null)
                    return View("Index");

                else
                    TempData["Admin"] = JsonConvert.SerializeObject(user);
                    TempData.Keep();
                    return RedirectToAction("Index2", "Home");
            }else
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
                    var admin = db.Admins.Where(query => query.AdminEmail.Equals(pwd.Email)).SingleOrDefault();

                    if (admin == null)
                        return View("Index");
                    else
                    {
                        admin.Adminpassword = pwd.NewPassword;
                        db.SaveChanges();
                        return View("AdLogIn");
                    }
                }
                else
                {
                    return View("Index");
                }
            }else
            {
                return View();
            }
        }

        public IActionResult AdminProfile()
        {
            var Adm = JsonConvert.DeserializeObject<Admin>((string)TempData["Admin"]);

            return View(Adm);
        }


        public IActionResult AdmList()
        {
            return View(db.Admins.ToList());
        }
        public IActionResult AdminRetailer()
        {
            return View(db.Retailers.ToList());
        }
        public IActionResult ProductsList()
        {
            return View(db.ProductDetails.ToList());
        }

        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            Admin admin = db.Admins.Find(id);            
            return View(admin);
        }

        [HttpPost]
        public IActionResult EditAdmin(Admin ad)
        {
            Admin admin = db.Admins.Find(ad.AdminId);
            admin.AdminName = ad.AdminName;
            admin.AdminPhoneNum = ad.AdminPhoneNum;
            admin.AdminEmail = ad.AdminEmail;
            db.SaveChanges();
            return View();
        }

        public IActionResult DeleteAdmin(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("AdmList");
        }

        [HttpGet]
        public IActionResult EditRetailer(int id)
        {
            Retailer r = db.Retailers.Find(id);          
            return View(r);
        }

        

        [HttpPost]
        public IActionResult EditRetailer(Retailer r)
        {
            Retailer retailer = db.Retailers.Find(r.RetailerId);
            retailer.ApprovedStatus = r.ApprovedStatus;
            db.SaveChanges();
            return RedirectToAction("AdminRetailer");

        }

        public IActionResult DeleteRetailer(int id)
        {
            Retailer retailer = db.Retailers.Find(id);
            db.Retailers.Remove(retailer);
            db.SaveChanges();
            return RedirectToAction("AdminRetailer");
        }
       
       
        
    }
}
