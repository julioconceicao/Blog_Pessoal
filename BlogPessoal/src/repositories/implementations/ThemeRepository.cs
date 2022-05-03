

using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class ThemeRepository : ITheme
    {
        #region Atributes

        private readonly BlogPessoalContext _context;

        #endregion Atributes

        #region Constructor
        public ThemeRepository(BlogPessoalContext context)
        {
            _context = context;
        }
        #endregion Constructor

#region Methods

      
        //Operations with theme
        public void NewTheme(NewThemeDTO theme)
        {
            _context.Themes.Add(new ThemeModel
            {
                Description = theme.Description,
            });
            _context.SaveChanges();
        }

        
        public void UpDateTheme(UpDateThemeDTO theme)
        {
            var existingTheme = GetThemeByID(theme.Id);
            existingTheme.Description = theme.Description;
            _context.Themes.Update(existingTheme);
            _context.SaveChanges();
        }

    

        public void DeleteTheme(int id)
        {
            _context.Themes.Remove(GetThemeByID(id));
            _context.SaveChanges();
        }

        //Search by themes.


        //public List<ThemeModel> GetAllThemes(int id)
        //{
        //    return _context.Themes.ToList();
        //}

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes 
                    .Where(u => u.Description.Contains(description))
                    .ToList();
        }

        public ThemeModel GetThemeByID(int id)
        {
            return _context.Themes.FirstOrDefault(u => u.Id == id);
        }
        #endregion Methods

    }
}
