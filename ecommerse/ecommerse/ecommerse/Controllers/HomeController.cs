using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ecommerse.Models;
using System.Diagnostics;

namespace ecommerse.Controllers
{
    public class HomeController : Controller
    {
        private readonly shoppingDB1Context db;

        public HomeController(shoppingDB1Context context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public ActionResult HomeIndex()
        {
            return View(db.ProductDetails.ToList());
        }
        public IActionResult DisplayDetails(int id)
        {
            return View(db.ProductDetails.Find(id));

        }
        public IActionResult GetProductsByCategory(string category)
        {
            var products = db.ProductDetails.Where(query => query.ProductCategory.Equals(category));

            if (products == null)
            {
                return Content("Oops! No matching product was found");
            }
            return View(products.ToList());
        }
        public IActionResult AddToCart(int id)
        {
            return View(db.ProductDetails.Find(id));

        }

        //[HttpGet]
        //public IActionResult AddToCart2(int id)
        //{
        //    //var product = db.ProductDetails.Find(id);
        //    //db.Carts.            
        //    var cust = JsonConvert.DeserializeObject<CustomerDetail>((string)TempData["Customer"]);
        //    ViewBag.CustomerId = cust.CustId;
        //    ViewBag.Products = id;
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult AddToCart2(Cart cart)
        //{
        //    db.Carts.Add(cart);
        //    db.SaveChanges();
        //    return View("HomeIndex");
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}