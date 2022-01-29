using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasahil_.net_core_6_.Models;
using Tasahil_.net_core_6_.Services;

namespace Tasahil_.net_core_6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthsController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost]
        [Route("~/Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
            {
                return Ok(new {message = result.Message });
            }

            return Ok(new {message = result.Message , token = result.Token , expiresOn = result.ExpiresOn});
        }

        [HttpPost]
        [Route("~/Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.LoginAsync(model);
            if (!result.IsAuthenticated)
                return Ok(new { message = result.Message});

            return Ok(new { message = result.Message, token = result.Token, expiresOn = result.ExpiresOn });
        }



        [Route("~/SavePhoto")]
        [HttpPost]
        public async Task<IActionResult>  SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = Guid.NewGuid()+ postedFile.FileName;
                var physicalPath = Directory.GetCurrentDirectory() + "/wwwroot/Img/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                return Ok(new { message = filename });
            }
            catch (Exception)
            {
                return Ok(new { message = "Error !" });            
            }
        }

        [HttpPost]
        [Route("~/UnSavePhoto")]
        public JsonResult UnSaveFile([FromBody] PhotoVM photoVM)
        {
            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Img/" + photoVM.Name))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Img/" + photoVM.Name);
                }

                return new JsonResult(new { message = "Deleted !" });
            }
            catch (Exception)
            {

                return new JsonResult("Error!");
            }
        }
    }
}
