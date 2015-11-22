using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// upload_file 的摘要说明
    /// </summary>
    public class upload_file : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JObject json = new JObject();
            context.Response.ContentType = "text/plain";
            do
            {
                var files = context.Request.Files;
                if (files.Count == 0) {
                    json["state"] = 0;
                    json["message"] = "没有上传文件！";
                    break;
                }
                string time_str = DateTime.Now.ToString().Replace("-", "").Replace(":", "").Replace("/", "").Replace(" ", "");
                string work_apk = data.DecompilerProject + @"\" + time_str + @".apk";
                string type = context.Request["type"].ToString();
                switch (type)
                {
                    case "new_original_package":
                        {
                            files[0].SaveAs(work_apk);
                            repack_shell.ShellPublic shell_apk = new repack_shell.ShellPublic();
                            shell_apk.Init(work_apk, string.Empty, null);
                            shell_apk.m_Func.decompiler_apk();
                            shell_apk.ReadSettings();
                            repack_tools.tool_apk_model apkinfo = shell_apk.GetApkInfo();
                            json["state"] = 1;
                            json["message"] = "ok";
                            JObject json_apkinfo = new JObject();
                            json_apkinfo["filepath"] = apkinfo.ApkPath.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                            json_apkinfo["packagename"] = apkinfo.settings.PackageName;
                            json_apkinfo["icon"] = "";
                            if (apkinfo.settings.IconFileName != "") {
                                json_apkinfo["icon"] = apkinfo.settings.IconFileName.Replace(AppDomain.CurrentDomain.BaseDirectory,"");
                            }
                            json_apkinfo["appname"] = apkinfo.settings.AppName;
                            json_apkinfo["versioncode"] = apkinfo.settings.VersionCode;
                            json_apkinfo["versionstring"] = apkinfo.settings.VersionString;
                            json_apkinfo["activity"] = apkinfo.settings.MainActivity;
                            json_apkinfo["application"] = apkinfo.settings.AndroidApplication;
                            json["apkinfo"] = json_apkinfo;
                        }
                        break;
                    default:
                        break;
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