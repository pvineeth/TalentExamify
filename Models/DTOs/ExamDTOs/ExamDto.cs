using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.ExamDTOs
{
    public class ExamDto
    {
        public string ExamID { get; set; }
        public string Name { get; set; }
        public decimal FullMarks { get; set; }
        public decimal Duration { get; set; }
    }
}
