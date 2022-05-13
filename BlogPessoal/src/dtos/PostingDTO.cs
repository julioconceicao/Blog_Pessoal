using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
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
