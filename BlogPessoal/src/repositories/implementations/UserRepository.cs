using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>This class implements User.</para>
    /// <para>By: Julio Conceicao</para>
    /// <para>v 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class UserRepository : IUser
    {
    /// <summary>
    /// <para>Class responsible by implementing IUser interface.</para>
    /// <para>By: Julio C. Goncalves Conceicao</para>
    /// <para>V1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>

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

        /// <summary>
        /// <para>Async method to CREATE a new User.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
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
        
        /// <summary>
        /// <para>Async method to UPDATE an existing user.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task UpDateUserAsync(UpDateUserDTO user)
        {
            var existingUser = await GetUserByIdAsync(user.Id);
            
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Picture = user.Picture;
            _context.User.Update(existingUser);
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to DELETE an existing user.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task DeleteUserAsync(int id)
        {
            _context.User.Remove(await GetUserByIdAsync(id));
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to GET an existing user BY EMAIL.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Async method to GET an existing user BY ID.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Async method to GET an existing user BY NAME.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task<List<UserModel>> GetUserByNameAsync(string name)
        {
            return await _context.User
                        .Where(u => u.Name.Contains(name))
                        .ToListAsync();
        }

        #endregion Methods
    }
}
