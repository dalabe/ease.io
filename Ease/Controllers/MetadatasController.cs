using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ease.Data;
using Microsoft.CodeAnalysis;

namespace Ease.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadatasController : ControllerBase
    {
        private readonly DataContext _context;

        public MetadatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Metadatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EasyMetadata>>> GetMetadata()
        {
            return await _context.Metadata.ToListAsync();
        }

        // GET: api/Metadatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EasyMetadata>> GetMetadata(Guid id)
        {
            var metadata = await _context.Metadata.FindAsync(id);

            if (metadata == null)
            {
                return NotFound();
            }

            return metadata;
        }

        // PUT: api/Metadatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetadata(Guid id, EasyMetadata metadata)
        {
            if (id != metadata.EaseGuid)
            {
                return BadRequest();
            }

            _context.Entry(metadata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetadataExists(id))
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

        // POST: api/Metadatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EasyMetadata>> PostMetadata(EasyMetadata metadata)
        {
            _context.Metadata.Add(metadata);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetadata", new { id = metadata.EaseGuid }, metadata);
        }

        // POST: api/Metadatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<EasyMetadata>> PostMetadata(Guid id, EasyMetadata metadata)
        {
            if (id != metadata.EaseGuid)
            {
                return BadRequest();
            }

            EasyMetadata metadata2 = new EasyMetadata(id, metadata.UserName);
            _context.Metadata.Add(metadata2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetadata", new { id = metadata2.EaseGuid }, metadata2);
        }

        // DELETE: api/Metadatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetadata(Guid id)
        {
            var metadata = await _context.Metadata.FindAsync(id);
            if (metadata == null)
            {
                return NotFound();
            }

            _context.Metadata.Remove(metadata);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetadataExists(Guid id)
        {
            return _context.Metadata.Any(e => e.EaseGuid == id);
        }
    }
}
