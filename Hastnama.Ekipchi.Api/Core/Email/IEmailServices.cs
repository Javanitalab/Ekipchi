﻿using System.Threading.Tasks;

 namespace Hastnama.Ekipchi.Api.Core.Email
{
    public interface IEmailServices
    {
        Task SendMessage(string emailTo, string subject, string body);
    }
}