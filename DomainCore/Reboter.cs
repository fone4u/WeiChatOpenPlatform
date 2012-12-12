using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCore
{
    public class Reboter
    {
        public static String TryToUnderStand(String from, string content)
        {
            if (content == "你是谁")
            {
                return "我是小C机器人";
            }
            else
            {
                return "我正在理解您的内容";
            }
        }
    }
}
