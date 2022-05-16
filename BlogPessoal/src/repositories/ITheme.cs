using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Represent the CRUID theme actions.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
        public interface ITheme
    {
        //Basic Operations
        Task NewThemeAsync(NewThemeDTO theme);
        Task UpDateThemeAsync(UpDateThemeDTO theme);
        Task DeleteThemeAsync(int id);
        
        //Search Methods
        Task<List<ThemeModel>> GetAllThemesAsync();
        Task<ThemeModel> GetThemeByIdAsync(int id);
        Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description);
    }
}
