using Models.DTOs;
using Models.DTOs.UserDTos;
using Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public interface IUserProfileRepository
    {
        Task<PaginationEntityDTO<UserProfileDto>> GetAllUserProfiles();
        Task<UserProfileDto> GetUserProfileById(string userProfileId);
        Task<Response> AddUserProfile(UserProfileDto userProfileDto);
        Task<Response> UpdateUserProfile(UserProfileDto userProfileDto);
        Task<Response> DeleteUserProfile(string userProfileId);
    }
}
