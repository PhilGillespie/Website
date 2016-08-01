using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using MySite.Models;

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
            return RedirectToCurrentUmbracoPage();
        }
    }
}