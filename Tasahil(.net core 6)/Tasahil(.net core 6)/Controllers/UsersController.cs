using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasahil_.net_core_6_.Extend;
using Tasahil_.net_core_6_.Helper;
using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("~/GetAllusers")]
        public IActionResult GetAllusers()
        {
            try
            {
                var data = userManager.Users;
                var FullData = data.Include(a => a.Posts).Include(a => a.SavedPosts).Select(a => a);

                return Ok(new ApiResponse<IEnumerable<ApplicationUser>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Retrved",
                    Count = FullData.Count(),
                    Data = FullData
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
        [Route("~/GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var data = roleManager.Roles;


                return Ok(new ApiResponse<IEnumerable<IdentityRole>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Retrved",
                    Count = data.Count(),
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
        [Route("~/Getuser/{id}")]
        public IActionResult Getuser(string id)
        {
            try
            {
                var data = userManager.Users;
                var FullData = data.Include(a => a.SavedPosts).Include(a => a.Posts).Include(a => a.Comments).Where(a => a.Id == id);


                return Ok(new ApiResponse<IEnumerable<ApplicationUser>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Retrved",
                    Count = FullData.Count(),
                    Data = FullData
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
        [Route("~/EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserVM userVM)
        {

            try
            {
                var error = string.Empty;
                if (ModelState.IsValid)
                {

                    var user = await userManager.FindByIdAsync(userVM.Id);
                    var emailExsit = await userManager.FindByEmailAsync(userVM.Email);
                    var UserNameExsit = await userManager.FindByNameAsync(userVM.UserName);
                    if (emailExsit != null)
                    {
                        return Ok(new ApiResponse<string>
                        {
                            Code = "200",
                            Status = "Ok",
                            Message = "Done!",
                            Error = "Email Is Already Token"
                        });
                    }

                    if (emailExsit != null)
                    {
                        return Ok(new ApiResponse<string>
                        {
                            Code = "200",
                            Status = "Ok",
                            Message = "Done !",
                            Error = "UserName Is Already Token"
                        });
                    }


                    user.UserName = userVM.UserName;
                    user.Email = userVM.Email;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.PhotoName = userVM.PhotoName;

                    var result = await userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            error += $"{item.Description},";
                        }
                        return NotFound(new ApiResponse<string>
                        {
                            Code = "404",
                            Status = "Not Found",
                            Message = "Error",
                            Error = error
                        });
                    }
                    return Ok(new ApiResponse<ApplicationUser>
                    {
                        Code = "200",
                        Status = "Ok",
                        Message = "Data Updating",
                        Data = user
                    });
                }
                foreach (var item in ModelState)
                {
                    error += $"{item.Value.Errors}";
                }
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = error
                });


            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = ex.Message
                });
            }

        }

        [HttpDelete]
        [Route("~/DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] UserVM userVM)
        {
            var error = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(userVM.Id);
                    if (user == null)
                    {
                        return NotFound(new ApiResponse<string>
                        {
                            Code = "404",
                            Status = "Not Found",
                            Message = "Error",
                            Error = "User Not Exsit"
                        });
                    }

                    var result = await userManager.DeleteAsync(user);

                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            error += $"{item.Description},";
                        }
                        return NotFound(new ApiResponse<string>
                        {
                            Code = "404",
                            Status = "Not Found",
                            Message = "Error",
                            Error = error
                        });
                    }
                    else
                    {

                        return Ok(new ApiResponse<ApplicationUser>
                        {
                            Code = "200",
                            Status = "Ok",
                            Message = "Data Updating",
                            Data = user
                        });
                    }
                }
                foreach (var item in ModelState)
                {
                    error += $"{item.Value.Errors}";
                }
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = error
                });



            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Error",
                    Error = ex.Message
                });
            }

        }


        [HttpPost]
        [Route("~/AddOrRemoveFromRole")]
        public async Task<IActionResult> AddOrRemoveFromRole([FromBody] MangeRolesVM mangeRolesVM)
        {
            try
            {
                var role = await roleManager.FindByNameAsync(mangeRolesVM.RoleName);
                var user = await userManager.FindByNameAsync(mangeRolesVM.UserName);
                if (role == null)
                {
                    return Ok(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error",
                        Error = "Role Not Exsit"
                    });
                }

                if (user == null)
                {
                    return Ok(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error",
                        Error = "User Not Exsit"
                    });
                }

                IdentityResult result = null;

                if (!(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if ((await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }

                if (!result.Succeeded)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Error",
                        Error = "XXXXXXXXXXXXXXXXXXXXXX"
                    });
                }

                return Ok(new ApiResponse<MangeRolesVM>
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Operation Done!",
                    Data = mangeRolesVM
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
        [Route("~/GetUsersAndThereRoles")]
        public async Task<IActionResult> GetUsersAndRoles()
        {
            var users = userManager.Users;
            List<MangeRolesVM> Usersroles = new List<MangeRolesVM>();

            foreach (var user in users)
            {
                var userRole = new MangeRolesVM();
                userRole.UserName = user.UserName;
                if (await userManager.IsInRoleAsync(user, "admin"))
                {
                    userRole.RoleName += " { admin } ";
                }

                if (await userManager.IsInRoleAsync(user, "user"))
                {
                    userRole.RoleName += " { user } ";
                }
                Usersroles.Add(userRole);
            }
            return Ok(new ApiResponse<IEnumerable<MangeRolesVM>>
            {
                Code = "200",
                Status = "OK",
                Message = "Operation Done!",
                Count = Usersroles.Count(),
                Data = Usersroles
            });
        }
    }
}
