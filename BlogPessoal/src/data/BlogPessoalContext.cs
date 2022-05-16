using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    public class BlogPessoalContext : DbContext
    {
    /// <summary>
    /// <para>Context class responsible for store context and set DbSets.</para>
    /// <para>By: Julio C. Goncalves Conceicao</para>
    /// <para>V1.0</para>
    /// <para>13/05/2022</para>
    /// </summary>
    
        public DbSet<UserModel> User { get; set; }
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostingModel> Posts { get; set; }

        public BlogPessoalContext(DbContextOptions<BlogPessoalContext> opt) : base(opt)
        {
        }
    }
}
