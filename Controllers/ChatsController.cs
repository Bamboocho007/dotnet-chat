using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myChat.DB;
using myChat.DB.Models;
using myChat.DTO;

namespace myChat.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ChatsController : ControllerBase
  {

    private readonly ILogger<UsersController> _logger;
    private readonly PgContext _context;

    public ChatsController(ILogger<UsersController> logger, PgContext context)
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<Chat>> GetById(Guid chatId)
    {
      var chat = await _context.Chats
        .Include(c => c.Users)
        .FirstOrDefaultAsync(c => c.Id == chatId);

      if (chat == null)
        return NotFound();

      return chat;
    }

    [HttpPost("getByUsers")]
    public async Task<ActionResult<Chat>> ReturnOrCreateChat([FromBody] List<NewChatWithUsersDto> dto)
    {
      if (dto == null || dto.Count < 2)
      {
        return BadRequest();
      }

      var userIds = dto.Select(d => d.UserId).ToList();

      var users = await _context.Users
        .Where(u => userIds.Contains(u.Id))
        .Include(u => u.Chats).ToListAsync();

      if (users.Count < 2)
        return NotFound();

      if (users == null)
        return NotFound();

      var usersChats = users.Select(u => u.Chats).SelectMany(c => c!);
      var grouped = usersChats.GroupBy(c => c.Id);
      var chatGroups = grouped.Where(g => g.Count() == 2);

      if (chatGroups != null && chatGroups.Count() == 1)
      {
        var existedChatId = chatGroups.First().Key;

        var chat = await _context.Chats.FindAsync(existedChatId);

        if (chat == null)
          return NotFound();

        return chat;
      }

      var newChat = new Chat
      {
        Id = Guid.NewGuid(),
        Users = users,
      };

      _context.Chats.Add(newChat);
      await _context.SaveChangesAsync();
      return newChat;
    }

  }
}