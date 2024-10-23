using FirstDotNet.Context;
using FirstDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNet.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task<Post> DeletePost(int id);
    }
    public class PostRepository : IPostRepository
    {

        private readonly SocialMediaContext _context;
        private readonly DbSet<Post> _posts;

        public PostRepository(SocialMediaContext context)
        {
            _context = context;
            _posts = context.Posts;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _posts.ToListAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _posts.FindAsync(id);
        }

        public async Task<Post> CreatePost(Post post)
        {
            _posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePost(int id)
        {
            var post = await _posts.FindAsync(id);
            if (post == null)
            {
                return null;
            }

            _posts.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
