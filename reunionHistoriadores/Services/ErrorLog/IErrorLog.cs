using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace reunionhistoriadores2025.Services.ErrorLog
{
    public interface IErrorLog
    {
        public Task ErrorLogAsync(string Mensaje);
    }
}
