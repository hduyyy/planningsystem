using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Mywebapi.Models;
using Microsoft.AspNetCore.Identity.Data;
using Mywebapi.Auth.Services;

namespace Mywebapi.Auth.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly AppDbContext dbcontext;
        private readonly AuthService authService;
        public AuthController(AppDbContext appDbContext, AuthService authService)
        {
            this.dbcontext = appDbContext;
            this.authService = authService;
        }
        [HttpGet("google-login")]
        public  IActionResult GoogleLogin()
        {
            var rediUrl = Url.Action(nameof(GoogleCallback), "Auth", null, Request.Scheme);
            var check =new AuthenticationProperties { RedirectUri= rediUrl };
            return  new ChallengeResult(GoogleDefaults.AuthenticationScheme, check);
        }
        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var authKq = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (!authKq.Succeeded) return Unauthorized();
                var claims = authKq.Principal.Identities.FirstOrDefault()?.Claims;
                var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var ggId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var pic = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(ggId))
                {
                    return BadRequest("Không thể lấy thông tin GG");

                }
                //var existingUser = dbcontext.Users.FirstOrDefault(u => u.Email == email);
                //    if (existingUser == null) {
                //    var newUser = new User
                //    {
                //        Email = email,
                //        Fullname = name
                //    };
                //    dbcontext.Users.Add(newUser);
                //    await dbcontext.SaveChangesAsync();

                //}
                HttpContext.Session.SetString("Email:", email);
                HttpContext.Session.SetString("fullname:", name);
                HttpContext.Session.SetString("Id:", ggId);
                HttpContext.Session.SetString("Avatar:", pic ?? "khong có");
                return Ok(new
                {
                    Message = "Đăng nhập thành công",
                    Session = new
                    {
                        Email = email,
                        Fullname = name,
                        GGId= ggId,
                        picture=pic
                    }
                });
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);  
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequest reqest)
        {
            var token=await authService.LoginAsync(reqest.Username, reqest.Password);
            if (token != null) { 
                return Unauthorized( new {message="Invalid"});
            }
            return Ok(new {accesstoken=token});
        }
    }

}
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
