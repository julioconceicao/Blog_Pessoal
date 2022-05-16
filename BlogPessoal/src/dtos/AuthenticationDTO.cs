using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    
    /// <summary>
    /// <para>Mirror class responsible by the USER AUTHENTICATION</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class AuthenticationDTO

    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    /// <summary>
    /// <para>Mirror class responsible by the AUTHORIZATION</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string Token { get; set; }

        public AuthorizationDTO(int id, string email, UserType type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}

