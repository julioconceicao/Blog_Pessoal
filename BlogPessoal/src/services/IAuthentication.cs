using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
    /// <summary>
    /// <para>Represent the CRUID Athentication actions.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    
        string EncodePassword(string password);
        Task CreateUserWithoutDuplicateAsync(NewUserDTO dto);
        string GenerateToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto);
    }
}
