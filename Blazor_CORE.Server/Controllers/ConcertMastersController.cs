using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_CORE.Shared.Models;

namespace Blazor_CORE.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/ConcertMasters")]
    public class ConcertMastersController : Controller
    {
        //DI
        private readonly ConcertManagementContext _context;

        public ConcertMastersController(ConcertManagementContext context) => _context = context;


        // GET: api/ConcertMasters
        /// <summary>
        /// API MEthod for getting concerts from database
        /// </summary>
        /// <returns>ConcertMasters table data</returns>
        [HttpGet]
        public IEnumerable<ConcertMasters> GetConcertMasters()
        {
            return _context.ConcertMasters;
        }

        // GET: api/ConcertMasters/5
        /// <summary>
        /// API MEthod for getting concerts from database based on parameter id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcertMasters([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concertMasters = await _context.ConcertMasters.SingleOrDefaultAsync(c => c.ConcertNo == id);

            if (concertMasters == null)
            {
                return NotFound();
            }

            return Ok(concertMasters);
        }

        // PUT: api/ConcertMasters/5
        /// <summary>
        /// API MEthod for updating concert, based on parameters id, concertMasters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concertMasters"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcertMasters([FromRoute] int id, [FromBody] ConcertMasters concertMasters)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != concertMasters.ConcertNo)
                {
                    return BadRequest();
                }

                
                _context.Entry(concertMasters).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertMastersExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
               string message = ex.Message;
            }

            return NoContent();
        }

        // POST: api/ConcertMasters
        /// <summary>
        /// API MEthod for creating new concert, based on parameter concertMasters
        /// </summary>
        /// <param name="concertMasters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostConcertMasters([FromBody] ConcertMasters concertMasters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ConcertMasters.Add(concertMasters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConcertMasters", new { id = concertMasters.ConcertNo }, concertMasters);
        }

        // DELETE: api/ConcertMasters/5
        /// <summary>
        /// API MEthod for deleting concert, based on parameter id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcertMasters([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concertMasters = await _context.ConcertMasters.FindAsync(id);
            if (concertMasters == null)
            {
                return NotFound();
            }

            _context.ConcertMasters.Remove(concertMasters);
            await _context.SaveChangesAsync();

            return Ok(concertMasters);
        }
        
        /// <summary>
        /// Helper method for checking if concert alreadz exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ConcertMastersExists(int id)
        {
            return _context.ConcertMasters.Any(e => e.ConcertNo == id);
        }

    }
}