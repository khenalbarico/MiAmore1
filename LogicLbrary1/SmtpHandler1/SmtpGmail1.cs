using LogicLbrary1.Models.Smtp;
using System.Net;
using System.Net.Mail;

namespace LogicLbrary1.SmtpHandler1;
public class SmtpGmail1
{
    public async Task<bool> SendAsync(SmtpPayload payload)
    {
        var smtpCreds = new SmtpCreds();

        var smtp = new SmtpClient(smtpCreds.GmailHost, smtpCreds.GmailTLSPort)
        {
            Credentials = new NetworkCredential(payload.SenderEmail, payload.SenderPassword),
            EnableSsl = true
        };

        var message = new MailMessage(payload.SenderEmail, payload.RecepientEmail)
        {
            Subject = payload.Subject,
            Body = MessageBody1.ReturnCustomizedBody(),
            IsBodyHtml = true
        };

        await smtp.SendMailAsync(message);

        return true;
    }
}
