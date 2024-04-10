using Models.DTOs;
using Models.DTOs.RoleDTOs;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RoleRepository
{
    public interface IRoleRepository
    {
        Task<PaginationEntityDTO<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> GetRoleByIdAsync(string roleId);
        Task<Response> AddRoleAsync(CreateRoleDTO role);
        Task<Response> UpdateRoleAsync(UpdateRoleDTO role);
        Task<Response> DeleteRoleAsync(string roleId);
    }
}
