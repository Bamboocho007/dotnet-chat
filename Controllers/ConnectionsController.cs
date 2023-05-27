using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myChat.DB;
using myChat.DTO;

namespace myChat.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ConnectionsController : ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly PgContext _context;

    public ConnectionsController(ILogger<UsersController> logger, PgContext context)
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<UserConnectionDto>>> Get(Guid userId)
    {
      var user = await _context.Users.Include(u => u.Chats).FirstOrDefaultAsync();

      if (user == null)
        return NotFound();

      var userConnections = user.Chats.Select(c => new UserConnectionDto
      {
        UserName = string.Join(", ", c.Users.Select(u => u.Name).ToArray()),
        ChatId = c.Id,
      });

      return userConnections.ToList();
    }
    [HttpPost("{chatId}")]
    public async Task<ActionResult<IEnumerable<UserConnectionDto>>> Post(Guid chatId, [FromBody] List<NewUserConnectionDto> dto)
    {
      if (dto == null || dto.ToArray().Length == 0)
      {
        return BadRequest();
      }

      var chat = await _context.Chats
        .Where(c => c.Id == chatId)
        .Include(c => c.Users)
        .FirstOrDefaultAsync();

      if (chat == null)
        return NotFound();

      var userIds = dto.Select(u => u.UserId).ToList();
      var users = await _context.Users
      .Where(u => userIds.Contains(u.Id))
      .ToListAsync();

      if (users == null)
        return NotFound();

      users.ForEach(u =>
      {
        if (chat.Users.Find(user => user.Id == u.Id) == null)
        {
          chat.Users.Add(u);
        }
      });

      await _context.SaveChangesAsync();

      return new List<UserConnectionDto>();
    }
  }
}