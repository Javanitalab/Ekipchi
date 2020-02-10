﻿using System.ComponentModel.DataAnnotations;

 namespace Hastnama.Ekipchi.Data.Auth
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}