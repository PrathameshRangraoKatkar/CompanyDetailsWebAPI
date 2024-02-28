using CompanyDetailsWebAPI.Repository;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace CompanyDetailsWebAPI.Common
{

    public class MailModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }

    }

    public class MailSender : BaseAsyncRepository
    {
        IConfiguration configuration;

        public MailSender(IConfiguration _configuration) : base(_configuration)
        {
            configuration = _configuration;
        }

        public static string SendUserRegistratuionMail(MailModel Email, IConfiguration configuration)
        {
            var ResetTemplate = @"<html>
                                         <head>
                                        <title>@Model.Subject</title>
                                    </head>
                                    <body>
                                        <h1>Hello @Model.RecipientName,</h1>
                                        <p>Thank you for registering with us!</p>
                                        <p>Best regards,</p>
                                        <p>Your Company</p>
                                    </body>

                                    </html>
                                        ";

            var mailMessage = new MailMessage();
            try
            {
                // Load the email template

                string emailTemplate = @"<!DOCTYPE html>
                                        <html>
                                        <head>
                                            <title>@Model.Subject</title>
                                        </head>
                                        <body>
                                            <h1>Hello @Model.Name,</h1>
                                            <p>@Model.Body</p>
                                            <p>Best regards,</p>
                                            <p>Your Company</p>
                                        </body>
                                        </html>
                                        ";


                string emailBody = ResetTemplate
                    .Replace("@Model.Name", Email.UserName)
                    .Replace("@Model.Link", Email.Link);    

                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(configuration.GetSection("MailInfo:SenderMailAddress").Value,
                        configuration.GetSection("MailInfo:SenderPassKey").Value);
                    mailMessage.From = new MailAddress(configuration.GetSection("MailInfo:SenderMailAddress").Value);
                    mailMessage.To.Add(new MailAddress(Email.Email));
                    mailMessage.Subject = Email.Subject;
                    mailMessage.Body = emailBody;
                    mailMessage.IsBodyHtml = true; 
                    smtpClient.Send(mailMessage);
                }

                return "Email sent successfully.";
            }
            catch (System.Exception ex)
            {
                return $"Error sending email: {ex.Message}";
            }
        }
    }
}
