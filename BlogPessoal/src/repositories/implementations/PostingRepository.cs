using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class PostingRepository : IPosting
    {
        private readonly BlogPessoalContext _context;

        public void NewPost(NewPostDTO post)
        {
            _context.Posts.Add(new PostingModel
            {
                Title = post.Title,
                Description = post.Description,
                Picture = post.Picture,
                Creator = _context.User.FirstOrDefault(
                    u => u.Name == post.NameCreator),
                Theme = _context.Themes.FirstOrDefault(
                t => t.Description == post.ThemeDescription)
            });
            _context.SaveChanges();
        }

        public void UpDatePost(UpDatePostDTO post)
        {
            var existingPost = GetPostByID(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Picture = post.Picture;
            existingPost.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == post.ThemeDescription);
            _context.Posts.Update(existingPost);
            _context.SaveChanges();

        }
        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostByID(id));
            _context.SaveChanges();

        }

        public List<PostingModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public PostingModel GetPostByID(int id)
        {
            return _context.Posts.FirstOrDefault(u => u.Id == id);
        }

        public List<PostingModel> GetPostBySearch(
            string title,
            string themeDescription,
            string nameCreator)
        {
            switch (title, themeDescription, nameCreator)
            {
                case (null, null, null):
                    return GetAllPosts();

                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToList();
                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();
                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(themeDescription))
                    .ToList();
                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(themeDescription) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(themeDescription) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
            }
        }


    }
}
