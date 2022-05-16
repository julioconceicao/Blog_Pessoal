

using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>Class responsible by implementing ITheme interface.</para>
    /// <para>By: Julio C. Goncalves Conceicao</para>
    /// <para>V1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class ThemeRepository : ITheme
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion 

        #region Constructor
        public ThemeRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion 

#region Methods

        /// <summary>
        /// <para>Async method to save a new THEME</para>
        /// </summary>
        /// <param name="theme">NewThemeDTO</param>
        public async Task NewThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description,
            });
            
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to update an existing theme</para>
        /// </summary>
        /// <param name="theme">UpDateThemeDTO</param>
        public async Task UpDateThemeAsync(UpDateThemeDTO theme)
        {
            var existingTheme = await GetThemeByIdAsync(theme.Id);
            
            existingTheme.Description = theme.Description;
            
            _context.Themes.Update(existingTheme);
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to delete an existing theme</para>
        /// </summary>
        /// <param name="id">Theme Id</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));

           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to GET all themes</para>
        /// </summary>
        /// <return>ThemeModel list</return>
        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes 
                    .Where(u => u.Description.Contains(description))
                    .ToListAsync();
        }

        /// <summary>
        /// <para>Async method to GET a theme by Id</para>
        /// </summary>
        /// <param name="id">Theme Id</param>
        /// <return>ThemeModel</return>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(u => u.Id == id);
        }

        #endregion Methods
    }
}
