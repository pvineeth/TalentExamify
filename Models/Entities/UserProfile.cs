using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class UserProfile : AuditColumns
    {
        [Key]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string PKUserProfileId { get; set; }
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        [Required]
        public string EmailId { get; set; }
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        [Required]
        public string PasswordHash { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }
        [MaxLength]
        public string ImgFile { get; set; }

        [ForeignKey("FKRoleId")]
        public Role Role { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        [Required]
        public string FKRoleId { get; set; }
    }
}
