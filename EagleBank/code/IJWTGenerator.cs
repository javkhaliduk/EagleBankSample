namespace EagleBank.code
{
    public interface IJWTGenerator
    {
        public TokenResponse GenerateToken(string id);
    }
}
