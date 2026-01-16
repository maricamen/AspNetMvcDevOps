using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.DirectoryServices;
using System.Net.Mail;

namespace reunionhistoriadores2025.Services.Email
{
    public class Email : IEmail
    {
        private readonly ILogger<Email> _logger;
        private readonly GlobalOptions _opciones;

        public Email(ILogger<Email> logger, IOptionsSnapshot<GlobalOptions> opciones)
        {
            _logger = logger;
            _opciones = opciones.Value;
        }

        [HttpPost]
        public Task<bool> EnviaCorreoAsync(string tema, string para, string cc, string bcc, string cuerpo, Attachment adjunto = null)
        {
            bool res = false;
            string asunto = "Confirmación– XVII Reunión Internacional de Historiadores de México";
            try
            {
                //Rutina para mandar el mail            
                MailMessage eMail = new();
                if (para != null)
                    eMail.To.Add(para);
                if (cc != null)
                    eMail.CC.Add(cc);
                if (bcc != null)
                    eMail.Bcc.Add(bcc);

                // Copias administrativas
                //eMail.Bcc.Add("maricperez@uv.mx");
                if (Convert.ToBoolean(_opciones.HabilitaProduccion))
                {
                    eMail.Bcc.Add("jbarrera@uv.mx");
                    //eMail.Bcc.Add(_opciones.CorreoEvento);
                }

                eMail.From = new MailAddress(_opciones.SmtpUser);
                //if (string.IsNullOrEmpty(tema))
                //    tema = "[sin asunto]";
                eMail.Subject = asunto;
                eMail.Body = cuerpo;
                if (adjunto != null)
                    eMail.Attachments.Add(adjunto);
                eMail.BodyEncoding = System.Text.Encoding.UTF8;
                eMail.SubjectEncoding = System.Text.Encoding.UTF8;
                eMail.HeadersEncoding = System.Text.Encoding.UTF8;
                eMail.IsBodyHtml = true;

                SmtpClient clienteSMTP = new(_opciones.SmtpServer);
                clienteSMTP.Port = 587;
                clienteSMTP.EnableSsl = true;
                clienteSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                clienteSMTP.UseDefaultCredentials = false;
                clienteSMTP.Credentials = new System.Net.NetworkCredential(_opciones.SmtpUser, _opciones.SmtpPwd);

                //Envia el correo
                //clienteSMTP.SendAsync(eMail, null);
                res = true;
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex.Message);
            }
            return Task.FromResult(res);
        }
    }
}
