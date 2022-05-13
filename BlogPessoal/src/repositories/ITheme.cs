using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
       
        public interface ITheme
    {
        Task NewThemeAsync(NewThemeDTO theme);
        Task UpDateThemeAsync(UpDateThemeDTO theme);
        Task DeleteThemeAsync(int id);
            
        Task<List<ThemeModel>> GetAllThemesAsync();
        Task<ThemeModel> GetThemeByIdAsync(int id);
        Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description);
    }
}
