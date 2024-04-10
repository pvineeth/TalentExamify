using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.AuthenticationDTOs;
using Models.Entities;
using Models.Reponses;
using Models.Utility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalentExamifyEFCore;

namespace Repository.AuthenticationRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly TalentExamifyContext _context;

        public AuthenticationRepository(TalentExamifyContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Login(LoginDTO loginDetails)
        {
            var existingUser = await _context.UserProfiles.Include(x => x.Role).Where(x => x.IsActive && !x.IsDelete).FirstOrDefaultAsync(e => e.EmailId.ToLower() == loginDetails.EmailId.ToLower());
            if (existingUser != null)
            {
                bool isValidPassword = Helper.ValidateBCryptPassword(loginDetails.Password, existingUser.PasswordHash);
                if (isValidPassword)
                {
                    var jwtToken = GenerateJwtToken(existingUser);
                    return new LoginResponse { JWTToken = jwtToken };
                }
                else
                {
                    return new LoginResponse
                    {
                        ErrorMessage = "Invalid Password."
                    };
                }
            }
            else
            {
                return new LoginResponse
                {
                    ErrorMessage = "Invalid Credentials."
                };
            }
        }

        private string GenerateJwtToken(UserProfile user)
        {
            var claims = new[]
            {
               new Claim(ClaimTypes.Role, user.Role.RoleName),
               new Claim(ClaimTypes.PrimarySid, user.PKUserProfileId),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Helper.SymmetricSecurityKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Helper.SymmetricSecurityKey,
                claims: claims,
                signingCredentials: signature,
                expires: DateTime.UtcNow.AddDays(7));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
