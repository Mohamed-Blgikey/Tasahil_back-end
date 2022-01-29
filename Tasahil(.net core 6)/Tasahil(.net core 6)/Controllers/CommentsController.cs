using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasahil_.net_core_6_.Entity;
using Tasahil_.net_core_6_.Helper;
using Tasahil_.net_core_6_.Interface;
using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IDynamicRep<Comment> rep;
        private readonly IMapper mapper;

        public CommentsController(IDynamicRep<Comment> rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("~/GetAllComments")]
        public IActionResult GetAllComments()
        {
            try
            {
                var data = rep.Get(new[] { "ApplicationUser" });
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Done!",
                        Error = "Error"
                    });
                }
                return Ok(new ApiResponse<IEnumerable<Comment>>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
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

        [HttpGet]
        [Route("~/GetComment/{id}")]
        public IActionResult GetComment(int id)
        {
            try
            {
                var data = rep.GetById(a => a.Id == id, new[] { "ApplicationUser", "Post" });
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Done!",
                        Error = "Error"
                    });
                }
                return Ok(new ApiResponse<Comment>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
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
        [Route("~/CreateComment")]
        public IActionResult CreateComment(CommentVM commentVM)
        {
            try
            {
                var map = mapper.Map<Comment>(commentVM);
                var data = rep.Create(map);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Done!",
                        Error = "Comment Not Created"
                    });
                }
                return Ok(new ApiResponse<Comment>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Created",
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
        [Route("~/EditComment")]
        public IActionResult EditComment([FromBody] CommentVM commentVM)
        {
            try
            {

                var find = rep.GetById(commentVM.Id);
                if (find != null)
                {
                    find.CommentContent = commentVM.CommentContent;

                    find.CommentDate = DateTime.Now;

                    rep.Edit(find);
                    return Ok(new ApiResponse<Comment>
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
                    Error = "Comment Not Found"
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
        [Route("~/DeleteComment")]
        public IActionResult DeleteComment(CommentVM commentVM)
        {
            try
            {
                var find = rep.GetById(commentVM.Id);
                if (find != null)
                {
                    rep.Delete(find);
                    return Ok(new ApiResponse<Comment>
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
                    Error = "Comment Not Found"
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
