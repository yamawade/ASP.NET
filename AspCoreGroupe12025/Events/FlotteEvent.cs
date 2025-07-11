namespace AspCoreGroupe12025.Events
{
    public record FlotteEvent(
        int IdFlotte,
        string? TypeFlotte,
        string? MatriculeFlotte,
        string EventType,
        DateTime Timestamp
    );
}