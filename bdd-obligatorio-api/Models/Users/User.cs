namespace bdd_obligatorio_api.Models;

public class User
{
    public User(Guid id, string username, string password)
    {
        this.Id = id;
        this.Password = password;
        this.Username = username;
    }
    public Guid Id { get; }
    public string Username { get; }
    public string Password { get; }
}