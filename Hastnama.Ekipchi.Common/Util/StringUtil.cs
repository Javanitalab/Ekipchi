namespace Hastnama.Ekipchi.Common.Util
{
    public static class StringUtil
    {
        public static string HashPass(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }
}