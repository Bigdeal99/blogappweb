using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service;
using System.Threading.Tasks;

namespace library.Controllers
{
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _adminService.GetAdminByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] Admin admin)
        {
            var createdAdmin = await _adminService.CreateAdminAsync(admin);
            return CreatedAtAction(nameof(GetAdmin), new { id = createdAdmin.Id }, createdAdmin);
        }

       

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _adminService.GetAdminByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            await _adminService.DeleteAdminAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var isAuthenticated = await _adminService.AuthenticateAdminAsync(model.Username, model.Password);

            if (isAuthenticated)
            {
                return Ok(new { MessageToClient = "Successfully logged in" });
            }

            return Unauthorized(new { MessageToClient = "Invalid login attempt" });
        }

        // Additional methods can be added here as needed
    }
}
