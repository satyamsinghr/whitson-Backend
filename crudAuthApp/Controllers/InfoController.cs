using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using crudAuthApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;

namespace crudAuthApp.Controllers
{
    [Route("api/info")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class InfoController : ControllerBase
    {
        private readonly Context _context;

        public InfoController(Context context)
        {
            _context = context;
        }

        // GET: api/info
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Info>>> GetInfos()
        {
            return await _context.Infos.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Info>> GetInfo(Guid id)
        {
            var info = await _context.Infos.FindAsync(id);

            if (info == null)
            {
                return NotFound();
            }

            return info;
        }

  


        [HttpPost]
        public async Task<ActionResult<Info>> PostTodo(Info todo)
        {
            _context.Infos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfo", new { id = todo.Id }, todo);
        }


        // PUT: api/info/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfo(Guid id, Info info)
        {
            if (id != info.Id)
            {
                return BadRequest();
            }

            _context.Entry(info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Infos.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfo(Guid id)
        {
            var info = await _context.Infos.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }

            _context.Infos.Remove(info);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
