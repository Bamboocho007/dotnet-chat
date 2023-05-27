using myChat.Enums;

namespace myChat.DTO
{
  public class GetMessageQueryDto
  {
    public int Limit { get; set; }
    public MessageDirecton Direction { get; set; } = MessageDirecton.NEXT;
    public DateTime From { get; set; }
  }
}

