using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using log4net;

namespace MySite.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        private static readonly ILog logger = LogManager.GetLogger(typeof(Contact));

        public void SendEmail() 
        {
            var appSettings = ConfigurationManager.AppSettings;

            try
            {
                SmtpClient client = new SmtpClient(appSettings["SMTPClient"], Int32.Parse(appSettings["SMTPClientPort"]));
                MailAddress from = new MailAddress("test@test.com");
                MailAddress to = new MailAddress(appSettings["EmailToAddress"]);
                MailMessage msg = new MailMessage(from, to);
                msg.Body = this.Name + " : " + this.Email + " : " + this.Message;
                msg.Subject = "Message from website";
                client.Host = "smtp.gmail.com";
                client.Credentials = new System.Net.NetworkCredential(appSettings["SiteEmailAddress"], appSettings["SiteEmailPassword"]);
                client.EnableSsl = true;
                client.Send(msg);
            }
            catch (Exception ex) {
                logger.Error("error sending email: " + ex.Message);
            }
            return;
        }
    }
}