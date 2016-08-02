using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using MySite.Models;
using System.Net.Mail;

namespace MySite.Controllers
{
    public class ContactController : SurfaceController
    {
        public ActionResult RenderForm()
        {
            return PartialView("ContactForm", new ContactModel());
        }

        public ActionResult FormPost(ContactModel post)
        {
            ContactModel model = new ContactModel()
            {
                Name = post.Name,
                Email = post.Email,
                Message = post.Message
            };
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            MailAddress from = new MailAddress("test@test.com");
            MailAddress to = new MailAddress("philgillespie@hotmail.com");
            MailMessage msg = new MailMessage(from, to);
            msg.Body = post.Name + " : " + post.Email + " : " + post.Message;
            msg.Subject = "Message from website";
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("phil.s.gillespie@gmail.com", "me$$ugah77");
            client.EnableSsl = true;
            client.Send(msg);
            return RedirectToCurrentUmbracoPage();
        }
    }
}