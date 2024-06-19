using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{

    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {

        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            this._context = context;
        }

        // METODO POST
        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            try 
            {

                _context.Countries.Add(country);

                await _context.SaveChangesAsync();
                return Ok(country);

            } 
            catch (DbUpdateException dbUpdateException)
            {
                
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {

                    return BadRequest("Ya existe un país con el mismo nombre");

                }

                //return BadRequest(dbUpdateException.Message);
                return BadRequest("Ya existe un país con el mismo nombre");

            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);

            }

        }

        // METODO GET
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries.ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if(country == null)
            {
                return NotFound();
            }

            return Ok(country);

        }

        // METODO PUT
        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {

            try
            {

                _context.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);

            }
            catch (DbUpdateException dbUpdateException)
            {

                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {

                    return BadRequest("Ya existe un país con el mismo nombre");

                }

                //return BadRequest(dbUpdateException.Message);
                return BadRequest("Ya existe un país con el mismo nombre");

            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);

            }

        }

        // METODO DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null) 
            { 

                return NotFound();

            }

            _context.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();

        }


    }
}
