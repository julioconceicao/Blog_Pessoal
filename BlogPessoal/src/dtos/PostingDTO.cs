using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{

    /// <summary>
    /// <para>Mirror class to CREATE A NEW POST.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(30)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public string NameCreator { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public NewPostDTO(string title, string description, string picture, string nameCreator, string themedescription, int id)
        {
            Title = title;
            Description = description;
            Picture = picture;
            NameCreator = nameCreator;
            ThemeDescription = themedescription;
            Id = id;
        }
    }

    /// <summary>
    /// <para>Mirror class to UPDATE A POST.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public class UpDatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(30)]
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
