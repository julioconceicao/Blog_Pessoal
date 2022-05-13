using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    public class NewThemeDTO
    {
        [Required, StringLength(20)]
        public string Description { get; set; }

        public NewThemeDTO(string description)
        {
            Description = description;
        }
    }

    public class UpDateThemeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }

        public UpDateThemeDTO(string description, int id)
        {
            Id = Id;
            Description = description;
        }
    }
}
