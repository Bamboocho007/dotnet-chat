using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myChat.DB;
using myChat.DB.Models;
using myChat.DTO;

namespace myChat.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MessagesController : ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly PgContext _context;

    public MessagesController(ILogger<UsersController> logger, PgContext context)
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet("{chatId}")]
    public async Task<ActionResult<IEnumerable<Message>>> Get(Guid chatId, [FromQuery] GetMessageQueryDto query)
    {
      var messages = await _context.Messages
        .Where(m => m.ChatId == chatId)
        .Include(m => m.User)
        .ToListAsync();

      if (messages == null)
      {
        return NotFound();
      }

      return messages;
    }

    [HttpPost("{chatId}")]
    public async Task<ActionResult<Message>> Post(Guid chatId, [FromBody] NewMessageDto dto)
    {
      if (dto == null)
      {
        return BadRequest();
      }

      var newMessage = new Message
      {
        Id = Guid.NewGuid(),
        Text = dto.Text,
        ChatId = chatId,
        UserId = dto.UserId,
        CreatedDate = new DateTime()
      };

      await _context.Messages.AddAsync(newMessage);

      return newMessage;
    }
  }
}