using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MohDemo.Utility
{
	public class EmailSender : IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			MailMessage mail = new MailMessage();
			mail.To.Add(email);
			mail.From = new MailAddress("cakhdeveloper@gmail.com");
			mail.Subject = subject;
			string Body = htmlMessage;
			mail.Body = Body;
			mail.IsBodyHtml = true;
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new System.Net.NetworkCredential("cakhdeveloper", "eBWR5844L8UAeTd");
			smtp.EnableSsl = true;
			await smtp.SendMailAsync(mail);
		}
	}
}
