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
        
        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(30)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public string EmailCreator { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public NewPostDTO(string title, string description, string picture, string emailCreator, string themedescription)
        {
            Title = title;
            Description = description;
            Picture = picture;
            EmailCreator = emailCreator;
            ThemeDescription = themedescription;
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

        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(30)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

        public UpDatePostDTO(string title, string description, string picture, string themeDescription)
        {
            Title = title;
            Description = description;
            Picture = picture;
            ThemeDescription = themeDescription;
        }
    }
}
