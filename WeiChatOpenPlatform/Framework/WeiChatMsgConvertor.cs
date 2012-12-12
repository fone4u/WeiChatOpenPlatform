using DomainCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace WeiChatOpenPlatform.Framework
{
    public class WeiChatMsgConvertor
    {
        public static Message ConverToMessage(String request)
        {
            return Message.CreateFromXml(request);
        }

        public static String ConvertToWeiChatResponse(Message message)
        {
            String xmlResponseContent = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlTextWriter xw = new XmlTextWriter(stream, UTF8Encoding.UTF8);
                xw.WriteStartElement("xml");

                xw.WriteStartElement("ToUserName");
                xw.WriteCData(message.ToUserName);
                xw.WriteEndElement();

                xw.WriteStartElement("FromUserName");
                xw.WriteCData(message.FromUserName);
                xw.WriteEndElement();

                xw.WriteStartElement("CreateTime");
                xw.WriteValue((int)(message.CreateTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
                xw.WriteEndElement();

                xw.WriteStartElement("MsgType");
                xw.WriteCData(message.MsgType.ToString());
                xw.WriteEndElement();

                if (message is TextMessage)
                {
                    xw.WriteStartElement("Content");
                    xw.WriteCData((message as TextMessage).Content);
                    xw.WriteEndElement();
                }
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
            return xmlResponseContent;
        }
    }
}