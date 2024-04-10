using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.UserDTos
{
    public class UserProfileDto
    {
        public string PKUserProfileId { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public string ImgFile { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
