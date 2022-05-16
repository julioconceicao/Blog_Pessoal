using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>This class represent tb_post at database.</para>
    /// <para>By: Julio Conceicao</para>
    /// <para>v 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>   
    [Table("tb_posting")]
    public class PostingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required, StringLength(30)]
        public int Id { get; set; }


        [Required, StringLength(30)]
        public string Title { get; set; }


        [Required, StringLength(30)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [ForeignKey("FK_user")]
        public UserModel Creator { get; set; }

        [ForeignKey("Fk_theme")]
        public ThemeModel Theme { get; set; }
    }
}