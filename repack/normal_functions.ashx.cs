using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// normal_functions 的摘要说明
    /// </summary>
    public class normal_functions : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JObject json = new JObject();
            json["state"] = 0;
            json["message"] = "undefined request";
            context.Response.ContentType = "text/plain";
            do
            {
                string type = context.Request["type"].ToString();
                switch (type)
                {
                    case "login"://用户登录
                        {
                            string account = context.Request["account"].ToString();
                            string pwd = context.Request["pwd"].ToString();
                        }
                        break;
                    default: break;
                }
            } while (false);
            context.Response.Write(json.ToString());
            context.Response.End();
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