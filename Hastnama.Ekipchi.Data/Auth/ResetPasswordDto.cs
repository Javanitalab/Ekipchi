using System;
using System.ComponentModel.DataAnnotations;

namespace Hastnama.Ekipchi.Data.Auth
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string ActiveCode { get; set; }
    }
}