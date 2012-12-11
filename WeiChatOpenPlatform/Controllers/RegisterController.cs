using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DomainCore;
using RestSharp.Deserializers;
using RestSharp.Serializers;

using RestSharp;
using System.IO;


namespace WeiChatOpenPlatform.Controllers
{
    public class RegisterController : Controller
    {
        private const string TOKEN = "CareerBuilderChina";
        private const String ERROR_CONTENT = "ERROR";

        [HttpGet]
        public ActionResult Index(String signature, string timestamp, string nonce, string echostr)
        {
            return new ContentResult() { Content = echostr };

            //if (!(String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(timestamp)
            //    || String.IsNullOrEmpty(nonce) || String.IsNullOrEmpty(echostr)))
            //{
            //    String[] qsValues = { TOKEN, timestamp, nonce };
            //    Array.Sort<String>(qsValues);
            //    String plainString = String.Join("", qsValues);
            //    String encryptedString = SHA1Algorithm.Enctypt(plainString);
            //    if (encryptedString == signature)
            //    {
            //        return new ContentResult() { Content = echostr };
            //    }
            //}
            //return new ContentResult() { Content = ERROR_CONTENT };
        }

        [HttpPost]
        public ActionResult Index()
        {
            string xmlContent = "";
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                xmlContent = sr.ReadToEnd();
            }
            XmlDeserializer xds = new XmlDeserializer();
            WeiChatRequest request = xds.Deserialize<WeiChatRequest>(new RestResponse() { Content = xmlContent });
            TextRequest req = new TextRequest(request);

            TextResponse rsp = new TextResponse()
            {
                Content = "测试",
                CreateTime = request.CreateTime + 10,
                FromUserName = "工作小助手",
                ToUserName = req.FromUserName,
                MsgType = "text",
                FuncFlag = 1
            };
            XmlSerializer xs = new XmlSerializer();
            xs.DateFormat = "";
            String xml = xs.Serialize(rsp);
            xml = xml.Replace("TextResponse", "xml");
            return new ContentResult() { Content = xml };

        }

    }
}
