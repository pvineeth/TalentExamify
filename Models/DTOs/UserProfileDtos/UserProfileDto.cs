using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.UserProfileDtos
{
    public class UserProfileDto
    {
        public string PKUserProfileId { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
    }
}
