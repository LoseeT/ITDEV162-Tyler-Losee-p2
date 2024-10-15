using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers{
    [ApiController]
    //Runs on localhost:5278/api/posts
    [Route("api/[controller]")]
    public class PostsController: ControllerBase
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            this._context = context;
        }


        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            return this._context.Posts.ToList();
        }

        /// <summary>
        /// Get api/post/[id]
        /// </summary>
        /// <param name="id"=pst id</param>
        /// <returnsA single post</returns>
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Post> GetById(Guid id)
        {
            var post = this._context.Posts.Find(id);
            if (post is null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        /// <summary>
        /// POST api/post
        /// </summary>
        /// <param name="request">JSON request containing post fields</param>
        /// <returns></returns>
        [HttpPost(Name = "Create")]

        public ActionResult<Post> Create([FromBody]Post request)
        {
            var post = new Post
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date
            };

            _context.Posts.Add(post);
            var success = _context.SaveChanges() > 0;

            if (success)
            {
                return Ok(post);
            }

            throw new Exception("Error creating post (Check again man)");
        }
    }
}
