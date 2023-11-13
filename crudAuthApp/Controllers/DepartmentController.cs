using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using crudAuthApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using crudAuthApp.DTO;

namespace crudAuthApp.Controllers
{
    [Route("api/departments")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly Context _context;

        public DepartmentController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.Include(x => x.Manager).ToListAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            var department = await _context.Departments.Include(x => x.Manager).FirstOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDept = new Department
            {
                Id = Guid.NewGuid(),
                DeptName = department.DeptName,
                DeptCode = department.DeptCode,
                ManagerId = department.ManagerId,
                CreatedBy = department.CreatedBy,
                CreatedDate = DateTime.Now,
            };


            _context.Departments.Add(newDept);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new
            {
                id = newDept.Id
            }, newDept);
        }
    }
}
