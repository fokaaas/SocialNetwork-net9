using System.Text.Json.Serialization;

namespace Data.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ConversationRole
{
    Member,
    Admin,
    Owner
}