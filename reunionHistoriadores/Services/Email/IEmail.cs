using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace reunionhistoriadores2025.Services.Email
{
    public interface IEmail
    {
        public Task<bool> EnviaCorreoAsync(string tema, string para, string cc, string bcc, string cuerpo, Attachment adjunto = null);
    }
}
