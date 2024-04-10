using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.UserDTos;
using Models.Reponses;
using Repository.UserRepository;

namespace TalentExamify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        [HttpGet]
        public ActionResult<PaginationEntityDTO<UserProfileDto>> GetAllUserProfiles()
        {
            var userProfiles = _userProfileRepository.GetAllUserProfiles();
            return Ok(userProfiles);
        }
        [HttpPost]
        public async Task<ActionResult> AddUserProfile([FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userProfileRepository.AddUserProfile(userProfileDto);
            return Ok(result);
        }

        [HttpPut("{userProfileId}")]
        public async Task<ActionResult<Response>> UpdateUserProfile(string userProfileId, [FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userProfileDto.PKUserProfileId != userProfileId)
                return BadRequest("UserProfile ID mismatch");

            try
            {
                var res = await _userProfileRepository.UpdateUserProfile(userProfileDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("{userProfileId}")]
        public async Task<ActionResult> DeleteUserProfile(string userProfileId)
        {
            var existingUserProfile = _userProfileRepository.GetUserProfileById(userProfileId);
            if (existingUserProfile == null)
                return NotFound();

           var res=await _userProfileRepository.DeleteUserProfile(userProfileId);
            return Ok(res);
        }

    }
}
