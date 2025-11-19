namespace Template.Domain.Exceptions
{
    public class ForbiddenException(string action) : Exception($"{action} is forbidden")
    {
    }
}
