using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotnetFinalAssessment.Data;
using DotnetFinalAssessment.Models;

namespace DotnetFinalAssessment.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Db_Context _context;

        public VehiclesController(Db_Context context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var db_Context = _context.Vehicles.Include(v => v.Owner);
            return View(await db_Context.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

    
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Vin,Color,Year,OwnerId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", vehicle.OwnerId);
            return View(vehicle);
        }

 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", vehicle.OwnerId);
            return View(vehicle);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Vin,Color,Year,OwnerId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "FirstName", vehicle.OwnerId);
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'Db_Context.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return _context.Vehicles.Any(e => e.Id == id);
        }

        //API 
        [Route("api/vehicles")]
        public IActionResult IndexApi()
        {
            return Ok(_context.Vehicles);
        }

        [Route("api/vehicles/{Id}")]
        public IActionResult DetailsAPI(int Id)
        {
            return Ok(_context.Vehicles.Find(Id));
        }

        [HttpPost]
        [Route("api/vehicles")]
        public IActionResult CreateAPI([FromBody] Vehicle vehicle)

        {
            _context.Add(vehicle);

            _context.SaveChanges();
            return CreatedAtAction("createAPI", new { id = vehicle.Id }, vehicle);
        }

        [HttpPut]
        [Route("api/vehicles/{Id}")]
        public IActionResult UpdateAPI(int Id, [FromBody] Vehicle vehicle)

        {

            var currentVehicle = _context.Vehicles.Find(Id);
            if (currentVehicle != null)
            {
                currentVehicle.Brand = vehicle.Brand;
                currentVehicle.Vin = vehicle.Vin;
                currentVehicle.Color = vehicle.Color;
                currentVehicle.Year = vehicle.Year;
                currentVehicle.OwnerId = vehicle.OwnerId;

            }

            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        [Route("api/vehicles/{Id}")]
        public IActionResult DeleteAPI(int Id)

        {

            var currentVehicle = _context.Vehicles.Find(Id);
            if (currentVehicle != null)
            {
                _context.Remove(currentVehicle);
            }

            _context.SaveChanges();

            return NoContent();
        }
    }
}
