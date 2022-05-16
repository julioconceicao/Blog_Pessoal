using BlogPessoal.src.utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>This class represent tb_user at database.</para>
    /// <para>By: Julio Conceicao</para>
    /// <para>v 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>    
    
    [Table("tb_user")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }


        public string Picture { get; set; }

        public UserType Type { get; set; }


        [JsonIgnore]
        public List<PostingModel> MyPosts { get; set; }
    }
}