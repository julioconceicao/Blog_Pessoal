using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Mirror class to CREATE A NEW THEME.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public class NewThemeDTO
    {
        [Required, StringLength(20)]
        public string Description { get; set; }
        public NewThemeDTO(string description)
        {
            Description = description;
        }

    }


    /// <summary>
    /// <para>Mirror class to UPDATE A THEME.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public class UpDateThemeDTO
    {
        [Required, StringLength(20)]
        public string Description { get; set; }
        public UpDateThemeDTO(string description)
        {
            Description = description;
        }

    }
}
