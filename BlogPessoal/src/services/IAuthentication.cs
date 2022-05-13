using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserWithoutDuplicateAsync(NewUserDTO dto);
        string GenerateToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto);
    }
}
