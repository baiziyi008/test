using Newtonsoft.Json.Linq;
using repack_shell;
using repack_tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace repack
{
    /// <summary>
    /// create_pack_task 的摘要说明
    /// </summary>
    public class create_pack_task : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JObject json = new JObject();
            json["state"] = 0;
            json["message"] = "undefined error message";
            context.Response.ContentType = "text/plain";

            string timestamp = utils.get_time_stamp();

            string package_original = context.Request["package_original"].ToString();
            string base_name_check = context.Request["base_name_check"].ToString();
            string app_name = context.Request["app_name"].ToString();
            string base_version_check = context.Request["base_version_check"].ToString();
            string app_version_code = context.Request["app_version_code"].ToString();
            string app_version_string = context.Request["app_version_string"].ToString();
            string base_icon_check = context.Request["base_icon_check"].ToString();
            string app_keystore = context.Request["app_keystore"].ToString();
            string umeng_check = context.Request["umeng_check"].ToString();
            string umeng_check_push = context.Request["umeng_check_push"].ToString();
            string umeng_type = context.Request["umeng_type"].ToString();
            string umeng_key = context.Request["umeng_key"].ToString();
            string gdt_check = context.Request["gdt_check"].ToString();
            string gdt_type = context.Request["gdt_type"].ToString();
            string gdt_key = context.Request["gdt_key"].ToString();
            string hao123_check = context.Request["hao123_check"].ToString();
            string g3pp_check = context.Request["g3pp_check"].ToString();
            string g3pp_type = context.Request["g3pp_type"].ToString();
            string g3pp_key = context.Request["g3pp_key"].ToString();
            string txsg_check = context.Request["txsg_check"].ToString();
            string txpush_check = context.Request["txpush_check"].ToString();
            string app_channle_list = context.Request["app_channle_list"].ToString();
            string baidussp_check = context.Request["baidussp_check"].ToString();
            string baidussp_key = context.Request["baidussp_key"].ToString();

            repark_transfer_data transfer_data = new repark_transfer_data();

            //保存ICON
            var files = context.Request.Files;
            string icon_path = string.Empty;
            if (files.Count == 1 && base_icon_check == "1")
            {
                icon_path = data.TempFolder + @"\" + timestamp + ".png";
                files[0].SaveAs(icon_path);
                transfer_data.use_icon = true;
                transfer_data.iconpath = icon_path;
            }

            //说明文字
            string content = string.Empty;

            //获取要打的SDK列表
            content = "接入的SDK有 （";
            List<SdkType> sdk_types = new List<SdkType>();
            string sdkids = string.Empty;
            table_repark_sdk sdk = null;
            sdk = Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.umeng_game]);
            if (umeng_check == "1")
            {
                if (umeng_type == "aly")
                    sdk_types.Add(SdkType.umeng_aly);
                else
                    sdk_types.Add(SdkType.umeng_game);
                transfer_data.umeng_key = Controller.GetManager().get_umeng_key(int.Parse(umeng_key));
            }
            if (umeng_check_push == "1")
            {
                sdk_types.Add(SdkType.umeng_push);
                transfer_data.umeng_key = Controller.GetManager().get_umeng_key(int.Parse(umeng_key));
            }
            if (gdt_check == "1")
            {
                if (gdt_type == "normal")
                    sdk_types.Add(SdkType.gdt);
                else
                    sdk_types.Add(SdkType.gdtx5);
                transfer_data.gdt_key = Controller.GetManager().get_gdt(int.Parse(gdt_key));
            }
            if (baidussp_check == "1")
            {
                sdk_types.Add(SdkType.baidussp);
                transfer_data.baidussp_key = Controller.GetManager().get_baidussp(int.Parse(baidussp_key));
            }
            if (hao123_check == "1")
                sdk_types.Add(SdkType.hao123);
            if (g3pp_check == "1")
            {
                if (g3pp_type == "normal")
                    sdk_types.Add(SdkType.g3pp);
                else
                    sdk_types.Add(SdkType.g3ppgame);
                transfer_data.g3pp_key = Controller.GetManager().get_3gpp(int.Parse(g3pp_key));
            }
            if (txsg_check == "1")
                sdk_types.Add(SdkType.txsg);
            if (txpush_check == "1")
                sdk_types.Add(SdkType.txpush);
            if (sdk_types.Count > 0)
            {
                foreach (SdkType type in sdk_types)
                {
                    sdk = Controller.GetManager().get_sdk(shell_env.sdk_class_dict[type]);
                    if (sdk != null)
                    {
                        sdkids += sdk.id.ToString() + ",";
                        content += sdk.sdk_title + " ,";
                    }
                }
                sdkids = sdkids.TrimEnd(',');
                content = content.TrimEnd(',');
            }
            content += "） \r\n";
            if (base_name_check == "1")
            {
                transfer_data.use_displayname = true;
                transfer_data.displayname = app_name;
                content += "改名为：" + app_name + " \r\n";
            }
            if (base_version_check == "1")
            {
                transfer_data.use_version = true;
                transfer_data.versioncode = app_version_code;
                transfer_data.versionstring = app_version_string;
                content += "改版本为：" + app_version_string + "(" + app_version_code + ") \r\n";
            }
            transfer_data.sdks = sdk_types;

            content += "打包时间：" + DateTime.Now.ToString();

            //渠道和保存路径字典
            Dictionary<string, string> channel_and_path_dict = new Dictionary<string, string>();
            string channel_ids = string.Empty;
            string paths = string.Empty;
            if (app_channle_list != "")
            {
                List<string> channel_id_list = app_channle_list.Split(',').ToList();
                table_repark_channel channel = null;
                string save_apk_path = string.Empty;
                foreach (string cid in channel_id_list)
                {
                    channel = Controller.GetManager().get_channel(int.Parse(cid));
                    if (channel != null)
                    {
                        save_apk_path = data.PublishApk + @"\" + timestamp + "_" + channel.channel_sign + ".apk";
                        channel_and_path_dict.Add(channel.channel_sign, save_apk_path);

                        channel_ids += cid + ",";
                        paths += save_apk_path.Replace(AppDomain.CurrentDomain.BaseDirectory,"") + ",";
                    }
                }
                channel_ids = channel_ids.TrimEnd(',');
                paths = paths.TrimEnd(',');
            }
            transfer_data.channel_and_path_dict = channel_and_path_dict;

            //读取签名
            table_repark_keystore keystore_record = Controller.GetManager().get_keystore(int.Parse(app_keystore));
            tool_keystore keystore = new tool_keystore();
            keystore.KeystorePath = AppDomain.CurrentDomain.BaseDirectory + keystore_record.path;
            keystore.Alias = keystore_record.alias;
            keystore.Password1 = keystore_record.pwd1;
            keystore.Password2 = keystore_record.pwd2;
            //读取原包
            table_repark_package_original_version original_version_apk = Controller.GetManager().get_original_version(int.Parse(package_original));
            table_repark_package_original original_apk = Controller.GetManager().get_original(original_version_apk.pkg_id);
            try
            {
                //开始打包
                ShellPublic shell = shell_router.smart_run_shell(original_apk.pkg_packagename);
                shell.Init(AppDomain.CurrentDomain.BaseDirectory + original_version_apk.pkg_path, "", keystore);
                shell.SetTransferData(transfer_data);
                shell.RunTask();

                //插入数据
                table_repark_package_published package = new table_repark_package_published();
                package.original_id = package_original;
                package.keystore_id = app_keystore;
                package.g3pp_id = g3pp_key;
                package.umeng_id = umeng_key;
                package.channel_id = channel_ids;
                package.path = paths;
                package.user_id = "0";
                package.sdk_ids = sdkids;
                package.content = content;
                package.ctime = timestamp;
                package.app_name = transfer_data.displayname;
                package.app_package_name = original_apk.pkg_packagename;
                package.app_version_code = app_version_code;
                package.app_version_string = app_version_string;
                if (transfer_data.iconpath != string.Empty)
                {
                    string new_icon_filepath = data.PublishApk + @"\" + Path.GetFileName(transfer_data.iconpath);
                    File.Copy(transfer_data.iconpath, new_icon_filepath);
                    File.Delete(transfer_data.iconpath);
                    package.app_icon = new_icon_filepath.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                }
                else {
                    package.app_icon = original_version_apk.pkg_icon_path;
                }

                string id = string.Empty;
                if (Controller.GetManager().add(package, ref id)) {
                    json["state"] = 1;
                    json["message"] = "ok";
                }
            }
            catch (Exception e)
            {
                json["state"] = 0;
                json["message"] = "repark exception:" + e.ToString();
            }

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