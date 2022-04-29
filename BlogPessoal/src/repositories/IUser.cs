
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{

    /// <summary>
    /// <para>Represent the CRUID USER ACTIONS.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public interface IUser
    {
        void AddNewUser(NewUserDTO user);

        void UpDateUser(UpDateUserDTO user);

        void DeleteUser(int id);
        UserModel GetUserByID(int id);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByName(string nome);

    }
}
