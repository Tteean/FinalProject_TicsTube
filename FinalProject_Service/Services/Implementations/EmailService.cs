using FinalProject_Service.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace FinalProject_Service.Services.Implementations
{
    public class EmailService:IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nailato@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ticstubeinfo@gmail.com", "rxxs pfwt yyyd vncx");
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
