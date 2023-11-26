namespace bdd_obligatorio_api.Models;

public class User
{
    public User(string username, string password)
    {
        this.Password = password;
        this.Username = username;
    }
    public string Username { get; }
    public string Password { get; }
}