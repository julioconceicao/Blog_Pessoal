using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Mirror class to CREATE A NEW USER.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>

    public class NewUserDTO
    {
        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Picture { get; set; }

        public NewUserDTO(string name, string email, string password, string picture)
        {
            Name = name;
            Email = email;
            Password = password;
            Picture = picture;
        }

    }


    /// <summary>
    /// <para>Mirror class to UPDATE AN USER.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public class UpDateUserDTO
    {

        [Required]
        [StringLength(30)]
        public string Name { get; set; }


        [Required]
        [StringLength(30)]
        public string Password { get; set; }


        public string Picture { get; set; }

        public UpDateUserDTO(string name, string password, string picture)
        {
            Name = name;
            Password = password;
            Picture = picture;
        }



    }
}
