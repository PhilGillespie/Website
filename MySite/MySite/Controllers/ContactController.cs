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
            return PartialView("ContactForm", new Contact());
        }

        public ActionResult RenderServices()
        {
            return PartialView("Services");
        }

        public ActionResult FormPost(Contact post)
        {
            
            Contact contact = new Contact()
            {
                Name = post.Name,
                Email = post.Email,
                Message = post.Message
            };
            contact.SendEmail();            
            return RedirectToCurrentUmbracoPage();
        }
    }
}