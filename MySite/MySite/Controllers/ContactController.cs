using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using MySite.Models;
using System.Net.Mail;
using System.Configuration;

namespace MySite.Controllers
{
    public class ContactController : SurfaceController
    {
        public ActionResult RenderForm()
        {
            return PartialView("ContactForm", new ContactModel());
        }

        public ActionResult RenderServices()
        {
            return PartialView("Services");
        }

        public ActionResult FormPost(ContactModel post)
        {
            var appSettings = ConfigurationManager.AppSettings;
            ContactModel model = new ContactModel()
            {
                Name = post.Name,
                Email = post.Email,
                Message = post.Message
            };
            SmtpClient client = new SmtpClient(appSettings["SMTPClient"], Int32.Parse(appSettings["SMTPClientPort"]));
            MailAddress from = new MailAddress("test@test.com");
            MailAddress to = new MailAddress(appSettings["EmailToAddress"]);
            MailMessage msg = new MailMessage(from, to);
            msg.Body = post.Name + " : " + post.Email + " : " + post.Message;
            msg.Subject = "Message from website";
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential(appSettings["SiteEmailAddress"], appSettings["SiteEmailPassword"]);
            client.EnableSsl = true;
            client.Send(msg);
            return RedirectToCurrentUmbracoPage();
        }
    }
}