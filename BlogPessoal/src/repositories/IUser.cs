
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Represent the CRUID user actions.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public interface IUser
    {
        //Basic Operations
        Task NewUserAsync(NewUserDTO user);
        Task UpDateUserAsync(UpDateUserDTO user);
        Task DeleteUserAsync(int id);
        
        //Search Methods
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUserByNameAsync(string name);

    }
}
