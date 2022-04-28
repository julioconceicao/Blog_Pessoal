using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    //NOME DE CLASSE COMEÇA SEMPRE EM MAIÚSCULO!!!!!
    [Table("tb_user")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        [Required]
        [StringLength(30)]

        public string Email { get; set; }
        [Required]
        [StringLength(30)]

        public string Password { get; set; }
        [Required]
        [StringLength(30)]

        public string Picture { get; set; }


        [JsonIgnore]

        public List<PostingModel> MyPosts {get; set;}
}
}