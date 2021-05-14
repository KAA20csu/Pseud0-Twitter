using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twi
{
    /// <summary>
    /// Сводное описание для Chatter
    /// </summary>
    public class Chatter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Привет всем!");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}