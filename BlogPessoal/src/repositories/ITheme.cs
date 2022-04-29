using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositories
{
    public class ITheme
    {
        /// <summary>
        /// <para>Represent the CRUID theme actions </para>
        /// <para>By: Julio Conceição</para>
        /// <para>v 1.0</para>
        /// <para>Apr.29.2022</para>
        /// </summary>
        public interface ITema
        {
            void NewTheme(NewThemeDTO theme);
            void UpDateTheme(UpDateThemeDTO theme);
            void DeleteTheme(int id);
            ThemeModel GetThemeByID(int id);
            List<ThemeModel> GetThemeByDescription(string description);
        }

    }
}
