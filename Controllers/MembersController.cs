using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetworkingWebApp.Data;
using NetworkingWebApp.Entities;

namespace NetworkingWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMember()
        {
            var members = await context.Users.ToListAsync();
            return members;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMemberById(string id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }
    }
}
