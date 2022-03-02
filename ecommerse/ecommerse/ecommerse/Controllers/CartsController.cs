using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerse.Models;
using Newtonsoft.Json;

namespace ecommerse.Controllers
{
    public class CartsController : Controller
    {
        private readonly shoppingDB1Context _context;

        public CartsController(shoppingDB1Context context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var shoppingDB1Context = _context.Carts.Include(c => c.Customer).Include(c => c.Product);
            return View(await shoppingDB1Context.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId");
            ViewData["ProductId"] = new SelectList(_context.ProductDetails, "ProductId", "ProductId");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,CustomerId,ProductId,Quantity,TotalPrice")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                var pdt = _context.ProductDetails.Find(cart.ProductId);             
                cart.TotalPrice = (int?)(cart.Quantity * (pdt.ProductPrice));
                _context.Add(cart);               
                await _context.SaveChangesAsync();               
                return View("Payment" , cart);
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.ProductDetails, "ProductId", "ProductId", cart.ProductId);
            return View();
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.ProductDetails, "ProductId", "ProductId", cart.ProductId);
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,CustomerId,ProductId,Quantity,TotalPrice")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.ProductDetails, "ProductId", "ProductId", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }

        public IActionResult Payment(Cart cart)
        {
            return View(cart);
        }
    }
}
