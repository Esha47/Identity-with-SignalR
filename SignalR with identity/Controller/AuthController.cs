using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalR_with_identity.Data;
using SignalR_with_identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_with_identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        public AuthController(ApplicationDbContext context, IConfiguration configuration, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpGet("allUsers")]
        public IActionResult allUsers()
        {
            var users = _context.Users.Select(s => new
            {
                s.UserName,
                //   s.Id,
                s.Email,
                //  s.PasswordHash
            }).ToList();
            return Ok(users);
        }
        [HttpGet("IsUserExist")]
        public async Task<bool> IsUserExist(string email)
        {

            if (await _context.Users.AnyAsync(s => s.Email == email))
            {
                return true;
            }
            return false;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserApp userapp)
        {
            object response = null;
            if (await IsUserExist(userapp.Email))
            {
                response = new
                {
                    success = false,
                    message = "user already exist"
                };
                return Ok(response);
            }
            IdentityUser user = new IdentityUser();
            user.UserName = userapp.Username;
            user.Email = userapp.Email;
            var result = await _userManager.CreateAsync(user, userapp.Password);

            if (result.Succeeded)
            {
                response = new
                {
                    success = true,
                    message = "user registered",
                    Id = user.Id
                };
            }
            else
            {
                response = new
                {
                    success = true,
                    message = result.Errors.FirstOrDefault(),

                };
            }
            /*  string url = string.Format("/Consume/Index?user={0}",
                      userapp
                  );
             return Redirect(url);
            */

            //   return RedirectToAction("Index", "Home", userapp);

            return Ok(response);
        }
        [HttpGet("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole(string email, string role)
        {
            object response = null;
            if (!await IsUserExist(email))
            {
                response = new
                {
                    success = false,
                    message = "user not exist"
                };
                return Ok(response);
            }

            var user = _context.Users.FirstOrDefault(s => s.Email == email);
            if (_context.UserRoles.Any(s => s.UserId == user.Id))
            {
                response = new
                {
                    success = true,
                    message = "role already created for this user",
                };
                return Ok(response);
            }
            await _userManager.AddToRoleAsync(user, role);
            response = new
            {
                success = true,
                message = "role created",
            };

            return Ok(response);
        }
        [HttpGet("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string email, string role)
        {
            object response = null;
            if (!await IsUserExist(email))
            {
                response = new
                {
                    success = false,
                    message = "user not exist"
                };

            }
            var user = _context.Users.FirstOrDefault(s => s.Email == email);
            if (role == null)
            {
                var userrole = _context.UserRoles.Where(a => a.UserId == user.Id).ToList();
                _context.UserRoles.RemoveRange(userrole);
                _context.SaveChanges();
                response = new
                {
                    success = true,
                    message = "user roles deleted",
                };
            }
            else
            {
                if (_roleManager != null)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                    else
                    {
                        var roleId = _context.Roles.FirstOrDefault(s => s.Name.ToLower() == role.ToLower()).Id;
                        if (_context.UserRoles.Any(s => s.UserId == user.Id && s.RoleId == roleId))
                        {
                            response = new
                            {
                                success = true,
                                message = "this role already exist for user",
                            };
                            return Ok(response);
                        }
                    }
                }
                await _userManager.AddToRoleAsync(user, role);
                response = new
                {
                    success = true,
                    message = "role added",
                };
            }
            return Ok(response);
        }
        [HttpGet("login")]
        public async Task<IActionResult> login(string email, string password)
        {
            object response = null;
            if (!await IsUserExist(email))
            {
                response = new
                {
                    success = false,
                    message = "user not exist"
                };
                return Ok(response);
            }
            var user = _context.Users.FirstOrDefault(s => s.Email == email);
            var result = await _signInManager.PasswordSignInAsync(user, password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //gettinf jwt token
                //getting list of claims
                var claims = new[]
                {
                //setting claims
                new Claim("Email",email),  //custom claims
                new Claim(ClaimTypes.NameIdentifier,user.Id)  //predefined claims
            };
                //key to encrypt token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                response = new
                {
                    success = true,
                    message = "user logged in",
                    token = tokenString
                };
            }
            else
            {
                response = new
                {
                    success = true,
                    message = "logged in failed",
                };
            }
            return Ok(response);
        }
        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            object response = null;
            if (_context.Roles.Any(a => a.Name.ToLower() == role))
            {
                var roleId = _context.Roles.Where(a => a.Name.ToLower() == role).FirstOrDefault().Id;
                var userList = from user in _context.Users
                               join userrole in _context.UserRoles
                               on user.Id equals userrole.UserId
                               where (userrole.RoleId == roleId)
                               select new
                               {
                                   user.Id,
                                   user.UserName,
                                   user.Email
                               };
                response = new
                {
                    success = true,
                    userList
                };
            }
            else
            {
                response = new
                {
                    success = false,
                    message = "role does not exist"
                };
            }
            return Ok(response);
        }
    }
}
