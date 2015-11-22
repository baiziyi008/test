using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using repack_shell;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// insert_data 的摘要说明
    /// </summary>
    public class insert_data : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JObject json = new JObject();
            json["state"] = 0;
            json["message"] = "undefined request";
            context.Response.ContentType = "text/plain";
            do {
                string type = context.Request["type"].ToString();
                switch (type) {
                    #region 新增原包
                    case "new_original_package_form"://新增原包
                        {
                            string apk_path = context.Request["apk_path"].ToString();
                            string apk_icon = context.Request["apk_icon"].ToString();
                            string apk_packagename = context.Request["apk_packagename"].ToString().ToLower();
                            string apk_appname = context.Request["apk_appname"].ToString();
                            string apk_versioncode = context.Request["apk_versioncode"].ToString();
                            string apk_versionstring = context.Request["apk_versionstring"].ToString();
                            string apk_activity = context.Request["apk_activity"].ToString();
                            string apk_application = context.Request["apk_application"].ToString();
                            string apk_title = context.Request["apk_title"].ToString();
                            if (!repack_shell.Controller.GetManager().exist_original_package(apk_packagename)) {
                                repack_shell.table_repark_package_original apk = new repack_shell.table_repark_package_original();
                                string timestamp = repack_shell.utils.get_time_stamp();
                                string original_apk_path = data.OriginalApk + @"\" + timestamp + ".apk";
                                string original_icon_path = data.OriginalApk + @"\" + timestamp + ".png";
                                apk.path = original_apk_path;
                                apk.content = apk_title;
                                apk.pkg_packagename = apk_packagename;
                                apk.state = 0;
                                apk.ctime = timestamp;
                                string id = string.Empty;
                                if (repack_shell.Controller.GetManager().add(apk,ref id))
                                {
                                    repack_shell.table_repark_package_original_version version = new repack_shell.table_repark_package_original_version();
                                    
                                    version.pkg_id = int.Parse(id);
                                    version.pkg_path = original_apk_path.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                                    version.pkg_versioncode = apk_versioncode;
                                    version.pkg_versionstring = apk_versionstring;
                                    version.pkg_label = apk_appname;
                                    version.pkg_icon_path = original_icon_path.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                                    version.pkg_main_activity = apk_activity;
                                    version.pkg_application_name = apk_application;
                                    version.title = apk_title;
                                    version.content = "";
                                    version.ctime = timestamp;
                                    if (repack_shell.Controller.GetManager().add(version, ref id))
                                    {
                                        try {
                                            File.Copy(AppDomain.CurrentDomain.BaseDirectory + apk_path, original_apk_path, true);
                                            File.Copy(AppDomain.CurrentDomain.BaseDirectory + apk_icon, original_icon_path, true);
                                        }
                                        catch (Exception) { }
                                        json["state"] = 1;
                                        json["message"] = "ok";
                                    }
                                    else {
                                        json["state"] = 0;
                                        json["message"] = "add table_repark_package_original_version error";
                                    }
                                }
                                else {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_package_original error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist package";
                            }
                        }
                        break;
                    #endregion
                    #region 新增SDK
                    case "new_sdk_form"://新增SDK
                        {
                            string sdk_path = context.Request["sdk_path"].ToString();
                            string sdk_codeclass = context.Request["sdk_codeclass"].ToString();
                            string sdk_title = context.Request["sdk_title"].ToString();
                            string sdk_content = context.Request["sdk_content"].ToString();
                            string sdk_version = context.Request["sdk_version"].ToString();

                            if (!repack_shell.Controller.GetManager().exist_sdk(sdk_path, sdk_codeclass))
                            {
                                repack_shell.table_repark_sdk sdk = new repack_shell.table_repark_sdk();
                                string timestamp = repack_shell.utils.get_time_stamp();
                                sdk.sdk_title = sdk_title;
                                sdk.sdk_content = sdk_content;
                                sdk.sdk_path = sdk_path;
                                sdk.sdk_codeclass = sdk_codeclass;
                                sdk.sdk_versioncode = sdk_version;
                                sdk.ctime = timestamp;
                                string id = string.Empty;
                                if (repack_shell.Controller.GetManager().add(sdk, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_sdk error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist sdk";
                            }
                        }
                        break;
                    #endregion
                    #region 新增签名
                    case "new_keystore_form"://新增签名
                        {
                            string keystore_title = context.Request["keystore_title"].ToString();
                            string keystore_pwd = context.Request["keystore_pwd"].ToString();
                            string keystore_key_alias = context.Request["keystore_key_alias"].ToString();
                            string keystore_key_pwd = context.Request["keystore_key_pwd"].ToString();
                            string timestamp = repack_shell.utils.get_time_stamp();
                            string keystore_path = data.KeystoreFolder + @"\" + timestamp + ".keystore";
                            context.Request.Files[0].SaveAs(keystore_path);
                            string keystore_md5 = repack_shell.utils.GetMD5HashFromFile(keystore_path);
                            if (!repack_shell.Controller.GetManager().exist_keystore(keystore_md5))
                            {
                                repack_shell.table_repark_keystore keystore = new repack_shell.table_repark_keystore();
                                keystore.title = keystore_title;
                                keystore.path = keystore_path.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                                keystore.alias = keystore_key_alias;
                                keystore.pwd1 = keystore_pwd;
                                keystore.pwd2 = keystore_key_pwd;
                                keystore.md5 = keystore_md5;
                                keystore.ctime = timestamp;
                                string id = string.Empty;
                                if (repack_shell.Controller.GetManager().add(keystore, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_keystore error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist keystore";
                            }
                        }
                        break;
                    #endregion
                    #region 新增渠道
                    case "new_channel_form"://新增渠道
                        {
                            string channel_name = context.Request["channel_name"].ToString();
                            string channel_sign = context.Request["channel_sign"].ToString();
                            if (!repack_shell.Controller.GetManager().exist_channel(channel_name,channel_sign))
                            {
                                string timestamp = repack_shell.utils.get_time_stamp();
                                repack_shell.table_repark_channel obj = new repack_shell.table_repark_channel();
                                obj.channel_name = channel_name;
                                obj.channel_sign = channel_sign;
                                obj.ctime = timestamp;
                                string id = string.Empty;
                                if (repack_shell.Controller.GetManager().add(obj, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_channel error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist keystore";
                            }
                        }
                        break;
                    #endregion
                    #region 新增友盟KEY
                    case "new_umeng_key_form"://新增友盟KEY
                        {
                            string key_name = context.Request["key_name"].ToString();
                            string key_content = context.Request["key_content"].ToString();
                            string key_appkey = context.Request["key_appkey"].ToString();
                            string key_message_key = context.Request["key_message_key"].ToString();
                            if (!repack_shell.Controller.GetManager().exist_umeng_key(key_appkey))
                            {
                                string timestamp = repack_shell.utils.get_time_stamp();
                                repack_shell.table_repark_umeng obj = new repack_shell.table_repark_umeng();
                                obj.title = key_name;
                                obj.content = key_content;
                                obj.umeng_key = key_appkey;
                                obj.umeng_message_key = key_message_key;
                                obj.umeng_channel = "0";
                                obj.ctime = timestamp;
                                string id = string.Empty;
                                if (repack_shell.Controller.GetManager().add(obj, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_umeng error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist umeng key";
                            }
                        }
                        break;
                    #endregion
                    #region 新申请3GPP
                    case "new_3gpp_new_form"://新申请3GPP
                        {
                            string new_name = context.Request["new_name"].ToString();
                            string new_packagename = context.Request["new_packagename"].ToString();
                            string new_productname = context.Request["new_productname"].ToString();
                            string new_pid = context.Request["new_pid"].ToString();
                            string new_channel_name = context.Request["new_channel_name"].ToString();
                            string new_cid = context.Request["new_cid"].ToString();

                            string new_appkey = shell_utils.get_3gpp_appkey(new_productname, new_packagename, new_channel_name, new_pid, new_cid);

                            if (new_appkey == "")
                            {
                                json["state"] = 0;
                                json["message"] = "request 3gpp error!";
                            }
                            else {
                                if (!Controller.GetManager().exist_3gpp(new_packagename))
                                {
                                    string timestamp = repack_shell.utils.get_time_stamp();
                                    table_repark_3gpp obj = new table_repark_3gpp();
                                    obj.title = new_name;
                                    obj.content = "";
                                    obj.packagename = new_packagename;
                                    obj.appkey = new_appkey;
                                    obj.g3pp_product_name = new_productname;
                                    obj.g3pp_channel_name = new_channel_name;
                                    obj.g3pp_pid = "0";
                                    obj.g3pp_cid = "0";
                                    obj.local_pid = new_pid;
                                    obj.local_cid = new_cid;
                                    obj.ctime = timestamp;
                                    string id = string.Empty;
                                    if (Controller.GetManager().add(obj, ref id))
                                    {
                                        json["state"] = 1;
                                        json["message"] = "ok";
                                    }
                                    else
                                    {
                                        json["state"] = 0;
                                        json["message"] = "add table_repark_3gpp error";
                                    }
                                }
                                else {
                                    json["state"] = 0;
                                    json["message"] = "exist 3gpp [packagename]!";
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 添加已经申请的3GPP
                    case "new_3gpp_old_form"://添加已经申请的3GPP
                        {
                            string old_name = context.Request["old_name"].ToString();
                            string old_packagename = context.Request["old_packagename"].ToString();
                            string old_appkey = context.Request["old_appkey"].ToString();
                            string old_productname = context.Request["old_productname"].ToString();
                            string old_pid = context.Request["old_pid"].ToString();
                            string old_channel_name = context.Request["old_channel_name"].ToString();
                            string old_cid = context.Request["old_cid"].ToString();
                            string old_3gpp_pid = context.Request["old_3gpp_pid"].ToString();
                            string old_3gpp_cid = context.Request["old_3gpp_cid"].ToString();

                            if (!Controller.GetManager().exist_3gpp(old_packagename))
                            {
                                string timestamp = repack_shell.utils.get_time_stamp();
                                table_repark_3gpp obj = new table_repark_3gpp();
                                obj.title = old_name;
                                obj.content = "";
                                obj.packagename = old_packagename;
                                obj.appkey = old_appkey;
                                obj.g3pp_product_name = old_productname;
                                obj.g3pp_channel_name = old_channel_name;
                                obj.g3pp_pid = old_3gpp_pid;
                                obj.g3pp_cid = old_3gpp_cid;
                                obj.local_pid = old_pid;
                                obj.local_cid = old_cid;
                                obj.ctime = timestamp;
                                string id = string.Empty;
                                if (Controller.GetManager().add(obj, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_3gpp error";
                                }
                            }
                            else {
                                json["state"] = 0;
                                json["message"] = "exist 3gpp";
                            }
                        }
                        break;
                    #endregion
                    #region 添加广点通
                    case "new_gdt_form"://添加广点通
                        {
                            string gdt_name = context.Request["gdt_name"].ToString();
                            string gdt_packagename = context.Request["gdt_packagename"].ToString();
                            string gdt_appkey = context.Request["gdt_appkey"].ToString();
                            string gdt_appid = context.Request["gdt_appid"].ToString();
                            string gdt_insert_adid = context.Request["gdt_insert_adid"].ToString();
                            string gdt_start_adid = context.Request["gdt_start_adid"].ToString();

                            if (!Controller.GetManager().exist_gdt(gdt_packagename))
                            {
                                string timestamp = repack_shell.utils.get_time_stamp();
                                table_reaprk_gdt obj = new table_reaprk_gdt();
                                obj.title = gdt_name;
                                obj.content = "";
                                obj.gdt_packagename = gdt_packagename;
                                obj.gdt_appkey = gdt_appkey;
                                obj.gdt_appid = gdt_appid;
                                obj.gdt_insert_adid = gdt_insert_adid;
                                obj.gdt_start_adid = gdt_start_adid;
                                obj.ctime = timestamp;
                                string id = string.Empty;
                                if (Controller.GetManager().add(obj, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_reaprk_gdt error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist gdt";
                            }
                        }
                        break;
                    #endregion
                    #region 添加百度SSP
                    case "new_baidussp_form"://添加百度SSP
                        {
                            string name = context.Request["name"].ToString();
                            string appid = context.Request["appid"].ToString();
                            string insert_adid = context.Request["insert_adid"].ToString();
                            string start_adid = context.Request["start_adid"].ToString();

                            if (!Controller.GetManager().exist_baidussp(appid))
                            {
                                string timestamp = repack_shell.utils.get_time_stamp();
                                table_repark_baidussp obj = new table_repark_baidussp();
                                obj.title = name;
                                obj.content = "";
                                obj.uid = (context.Session["repark_uid"] == null) ? context.Session["repark_uid"].ToString() : "0";
                                obj.baidussp_appid = appid;
                                obj.baidussp_insert_adid = insert_adid;
                                obj.baidussp_start_adid = start_adid;
                                obj.ctime = timestamp;
                                string id = string.Empty;
                                if (Controller.GetManager().add(obj, ref id))
                                {
                                    json["state"] = 1;
                                    json["message"] = "ok";
                                }
                                else
                                {
                                    json["state"] = 0;
                                    json["message"] = "add table_repark_baidussp error";
                                }
                            }
                            else
                            {
                                json["state"] = 0;
                                json["message"] = "exist baidussp";
                            }
                        }
                        break;
                    #endregion
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