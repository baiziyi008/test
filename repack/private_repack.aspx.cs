using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class private_repack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) {
                HttpPostedFile apk = Request.Files["apk"];

                string displayname = Request.Form["displayname"].ToString();
                string versionstring = Request.Form["versionstring"].ToString();
                string versioncode = Request.Form["versioncode"].ToString();

                string umeng_appkey = Request.Form["umeng_appkey"].ToString();
                string umeng_channel = Request.Form["umeng_channel"].ToString();
                string umeng_messageid = Request.Form["umeng_push_messageid"].ToString();

                string gdt_appkey = Request.Form["gdt_appkey"].ToString();
                string gdt_appid = Request.Form["gdt_appid"].ToString();
                string gdt_adid = Request.Form["gdt_adid"].ToString();

                string g3pp_appname = Request.Form["g3pp_appname"].ToString();
                string g3pp_channelname = Request.Form["g3pp_channelname"].ToString();
                string g3pp_pid = Request.Form["g3pp_pid"].ToString();
                string g3pp_cid = Request.Form["g3pp_cid"].ToString();

                string umeng_type = Request.Form["umeng_select"].ToString();

                string time_str = DateTime.Now.ToString().Replace("-", "").Replace(":", "").Replace("/", "").Replace(" ", "");
                string work_apk = data.DecompilerProject + @"\" + time_str + @".apk";
                string work_icon = data.DecompilerProject + @"\" + time_str + @".png";
                string signed_apk = data.PublishApk + @"\" + time_str + @".apk";

                apk.SaveAs(work_apk);

                repack_shell.repark_input_model rim = new repack_shell.repark_input_model();
                rim.displayname = displayname;
                rim.versioncode = versioncode;
                rim.versionstring = versionstring;
                rim.umeng_appkey = umeng_appkey;
                rim.umeng_channel = umeng_channel;
                rim.umeng_messageid = umeng_messageid;
                rim.gdt_appkey = gdt_appkey;
                rim.gdt_appid = gdt_appid;
                rim.gdt_adid = gdt_adid;
                rim.g3pp_appname = g3pp_appname;
                rim.g3pp_channelname = g3pp_channelname;
                rim.g3pp_cid = g3pp_cid;
                rim.g3pp_pid = g3pp_pid;
                if (display_check.Checked)
                    rim.use_displayname = true;
                if (version_check.Checked)
                    rim.use_version = true;
                if (umeng_check.Checked)
                {
                    rim.use_umeng = true;
                    if (umeng_type == "1")
                        rim.umeng_type = repack_shell.UmengType.Game;
                }
                if (umeng_push_check.Checked) {
                    rim.use_umeng = true;
                    rim.use_umeng_push = true;
                }
                if (gdt_check.Checked)
                    rim.use_gdt = true;
                if (hao123_check.Checked)
                    rim.use_hao123 = true;
                if (g3pp_check.Checked)
                    rim.use_3gpp = true;
                if (txsg_check.Checked)
                    rim.use_txsg = true;

                repack_shell.ShellPublic shell = repack_shell.shell_router.smart_run_shell();
                repack_tools.tool_keystore keystore = data.GetPublicKeyStore();
                if (rim.use_txsg)
                    keystore = data.GetGuijunKeyStore();
                shell.Init(work_apk, signed_apk, keystore);
                shell.SetRepackInputModel(rim);
                shell.RunTask();

                Response.Write("<a href=\""+ shell.GetApkInfo().ZipalignApkPath.Replace(AppDomain.CurrentDomain.BaseDirectory,"") + "\">点击保存打好包的Apk</a>");
            }
        }
    }
}