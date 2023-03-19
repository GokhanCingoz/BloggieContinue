using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {   
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository,SignInManager <IdentityUser>signInManager,UserManager<IdentityUser> userManager)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IBlogPostLikeRepository BlogPostLike { get; }
       

        [HttpGet]
        [Route("Add")]

        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest) 
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,
            };

            blogPostLikeRepository.AddLikeForBlog(model);

            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogpostId)
        {   
            
            var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogpostId);
            return Ok(totalLikes);
        }


    }
}
