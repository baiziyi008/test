using Newtonsoft.Json.Linq;
using repack_shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// delete_data 的摘要说明
    /// </summary>
    public class delete_data : IHttpHandler, IRequiresSessionState
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
                    case "delete_original_package_form"://删除原包
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_package_original_version version = repack_shell.Controller.GetManager().get_original_version(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(version))
                            {
                                try {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + version.pkg_icon_path);
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + version.pkg_path);
                                } catch (Exception) { }
                                repack_shell.table_repark_package_original apk = new repack_shell.table_repark_package_original();
                                apk.id = version.pkg_id;
                                if (repack_shell.Controller.GetManager().delete(apk))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "delete table_repark_package_original error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_package_original_version error";
                            }
                        }
                        break;
                    case "delete_sdk_form"://删除SDK
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_sdk sdk = repack_shell.Controller.GetManager().get_sdk(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(sdk))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_sdk error";
                            }
                        }
                        break;
                    case "delete_keystore_form"://删除签名
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_keystore obj = repack_shell.Controller.GetManager().get_keystore(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_keystore error";
                            }
                        }
                        break;
                    case "delete_channel_form"://删除渠道
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_channel obj = repack_shell.Controller.GetManager().get_channel(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_keystore error";
                            }
                        }
                        break;
                    case "delete_umeng_key_form"://删除Umeng Key
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_umeng obj = repack_shell.Controller.GetManager().get_umeng_key(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_keystore error";
                            }
                        }
                        break;
                    case "delete_3gpp_form"://删除3gpp
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_3gpp obj = repack_shell.Controller.GetManager().get_3gpp(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_3gpp error";
                            }
                        }
                        break;
                    case "delete_gdt_form"://删除广点通
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_reaprk_gdt obj = repack_shell.Controller.GetManager().get_gdt(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_reaprk_gdt error";
                            }
                        }
                        break;
                    case "delete_online_form"://删除在线包
                        {
                            string id = context.Request["id"].ToString();
                            repack_shell.table_repark_package_published obj = repack_shell.Controller.GetManager().get_publish(int.Parse(id));
                            if (repack_shell.Controller.GetManager().delete(obj))
                            {
                                try {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + obj.path);
                                } catch (Exception) { }
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_package_published error";
                            }
                        }
                        break;
                    case "delete_baidussp_form"://删除百度SSP
                        {
                            string id = context.Request["id"].ToString();
                            table_repark_baidussp obj = Controller.GetManager().get_baidussp(int.Parse(id));
                            if (Controller.GetManager().delete(obj))
                            {
                                json["state"] = 1;
                                json["message"] = "ok";
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "delete table_repark_baidussp error";
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