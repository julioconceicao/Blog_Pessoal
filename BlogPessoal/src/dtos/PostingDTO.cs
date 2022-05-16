using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Mirror class responsible by CREATING A NEW POST</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required, StringLength(30)]
        public string NameCreator { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public NewPostDTO(string title, string description, string picture, string nameCreator, string themedescription)
        {
            Title = title;
            Description = description;
            Picture = picture;
            NameCreator = nameCreator;
            ThemeDescription = themedescription;
        }
    }

    /// <summary>
    /// <para>Mirror class responsible by UPDATE an existing post</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>

    public class UpDatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public UpDatePostDTO(int id, string title, string description, string picture, string themeDescription)
        {
            Id = id;
            Title = title;
            Description = description;
            Picture = picture;
            ThemeDescription = themeDescription;
        }
    }
}
