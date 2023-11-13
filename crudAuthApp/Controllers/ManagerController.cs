using crudAuthApp.DTO;
using crudAuthApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crudAuthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class ManagerController : ControllerBase
    {
        private readonly Context _context;

        public ManagerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            var managers = await _context.Manager.ToListAsync();
            return Ok(managers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager([FromBody] ManagerDto manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newManager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = manager.ManagerName,
                Code = manager.ManagerCode,
                CreatedBy = manager.CreatedBy,
                CreatedDate = DateTime.Now
            };

            _context.Manager.Add(newManager);
            await _context.SaveChangesAsync();

            return Ok(new { manager = newManager });
        }

    }
}
