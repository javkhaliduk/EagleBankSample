namespace EagleBank.code
{
    public interface IJWTGenerator
    {
        public string GenerateToken(string id);
    }
}
