namespace AspCoreGroupe12025.Events
{
    public record UserEvent(
        string UserId,
        string Email,
        string EventType,
        DateTime Timestamp
    );
}
