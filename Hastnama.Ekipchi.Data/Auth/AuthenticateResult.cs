namespace Hastnama.Ekipchi.Data.Auth
{
    public class AuthenticateResult : TokenDto
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }
    }
}