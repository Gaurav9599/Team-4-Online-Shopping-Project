using AdminFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminFinal.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly shoppingDB1Context db;

        public HomeController(shoppingDB1Context context)
        {
            db = context;
        }
        public IActionResult Index2()
        {            
            return View(db.ProductDetails.ToList());
        }
        public IActionResult DisplayDetails(int id)
        {
            return View(db.ProductDetails.Find(id));

        }
        public IActionResult GetProductsByCategory(string category)
        {
            var products = db.ProductDetails.Where(query => query.ProductCategory.Contains(category));
            
            if(products == null)
            {
                return Content("Oops! No matching product was found");
            }
            return View(products.ToList());
        }

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