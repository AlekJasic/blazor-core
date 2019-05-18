using Blazor_CORE.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_CORE.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConcertDetailsController : Controller
    {
        private readonly ConcertManagementContext _context;

        public ConcertDetailsController(ConcertManagementContext context) => _context = context;

        /// <summary>
        /// API MEthod for getting concert details from database
        /// </summary>
        /// <returns>ConcertDetails table data</returns>
        // GET: api/ConcertDetails
        [HttpGet]
        public IEnumerable<ConcertDetails> GetConcertDetails()
        {
            return _context.ConcertDetails;
        }

        /// <summary>
        /// API MEthod for getting concerts details from database based on parameter id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ConcertDetails table data</returns>
        // GET: api/ConcertDetails/5
        [HttpGet("{id}")]
        public IEnumerable<ConcertDetails> GetConcertDetails([FromRoute] int id)
        {
            var concertDetails = _context.ConcertDetails.Where(i => i.ConcertNo == id).ToList();
            return concertDetails;
        }

        /// <summary>
        /// API MEthod for updating concert detail, based on parameters id and mode
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concertMasters"></param>
        /// <returns>Task<T></returns>
        // GET: api/ConcertDetail/5/update  
        [HttpGet("{id}/{mode}")]
        public async Task<IActionResult> GetConcertDetailForEdit([FromRoute] int id, [FromRoute] string mode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concertDetails = await _context.ConcertDetails.SingleOrDefaultAsync(m => m.ConcertDetailNo == id);

            if (concertDetails == null)
            {
                return NotFound();
            }

            return Ok(concertDetails);
        }
        /// <summary>
        /// API MEthod for updating concert detail, based on parameters id and concertDetails
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concertDetails"></param>
        /// <returns>Task<T></returns>

        // PUT: api/ConcertDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcertDetails([FromRoute] int id, [FromBody] ConcertDetails concertDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != concertDetails.ConcertDetailNo)
            {
                return BadRequest();
            }

            _context.Entry(concertDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcertDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ConcertDetails
        [HttpPost]
        public async Task<IActionResult> PostConcertDetails([FromBody] ConcertDetails concertDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ConcertDetails.Add(concertDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConcertDetails", new { id = concertDetails.ConcertDetailNo }, concertDetails);
        }

        // DELETE: api/ConcertDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcertDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var concertDetails = await _context.ConcertDetails.FindAsync(id);
            if (concertDetails == null)
            {
                return NotFound();
            }

            _context.ConcertDetails.Remove(concertDetails);
            await _context.SaveChangesAsync();

            return Ok(concertDetails);
        }

        private bool ConcertDetailsExists(int id)
        {
            return _context.ConcertDetails.Any(e => e.ConcertDetailNo == id);
        }
    }
}