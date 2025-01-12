﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>This class represent tb_themes at database.</para>
    /// <para>By: Julio Conceicao</para>
    /// <para>v 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
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
