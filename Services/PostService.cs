using FirstDotNet.Models;
using FirstDotNet.Repositories;

namespace FirstDotNet.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task<Post> DeletePost(int id);
    }

    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> CreatePost(Post post)
        {
            return await _postRepository.CreatePost(post);
        }

        public Task<Post> DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public Task<Post> UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
