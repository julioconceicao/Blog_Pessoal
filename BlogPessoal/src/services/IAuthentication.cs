using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserWithoutDuplicateAync(NewUserDTO user);
        string GenerateToken(UserModel user);
        AuthorizationDTO GetAuthorization(AuthenticationDTO authentication);
    }
}
