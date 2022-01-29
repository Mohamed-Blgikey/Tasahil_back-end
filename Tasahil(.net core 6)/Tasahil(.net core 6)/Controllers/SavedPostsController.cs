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
    public class SavedPostsController : ControllerBase
    {
        private readonly IDynamicRep<SavedPosts> rep;
        private readonly IMapper mapper;

        public SavedPostsController(IDynamicRep<SavedPosts> rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("~/GetSaved")]
        public IActionResult GetSaved()
        {
            try
            {
                var data = rep.Get(new[] { "Post" });
                return Ok(new ApiResponse<List<SavedPosts>>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retreved",
                    Count = data.Count(),
                    Data = data.ToList()
                }); ;
            }
            catch (Exception e)
            {
                return Ok(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = e.Message
                });
            }
        }
        [HttpPost]
        [Route("~/Saved")]
        public IActionResult Saved([FromBody] SavedPostsVM savedPostsVM)
        {
            try
            {
                var map = new SavedPosts
                {
                    PostId = savedPostsVM.PostId,
                    ApplicationUserId = savedPostsVM.ApplicationUserId
                };
                var data = rep.Create(map);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Done!",
                        Error = "Post Not Created"
                    });
                }
                return Ok(new ApiResponse<SavedPosts>
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


        [HttpPost]
        [Route("~/unSaved")]
        public IActionResult unSaved([FromBody] SavedPostsVM savedPostsVM)
        {
            try
            {
                var map = rep.GetById(a => a.ApplicationUserId == savedPostsVM.ApplicationUserId && a.PostId == savedPostsVM.PostId);
                var data = rep.Delete(map);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Done!",
                        Error = "Post Not Created"
                    });
                }
                return Ok(new ApiResponse<SavedPosts>
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
    }
}
