using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ease.Data;
using Ease.Services;
using Microsoft.IdentityModel.Tokens;

namespace Ease.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasyMetadatasController : ControllerBase
    {
        private readonly DataContext _context;

        public EasyMetadatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EasyMetadatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EasyMetadata>>> GetEasyMetadata()
        {
            return await _context.EasyMetadata.ToListAsync();
        }

        // GET: api/EasyMetadatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EasyMetadata>> GetEasyMetadata(string id)
        {
            var easyMetadata = await _context.EasyMetadata.FindAsync(id);

            if (easyMetadata == null || !Utils.IsValidGuid(id))
            {
                return NotFound();
            }

            return easyMetadata;
        }

        // PUT: api/EasyMetadatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEasyMetadata(string id, EasyMetadata updatedEasyMetadata)
        {
            if (updatedEasyMetadata.Expires == null || updatedEasyMetadata.Expires == DateTime.MinValue)
            {
                return BadRequest();
            }
            var easyMetadata = await _context.EasyMetadata.FindAsync(id);
            if (easyMetadata == null)
            {
                return NotFound();
            }
            easyMetadata.Expires = updatedEasyMetadata.Expires;
            _context.Entry(easyMetadata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EasyMetadataExists(id))
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

        // POST: api/EasyMetadatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EasyMetadata>> PostEasyMetadata(EasyMetadata easyMetadata)
        {

            if (!string.IsNullOrEmpty(easyMetadata.Guid) || easyMetadata.Expires != null || (string.IsNullOrEmpty(easyMetadata.User)))
            {
                return BadRequest();
            }
            easyMetadata.Guid = Guid.NewGuid().ToString("N").ToUpper();
            easyMetadata.Expires = DateTime.UtcNow.AddDays(30);
            
            _context.EasyMetadata.Add(easyMetadata);
            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EasyMetadataExists(easyMetadata.Guid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEasyMetadata", new { id = easyMetadata.Guid }, easyMetadata);
        }

        // POST: api/EasyMetadatas/guid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<EasyMetadata>> PostEasyMetadata(string id, EasyMetadata easyMetadata)
        {

            if (!string.IsNullOrEmpty(easyMetadata.Guid) || easyMetadata.Expires == null || easyMetadata.Expires == DateTime.MinValue || string.IsNullOrEmpty(easyMetadata.User)
                            || string.IsNullOrEmpty(id) || id.Equals(easyMetadata.Guid))
            {
                return BadRequest();
            }
            easyMetadata.Guid = id;

            _context.EasyMetadata.Add(easyMetadata);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EasyMetadataExists(easyMetadata.Guid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEasyMetadata", new { id = easyMetadata.Guid }, easyMetadata);
        }

        // DELETE: api/EasyMetadatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEasyMetadata(string id)
        {
            var easyMetadata = await _context.EasyMetadata.FindAsync(id);
            if (easyMetadata == null)
            {
                return NotFound();
            }

            _context.EasyMetadata.Remove(easyMetadata);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EasyMetadataExists(string id)
        {
            return _context.EasyMetadata.Any(e => e.Guid == id);
        }
    }
}
