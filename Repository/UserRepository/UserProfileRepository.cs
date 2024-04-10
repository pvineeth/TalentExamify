using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.DTOs.RoleDTOs;
using Models.DTOs.UserDTos;
using Models.Entities;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.UserRepository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly TalentExamifyContext _context;

        public UserProfileRepository(TalentExamifyContext context)
        {
            _context = context;
        }
        public async Task<PaginationEntityDTO<UserProfileDto>> GetAllUserProfiles()
        {
            var result = _context.UserProfiles
                           .Include(u => u.Role)
                           .Select(u => new UserProfileDto
                           {
                               PKUserProfileId = u.PKUserProfileId,
                               EmailId = u.EmailId,
                               Phone = u.Phone,
                               ImgFile = u.ImgFile,
                               RoleId = u.FKRoleId,
                               RoleName = u.Role.RoleName
                           });
            return new PaginationEntityDTO<UserProfileDto>
            {
                Entities = await result.ToListAsync(),
                TotalEntityCount = await result.CountAsync()
            };
        }

        public async Task<UserProfileDto> GetUserProfileById(string userProfileId)
        {
            return await _context.UserProfiles
                           .Include(u => u.Role)
                           .Where(u => u.PKUserProfileId == userProfileId)
                           .Select(u => new UserProfileDto
                           {
                               PKUserProfileId = u.PKUserProfileId,
                               EmailId = u.EmailId,
                               Phone = u.Phone,
                               ImgFile = u.ImgFile,
                               RoleId = u.FKRoleId,
                               RoleName = u.Role.RoleName
                           })
                           .FirstOrDefaultAsync();
        }
        public async Task<Response> AddUserProfile(UserProfileDto userProfileDto)
        {
            var userProfile = new UserProfile
            {
                PKUserProfileId = userProfileDto.PKUserProfileId,
                EmailId = userProfileDto.EmailId,
                Phone = userProfileDto.Phone,
                ImgFile = userProfileDto.ImgFile,
                FKRoleId = userProfileDto.RoleId
            };

            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> UpdateUserProfile(UserProfileDto userProfileDto)
        {
            var userProfile = _context.UserProfiles.Find(userProfileDto.PKUserProfileId);
            if (userProfile == null)
                return new Response { ErrorMessage = "UserProfile not found" };

            userProfile.EmailId = userProfileDto.EmailId;
            userProfile.Phone = userProfileDto.Phone;
            userProfile.ImgFile = userProfileDto.ImgFile;
            userProfile.FKRoleId = userProfileDto.RoleId;

            await _context.SaveChangesAsync();
            return new Response();
        }
        public async Task<Response> DeleteUserProfile(string userProfileId)
        {
            var userProfileToDelete = _context.UserProfiles.Find(userProfileId);
            if (userProfileToDelete == null)
                return new Response() { ErrorMessage = "User not Found" };

            _context.UserProfiles.Remove(userProfileToDelete);
            await _context.SaveChangesAsync();
            return new Response();
            
        }
    }
}
