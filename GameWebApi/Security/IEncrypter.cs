namespace Security
{
    public interface IEncrypter
    {
        string Encrypted(string password);
    }
}
