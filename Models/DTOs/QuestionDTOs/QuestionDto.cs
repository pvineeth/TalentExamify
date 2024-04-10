using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.QuestionDTOs
{
    public class QuestionDto
    {
        public string QuestionID { get; set; }
        public string QuestionType { get; set; }
        public string DisplayText { get; set; }
        public string ExamID { get; set; }
        public string ExamName { get; set; }
    }
}
