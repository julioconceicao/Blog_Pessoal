﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{

    [Table("tb_theme")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }

        [JsonIgnore]

        public List<PostingModel> RelatedPosts { get; set; }
    }
}
