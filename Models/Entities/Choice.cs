using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Choice :AuditColumns
    {
        [Key]
        public string ChoiceID { get; set; }
        public string QuestionID { get; set; }
        public string DisplayText { get; set; }
    }
}
