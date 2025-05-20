namespace WebSmonder.Data.Entities;

public class AccountEntity
{
    public int Id { get; set; } 
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
