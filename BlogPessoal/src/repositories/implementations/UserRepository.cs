using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class UserRepository : IUser
    {
        #region Atributes


        private readonly BlogPessoalContext _context;

        #endregion Atributes


        #region Constructor
        public UserRepository(BlogPessoalContext context)
        {
            _context = context;
        }
        #endregion Constructor


        #region Methods
        public void NewUser(NewUserDTO user)
        {
            _context.User.Add(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Picture = user.Picture,
                Type = user.Type
            });
            _context.SaveChanges();

        }

        public void UpDateUser(UpDateUserDTO user)
        {
            var existingUser = GetUserByID(user.Id);
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Picture = user.Picture;
            _context.Update(existingUser);
            _context.SaveChanges();
            
        }

        public void DeleteUser(int id)
        {
            _context.User.Remove(GetUserByID(id));
            _context.SaveChanges();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserByID(int id)
        {
            return _context.User.FirstOrDefault(u => u.Id == id);
        }

        public List<UserModel> GetUserByName(string name)
        {
            return _context.User
                        .Where(u => u.Name.Contains(name))
                        .ToList();

        }



        #endregion Methods
    }
}
