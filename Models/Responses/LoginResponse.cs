using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reponses
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            ErrorMessage = string.Empty;
        }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; set; }
        public string JWTToken { get; set; }
    }
}
