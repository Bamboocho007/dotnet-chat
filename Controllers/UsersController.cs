using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myChat.DB;
using myChat.DB.Models;

namespace myChat.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly PgContext _context;

    public UsersController(ILogger<UsersController> logger, PgContext context)
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<User>> GetOrCreateUserByName(string name)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

      if (user == null)
      {
        User newUser = new User
        {
          Name = name,
          Id = Guid.NewGuid(),
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser;
      }

      return user;
    }
  }
}