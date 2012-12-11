using DomainCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiChatOpenPlatform.Controllers
{
    public class ViewMessagesController : Controller
    {
        
        public ActionResult Index()
        {
            return View(MsgRepositry.Messages);
        }

    }
}
