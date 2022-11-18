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
    public class ClaimsController : Controller
    {
        private readonly Db_Context _context;

        public ClaimsController(Db_Context context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
            var db_Context = _context.Claims.Include(c => c.Vehicle);
            return View(await db_Context.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

       
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Vin");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Status,Date,VehicleId")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Vin", claim.VehicleId);
            return View(claim);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Vin", claim.VehicleId);
            return View(claim);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Status,Date,VehicleId")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Vin", claim.VehicleId);
            return View(claim);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claims == null)
            {
                return NotFound();
            }

            var claim = await _context.Claims
                .Include(c => c.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claims == null)
            {
                return Problem("Entity set 'Db_Context.Claims'  is null.");
            }
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(int id)
        {
          return _context.Claims.Any(e => e.Id == id);
        }


        //API 
        [Route("api/claims")]
        public IActionResult IndexApi()
        {
            return Ok(_context.Claims);
        }

        [Route("api/claims/{Id}")]
        public IActionResult DetailsAPI(int Id)
        {
            return Ok(_context.Claims.Find(Id));
        }

        [HttpPost]
        [Route("api/claims")]
        public IActionResult CreateAPI([FromBody] Claim claim)

        {
            _context.Add(claim);

            _context.SaveChanges();
            return CreatedAtAction("createAPI", new { id = claim.Id }, claim);
        }

        [HttpPut]
        [Route("api/claims/{Id}")]
        public IActionResult UpdateAPI(int Id, [FromBody] Claim claim)

        {

            var currentClaim = _context.Claims.Find(Id);
            if (currentClaim != null)
            {
                currentClaim.Description = claim.Description;
                currentClaim.Status = claim.Status;
                currentClaim.Date = claim.Date;
                currentClaim.VehicleId = claim.VehicleId;

            }

            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        [Route("api/claims/{Id}")]
        public IActionResult DeleteAPI(int Id)

        {

            var currentClaim= _context.Claims.Find(Id);
            if (currentClaim != null)
            {
                _context.Remove(currentClaim);
            }

            _context.SaveChanges();

            return NoContent();
        }

    }
}
