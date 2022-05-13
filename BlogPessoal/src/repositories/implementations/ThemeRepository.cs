

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

        public async Task NewThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description,
            });
            
           await _context.SaveChangesAsync();
        }

        public async Task UpDateThemeAsync(UpDateThemeDTO theme)
        {
            var existingTheme = await GetThemeByIdAsync(theme.Id);
            
            existingTheme.Description = theme.Description;
            
            _context.Themes.Update(existingTheme);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));

           await _context.SaveChangesAsync();
        }

        //Search by themes.
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

        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(u => u.Id == id);
        }

        #endregion Methods
    }
}
