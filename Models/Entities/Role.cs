using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Role : AuditColumns
    {
        [Key]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string PKRoleId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        [Required]
        public string RoleName { get; set; }

        [InverseProperty("role")]
        public IList<UserProfile> UserProfiles { get; set; }
    }
}
