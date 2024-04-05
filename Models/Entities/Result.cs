using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Result :AuditColumns
    {
        [Key]
        public int Sl_No { get; set; }

        [MaxLength]
        public string SessionID { get; set; }
        public string CandidateID { get; set; }
        public string ExamID { get; set; }
        public string QuestionID { get; set; }
        public string AnswerID { get; set; }
        public string SelectedOptionID { get; set; }
        public bool IsCorrent { get; set; }
    }
}
