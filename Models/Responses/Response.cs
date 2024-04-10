using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reponses
{
    public class Response
    {
        public Response()
        {
            ErrorMessage = string.Empty;
        }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; set; }
    }
}
