using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.RoleDTOs;
using Repository.RoleRepository;

namespace TalentExamify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet]
        public async Task<ActionResult<PaginationEntityDTO<RoleDTO>>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(string id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var response= await _roleRepository.AddRoleAsync(role);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(string id, [FromBody] UpdateRoleDTO role)
        {
            if (id != role.PKRoleId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _roleRepository.UpdateRoleAsync(role);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var response = await _roleRepository.DeleteRoleAsync(id);
            return Ok(response);
        }
    }
}
