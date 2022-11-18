using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotnetFinalAssessment.Data;
using DotnetFinalAssessment.Models;

namespace DotnetFinalAssessment.Controllers
{
    public class OwnersController : Controller
    {
        private readonly Db_Context _context;

        public OwnersController(Db_Context context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
              return View(await _context.Owners.ToListAsync());
        }
    

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DriverLicense")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DriverLicense")] Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
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
            return View(owner);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Owners == null)
            {
                return Problem("Entity set 'Db_Context.Owners'  is null.");
            }
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
          return _context.Owners.Any(e => e.Id == id);
        }




        //API 
        [Route("api/owners")]
        public IActionResult IndexApi()
        {
            return Ok(_context.Owners);
        }

        [Route("api/owners/{Id}")]
        public IActionResult DetailsAPI(int Id)
        {
            return Ok(_context.Owners.Find(Id));
        }

        [HttpPost]
        [Route("api/owners")]
        public IActionResult CreateAPI([FromBody] Owner owner)

        {
            _context.Add(owner);

            _context.SaveChanges();
            return CreatedAtAction("createAPI", new { id = owner.Id }, owner);
        }

        [HttpPut]
        [Route("api/owners/{Id}")]
        public IActionResult UpdateAPI(int Id, [FromBody] Owner owner)

        {

            var currentOwner = _context.Owners.Find(Id);
            if (currentOwner != null)
            {
                currentOwner.FirstName = owner.FirstName;
                currentOwner.LastName = owner.LastName;
                currentOwner.DriverLicense = owner.DriverLicense;

            }

           _context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        [Route("api/owners/{Id}")]
        public IActionResult DeleteAPI(int Id)

        {

            var currentOwner = _context.Owners.Find(Id);
            if (currentOwner != null)
            {
                _context.Remove(currentOwner);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
