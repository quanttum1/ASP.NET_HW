namespace WebSmonder.Interfaces;

public interface IPasswordService
{
    public void CreateHash(string password, out byte[] hash, out byte[] salt);
    public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
}
