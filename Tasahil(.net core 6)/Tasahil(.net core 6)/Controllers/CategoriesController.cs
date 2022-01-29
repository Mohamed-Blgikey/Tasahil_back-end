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
    public class CategoriesController : ControllerBase
    {
        private readonly IDynamicRep<Category> rep;
        private readonly IMapper mapper;

        public CategoriesController(IDynamicRep<Category> rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("~/GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            try
            {
                var data = rep.Get();
                var model = mapper.Map<IEnumerable<CategoryVM>>(data);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error"
                    });
                }
                return Ok(new ApiResponse<IEnumerable<CategoryVM>>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
                    Count = model.Count(),
                    Data = model
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
        [Route("~/GetCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var data = rep.GetById(id);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error",
                        Count = 1,
                        Error = "Category not Found"
                    });
                }
                var model = mapper.Map<CategoryVM>(data);

                return Ok(new ApiResponse<CategoryVM>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Retrevied",
                    Data = model
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
        [Route("~/CreateCategory")]
        public IActionResult CreateCategory([FromBody] CategoryVM categoryVM)
        {
            try
            {
                var category = mapper.Map<Category>(categoryVM);
                var data = rep.Create(category);


                if (data == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error"
                    });
                }

                categoryVM.ID = data.Id;
                categoryVM.Name = data.Name;
                return Ok(new ApiResponse<CategoryVM>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Date Created",
                    Count = 1,
                    Data = categoryVM
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
        [Route("~/EditCategory")]
        public IActionResult EditCategory([FromBody] CategoryVM categoryVM)
        {
            try
            {
                var find = rep.GetById(categoryVM.ID);
                if (find != null)
                {
                    find.Name = categoryVM.Name;
                    rep.Edit(find);
                    return Ok(new ApiResponse<CategoryVM>
                    {
                        Code = "200",
                        Status = "OK",
                        Message = "Date has been Edit",
                        Count = 1,
                        Data = categoryVM
                    });
                }

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = "Category Not Found"
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
        [Route("~/DeleteCategory")]
        public IActionResult DeleteCategory([FromBody] CategoryVM categoryVM)
        {
            try
            {
                var find = rep.GetById(categoryVM.ID);
                if (find != null && find.Name == categoryVM.Name)
                {
                    rep.Delete(find);
                    return Ok(new ApiResponse<CategoryVM>
                    {
                        Code = "200",
                        Status = "OK",
                        Message = "Date has been Deleted",
                        Count = 1,
                        Data = categoryVM
                    });
                }

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = "Category Not Found"
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
