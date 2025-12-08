namespace Application.Dtos;

public class CurrentUser
{
    public readonly string Id;
    public readonly string Email;

    public CurrentUser(string id, string email)
    {
        Id = id;
        Email = email;
    }
}