using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task NewPostAsync(NewPostDTO post);

        Task UpDatePostAsync(UpDatePostDTO post);

        Task DeletePostAsync(int id);

        Task<PostingModel> GetPostByIdAsync(int id);

        Task<List<PostingModel>> GetAllPostsAsync();

        Task<List<PostingModel>> GetPostBySearchAsync(string title, string themeDescription, string nameCreator);


    }
}
