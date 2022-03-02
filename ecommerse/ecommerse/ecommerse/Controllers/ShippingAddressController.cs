#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ecommerse.Models;

namespace ecommerse.Controllers
{
    public class ShippingAddressController : Controller
    {
        private readonly shoppingDB1Context _context;

        public ShippingAddressController(shoppingDB1Context context)
        {
            _context = context;
        }

        // GET: ShippingAddress
        public async Task<IActionResult> Index()
        {
            var shoppingDB1Context = _context.ShippingAddresses.Include(s => s.User);
            return View(await shoppingDB1Context.ToListAsync());
        }

        // GET: ShippingAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddresses
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            return View(shippingAddress);
        }

        // GET: ShippingAddress/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId");
            return View();
        }

        // POST: ShippingAddress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,City,Village,Landmark,Pincode,UserId")] ShippingAddress shippingAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingAddress);
                await _context.SaveChangesAsync();
                return View("Order");
            }
            ViewData["UserId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", shippingAddress.UserId);
            return View(shippingAddress);
        }

        // GET: ShippingAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);
            if (shippingAddress == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", shippingAddress.UserId);
            return View(shippingAddress);
        }

        // POST: ShippingAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,City,Village,Landmark,Pincode,UserId")] ShippingAddress shippingAddress)
        {
            if (id != shippingAddress.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingAddressExists(shippingAddress.AddressId))
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
            ViewData["UserId"] = new SelectList(_context.CustomerDetails, "CustId", "CustId", shippingAddress.UserId);
            return View(shippingAddress);
        }

        // GET: ShippingAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddresses
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            return View(shippingAddress);
        }

        // POST: ShippingAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);
            _context.ShippingAddresses.Remove(shippingAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingAddressExists(int id)
        {
            return _context.ShippingAddresses.Any(e => e.AddressId == id);
        }
    }
}
