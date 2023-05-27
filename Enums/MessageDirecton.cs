using System.Text.Json.Serialization;

namespace myChat.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum MessageDirecton
  {
    PREVIOUS,
    NEXT,
  }
}