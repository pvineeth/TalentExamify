using BCrypt.Net;
using Microsoft.Extensions.Configuration;



namespace Models.Utility
{
    public static class Helper
    {
        public static string SymmetricSecurityKey { get; set; }

        public static bool ValidateBCryptPassword(string plainPassword, string passwordHash)
        {
            var validatePassword = BCrypt.Net.BCrypt.EnhancedVerify(plainPassword, passwordHash, hashType: HashType.SHA512);
            return validatePassword;
        }
        public static void LoadConfigurations(IConfiguration configuration)
        {
            SymmetricSecurityKey = configuration["SecurityConfig:symmetricSecurityKey"];
        }
    }
}
