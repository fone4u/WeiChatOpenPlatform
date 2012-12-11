using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DomainCore;

namespace WeiChatOpenPlatform.Controllers
{
    public class RegisterController : Controller
    {
        private const string TOKEN = "CareerBuilderChina";
        private const String ERROR_CONTENT = "ERROR";
        public ActionResult Index(String signature, string timestamp, string nonce, string echostr)
        {
            if (String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(timestamp)
                || String.IsNullOrEmpty(nonce) || String.IsNullOrEmpty(echostr))
            {
                String[] qsValues = { TOKEN, timestamp, nonce };
                Array.Sort<String>(qsValues);
                String plainString = String.Join("", qsValues);
                String encryptedString = SHA1Algorithm.Enctypt(plainString);
                if (encryptedString == signature)
                {
                    return new ContentResult() { Content = echostr };
                }
            }
            return new ContentResult() { Content = ERROR_CONTENT };
        }

    }
}
