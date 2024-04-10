using Models.DTOs.AuthenticationDTOs;
using Models.Reponses;


namespace Repository.AuthenticationRepository
{
    public  interface IAuthenticationRepository
    {
        Task<LoginResponse> Login(LoginDTO loginDetails);
    }
}
