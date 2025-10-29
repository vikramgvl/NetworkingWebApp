using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetworkingWebApp.Data;
using NetworkingWebApp.Dto;
using NetworkingWebApp.Entities;

namespace NetworkingWebApp.Controllers
{
   
    public class AccountController(AppDbContext context) : BaseApiController
    {
        [HttpPost("Register")]
        public async Task <ActionResult<AppUser>> Register(RegisterDTO registerDTO)
        {
            // Registration logic here
            if (UserExists(registerDTO.Email))
            {
                return BadRequest("Email is already taken");
            }
            var hmac = new System.Security.Cryptography.HMACSHA512();
            var user = new AppUser
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email");
            }
            var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }
            return user;
        }

        private bool UserExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }
    }
}
