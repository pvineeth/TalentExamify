using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.AuthenticationDTOs
{
    public class LoginDTO
    {
        [Required]
        [StringLength(50)]
        public string EmailId { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
