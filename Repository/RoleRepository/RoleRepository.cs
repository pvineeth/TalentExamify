using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.DTOs.RoleDTOs;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TalentExamifyContext _context;

        public RoleRepository(TalentExamifyContext context)
        {
            _context = context;
        }

        public async Task<PaginationEntityDTO<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return new PaginationEntityDTO<RoleDTO>
            {
                Entities= roles.Select(r => MapRoleToDTO(r)).ToList(),
                TotalEntityCount= roles.Count
            };
            
        }
        public async Task<RoleDTO> GetRoleByIdAsync(string roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return MapRoleToDTO(role);
        }
        public async Task<Response> AddRoleAsync(CreateRoleDTO role)
        {
            var roleEntity = new Role
            {
                PKRoleId = Guid.NewGuid().ToString(),
                RoleName = role.RoleName,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "N/A"
            };
            await _context.Roles.AddAsync(roleEntity);
            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> UpdateRoleAsync(UpdateRoleDTO role)
        {
            var roleEntity = await _context.Roles.FindAsync(role.PKRoleId);
            if (roleEntity == null)
                return new Response { ErrorMessage = "Role not found" };

            roleEntity.RoleName = role.RoleName;
            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> DeleteRoleAsync(string roleId)
        {
            var roleToDelete = await _context.Roles.FindAsync(roleId);
            if (roleToDelete == null)
                return new Response { ErrorMessage = "Role not found" };

            _context.Roles.Remove(roleToDelete);
            await _context.SaveChangesAsync();
            return new Response();
        }
        private RoleDTO MapRoleToDTO(Role role)
        {
            if (role == null)
                return null;
            return new RoleDTO
            {
                PKRoleId = role.PKRoleId,
                RoleName = role.RoleName
            };
        }
    }
}
