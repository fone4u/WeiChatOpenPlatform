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
using System.Xml;


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
            MsgRepositry.Messages.Add(xmlContent);

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

            String xmlResponseContent = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter xw = new XmlTextWriter(stream, UTF8Encoding.UTF8);
                xw.WriteStartElement("xml");

                xw.WriteStartElement("ToUserName");
                xw.WriteCData(rsp.ToUserName);
                xw.WriteEndElement();

                xw.WriteStartElement("FromUserName");
                xw.WriteCData(rsp.FromUserName);
                xw.WriteEndElement();

                xw.WriteStartElement("CreateTime");
                xw.WriteValue(rsp.CreateTime);
                xw.WriteEndElement();

                xw.WriteStartElement("MsgType");
                xw.WriteCData(rsp.MsgType);
                xw.WriteEndElement();

                xw.WriteStartElement("Content");
                xw.WriteCData(rsp.Content);
                xw.WriteEndElement();

                xw.WriteStartElement("FuncFlag");
                xw.WriteValue(0);
                xw.WriteEndElement();


                xw.WriteEndElement();

                xw.Flush();

                stream.Position = 0;

                using (StreamReader sr = new StreamReader(stream))
                {
                    xmlResponseContent = sr.ReadToEnd();
                }
            }
            return new ContentResult() { Content = xmlResponseContent };

        }

    }
}
