using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tasahil_.net_core_6_.Entity;
using Tasahil_.net_core_6_.Helper;
using Tasahil_.net_core_6_.Interface;
using Tasahil_.net_core_6_.Models;
using Tasahil_.net_core_6_.Repository;

namespace Tasahil_.net_core_6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDynamicRep<Post> rep;
        private readonly IHubContext<PostHub, IPostHub> hubContext;

        public PostsController(IDynamicRep<Post> rep,IHubContext<PostHub,IPostHub> hubContext)
        {
            this.rep = rep;
            this.hubContext = hubContext;
        }

        [HttpGet]
        [Route("~/Updated")]
        public IActionResult Updated()
        {
            string retPost = string.Empty; 
            try
            {
                var postVM = new PostVM {
                  Id= 0,
                  Title= "string",
                  Price= 1000000,
                  Description ="string",
                  CateId= 0,
                  UserId= "string",
                  PhotoName= "string"
                };
                this.hubContext.Clients.All.BrodcastPosts(postVM);
                retPost = "Success";

                return Ok(retPost);
            }
            catch (Exception e)
            {
                retPost = e.Message;
                return Ok(retPost);
            }
        }

        [HttpGet]
        [Route("~/GetAllPost")]
        public IActionResult GetAllPost()
        {
            try
            {
                var data = rep.Get(new[] { "ApplicationUser", "Category", "Comments" });
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error"
                    });
                }

                return Ok(new ApiResponse<IEnumerable<Post>>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
                    Count = data.Count(),
                    Data = data.OrderByDescending(a=>a.PuplishDate).ToList()
                });
            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }


        [HttpGet]
        [Route("~/GetPost/{id}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var data = rep.GetById(a => a.Id == id, new[] { "ApplicationUser", "Category" });
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error",
                        Error = "Post Not Found"
                    });
                }

                return Ok(new ApiResponse<Post>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
                    Count = 1,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }


        [HttpPost]
        [Route("~/CreatePost")]
        public IActionResult CreatePost([FromBody] PostVM postVM)
        {
            try
            {
                var post = new Post
                {
                    Id = postVM.Id,
                    Title = postVM.Title,
                    Description = postVM.Description,
                    Price = postVM.Price,
                    CateId = postVM.CateId,
                    UserId = postVM.UserId,
                    PhotoName = postVM.PhotoName
                };
                var data = rep.Create(post);

                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error"
                    });
                }

                return Ok(new ApiResponse<Post>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Created",
                    Count = 1,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }


        [HttpPut]
        [Route("~/EditPost")]
        public IActionResult EditPost([FromBody] PostVM postVM)
        {
            try
            {

                var find = rep.GetById(postVM.Id);
                if (find != null)
                {
                    find.Title = postVM.Title;
                    find.Description = postVM.Description;
                    find.PhotoName = postVM.PhotoName;
                    find.Price = postVM.Price;
                    find.CateId = postVM.CateId;
                    find.PuplishDate = DateTime.Now;

                    rep.Edit(find);
                    return Ok(new ApiResponse<Post>
                    {
                        Code = "200",
                        Status = "OK",
                        Message = "Date Updated",
                        Count = 1,
                        Data = find
                    });
                }

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = "Post Not Found"
                });

            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }


        [HttpPost]
        [Route("~/DeletePost")]
        public IActionResult DeletePost([FromBody] PostVM postVM)
        {
            try
            {
                var find = rep.GetById(postVM.Id);
                if (find != null && find.Title == postVM.Title)
                {
                    rep.Delete(find);
                    return Ok(new ApiResponse<Post>
                    {
                        Code = "200",
                        Status = "OK",
                        Message = "Date has been Deleted",
                        Count = 1,
                        Data = find
                    });
                }

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = "Post Not Found"
                });


            }
            catch (Exception e)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }
    }
}
