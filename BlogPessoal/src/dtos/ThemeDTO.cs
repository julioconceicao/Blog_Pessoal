using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Mirror class to CREATE  new theme.</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
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
    /// <para>Mirror class to UPDATE an existing theme.</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class UpDateThemeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }

        public UpDateThemeDTO(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
