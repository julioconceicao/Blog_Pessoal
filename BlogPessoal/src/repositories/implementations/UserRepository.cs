using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task NewUserAsync(NewUserDTO user)
        {
           await _context.User.AddAsync(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Picture = user.Picture,
                Type = user.Type
            });
           await _context.SaveChangesAsync();

        }

        public async Task UpDateUserAsync(UpDateUserDTO user)
        {
            var existingUser = await GetUserByIdAsync(user.Id);
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Picture = user.Picture;
            _context.Update(existingUser);
            await _context.SaveChangesAsync();
            
        }

        public async Task DeleteUserAsync(int id)
        {
            _context.User.Remove(await GetUserByIdAsync(id));
            _context.SaveChanges();
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserModel>> GetUserByNameAsync(string name)
        {
            return await _context.User
                        .Where(u => u.Name.Contains(name))
                        .ToListAsync();

        }

        #endregion Methods
    }
}
