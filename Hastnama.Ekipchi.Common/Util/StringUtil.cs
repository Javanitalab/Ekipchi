namespace Hastnama.Ekipchi.Common.Util
{
    public static class StringUtil
    {
        public static string HashPass(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
        
        public static bool CheckPassword(string enterPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(enterPassword, password);
        }

    }
}