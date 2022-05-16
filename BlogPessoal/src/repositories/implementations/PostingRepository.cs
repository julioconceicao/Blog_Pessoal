using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>Class responsible by implementing IPosting interface.</para>
    /// <para>By: Julio C. Goncalves Conceicao</para>
    /// <para>V1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>

    public class PostingRepository : IPosting
    {
        private readonly BlogPessoalContext _context;

         public PostingRepository(BlogPessoalContext context)
        {
            _context = context;
        }
        /// <summary>
        /// <para>Async method to CREATE a new post.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task NewPostAsync(NewPostDTO post)
        {
           await _context.Posts.AddAsync(new PostingModel
            {
                Title = post.Title,
                Description = post.Description,
                Picture = post.Picture,
                Creator = _context.User.FirstOrDefault(u => u.Name == post.NameCreator),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.ThemeDescription)
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to UPDATE an existing posts.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task UpDatePostAsync(UpDatePostDTO post)
        {
            var existingPost = await GetPostByIdAsync(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Picture = post.Picture;
            existingPost.Theme = _context.Themes.FirstOrDefault(t => t.Description == post.ThemeDescription);
            
            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();
        }
        
        /// <summary>
        /// <para>Async method to DELETE an existing post.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Async method to GET ALL POSTS.</para>
        /// <para>By: Julio Conceicao</para>
        /// <para>v 1.0</para>
        /// <para>May.12.2022</para>
        /// </summary>
        public async Task<List<PostingModel>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.Creator)
                .Include(p => p.Theme)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Async method to GET a post by ID</para>
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <return>PostingModel</return>
        public async Task<PostingModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts
            .Include(p => p.Creator)
            .Include(p => p.Theme)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Async method to GET post by SEARCH</para>
        /// </summary>
        /// <param name="title">Post title</param>
        /// <param name="themeDescription">Theme description</param>
        /// <param name="nameCreator">Creator name</param>
        /// <return>List PostingModel</return>
        public async Task<List<PostingModel>> GetPostBySearchAsync(
            string title,
            string themeDescription,
            string nameCreator)
        {
            switch (title, themeDescription, nameCreator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();

                case (null, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();

                case (_, null, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();

                case (_, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();

                case (null, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(themeDescription) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();

                case (_, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();

                case (_, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(themeDescription) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
            }
        }
    }
}
