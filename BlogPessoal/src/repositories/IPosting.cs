using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositories
{

    /// <summary>
    /// <para>Represent the CRUID posting actions.</para>
    /// <para>By: Julio Conceição</para>
    /// <para>v 1.0</para>
    /// <para>Apr.29.2022</para>
    /// </summary>
    public interface IPosting
    {
        void NewPost(NewPostDTO post);

        void UpDatePost(UpDatePostDTO post);

        void DeletePost(int id);

        PostingModel GetPostByID(int id);

        List<PostingModel> GetAllPosts();

        List<PostingModel> GetPostBySearch(string title, string themeDescription, string emailCreator);


    }
}
