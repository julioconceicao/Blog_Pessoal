using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlogPessoal.src.models
{
    //NOME DE CLASSE COMEÇA SEMPRE EM MAIÚSCULO!!!!!
    [Table("tb_posting")]
    public class PostingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(30)]    
        
        
        public string Title { get; set; }
        [Required, StringLength(30)]
        
        
        public string Description { get; set; }
        [Required, StringLength(30)]

        public string Picture { get; set; }


        [ForeignKey("FK_user")]
        public UserModel Creator { get; set; }

       
    }
}