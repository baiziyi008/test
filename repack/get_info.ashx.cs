using Newtonsoft.Json.Linq;
using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// get_info 的摘要说明
    /// </summary>
    public class get_info : IHttpHandler, IRequiresSessionState
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
                    case "get_original_version_info"://获取原包最新版本信息
                        {
                            string id = context.Request["id"].ToString();
                            table_repark_package_original_version obj = Controller.GetManager().get_original_version(int.Parse(id));
                            if (obj!=null)
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                                table_repark_package_original original = Controller.GetManager().get_original(obj.pkg_id);
                                JObject json_version = new JObject();
                                json_version["packagename"] = original.pkg_packagename;
                                json_version["title"] = obj.title;
                                json_version["pkg_path"] = obj.pkg_path;
                                json_version["pkg_versioncode"] = obj.pkg_versioncode;
                                json_version["pkg_versionstring"] = obj.pkg_versionstring;
                                json_version["pkg_label"] = obj.pkg_label;
                                json_version["pkg_icon_path"] = obj.pkg_icon_path;
                                json_version["pkg_main_activity"] = obj.pkg_main_activity;
                                json_version["pkg_application_name"] = obj.pkg_application_name;
                                json["info"] = json_version;
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "can not find table_repark_package_original_version";
                            }
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