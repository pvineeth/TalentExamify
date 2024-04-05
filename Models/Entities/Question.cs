using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Question : AuditColumns
    {
        [Key]
        public string QuestionID { get; set; }
        public string QuestionType { get; set; }  //MCQ-1      
        public string DisplayText { get; set; }

        [ForeignKey("ExamID")]
        public Exam Exam { get; set; }
        public string ExamID { get; set; }
    }
}
