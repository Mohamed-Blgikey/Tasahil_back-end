using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasahil_.net_core_6_.Extend;
using Tasahil_.net_core_6_.Helper;
using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IOptions<JWT> jwt;

        public RoleManager<IdentityRole> RoleManager => roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt;
        }


        public async Task<AuthResponseVM> LoginAsync(LoginVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.password))
                return new AuthResponseVM { Message = "Inavlid Email Or Password" };

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthResponseVM
            {
                Message = "Success",
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true
            };
        }

        public async Task<AuthResponseVM> RegisterAsync(RegisterVM model)
        {
            if (await userManager.FindByEmailAsync(model.Email) != null)
                return new AuthResponseVM { Message = "Email Is Already Token" };

            if (await userManager.FindByNameAsync(model.UserName) != null)
                return new AuthResponseVM { Message = "UserName Is Already Token" };

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                IsAgree = model.IsAgree,
                PhoneNumber = model.Phone,
                PhotoName = model.PhotoName
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var error = string.Empty;
                foreach (var item in result.Errors)
                {
                    error += $"{item.Description},";
                }
                return new AuthResponseVM { Message = error };
            }

            var RoleExsit = await RoleManager.RoleExistsAsync("admin");
            if (!RoleExsit)
            {
                await RoleManager.CreateAsync(new IdentityRole("admin"));
                await userManager.AddToRoleAsync(user, "admin");
            }
            else
            {
                await RoleManager.CreateAsync(new IdentityRole("user"));
                await userManager.AddToRoleAsync(user, "user");
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthResponseVM
            {
                Message = "Success",
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true
            };

            throw new NotImplementedException();
        }

        public async Task<AuthResponseVM> ForgetPass(ForgetPassVM model)
        {
            if (await userManager.FindByEmailAsync(model.Email) == null)
            {
                return new AuthResponseVM { Message = "User not Exsit" };
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return new AuthResponseVM
            {
                Message = "Success",
                Token = token,
                Email = user.Email
            };
        }

        public async Task<AuthResponseVM> resstPass(RessetPassVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseVM { Message = "Email Not Exsit" };
            }

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseVM { Message = "Passwords must have at least one non alphanumeric character., Passwords must have at least one digit('0' - '9').,Passwords must have at least one uppercase('A' - 'Z').," };
            }
            return new AuthResponseVM { Message = "Succss" };

            throw new NotImplementedException();
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Value.Issuer,
                audience: jwt.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.Value.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
