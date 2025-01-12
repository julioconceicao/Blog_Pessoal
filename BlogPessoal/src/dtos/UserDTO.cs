﻿using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
        /// <summary>
    /// <para>Mirror class to CREATE a new user</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class NewUserDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Picture { get; set; }

        [Required]
        public UserType Type { get; set; }

        public NewUserDTO(string name, string email, string password, string picture, UserType type)
        {
            Name = name;
            Email = email;
            Password = password;
            Picture = picture;
            Type = type;
        }
    }

    /// <summary>
    /// <para>Mirror class to UPDATE an existing user.</para>
    /// <para>by: Julio Conceicao</para>
    /// <para>V 1.0</para>
    /// <para>May.12.2022</para>
    /// </summary>
    public class UpDateUserDTO
    {

        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Picture { get; set; }

        public UpDateUserDTO(int id, string name, string email, string password, string picture)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Picture = picture;
        }
    }
}
