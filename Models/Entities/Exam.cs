using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Exam:AuditColumns
    {
        [Key]
        public string ExamID { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FullMarks { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Duration { get; set; }
    }
}
