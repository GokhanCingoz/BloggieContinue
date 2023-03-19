using Bloggie.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContex;

       public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
           this.bloggieDbContex = bloggieDbContext;
        }

    

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContex.BlogPostLikes.CountAsync(x=>x.BlogPostId==blogPostId);
        }
    }
}
