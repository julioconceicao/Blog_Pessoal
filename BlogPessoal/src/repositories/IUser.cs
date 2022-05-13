
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    public interface IUser
    {
        Task NewUserAsync(NewUserDTO user);
        Task UpDateUserAsync(UpDateUserDTO user);
        Task DeleteUserAsync(int id);

        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUserByNameAsync(string name);

    }
}
