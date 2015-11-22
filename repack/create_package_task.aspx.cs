using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class create_package_task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 载入原包信息
        /// </summary>
        /// <returns></returns>
        public string load_original_package_info() {
            string table_str = string.Empty;
            List<table_repark_package_original> original_apks = Controller.GetManager().get_original_list();
            if (original_apks.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < original_apks.Count; i++)
                {
                    List<table_repark_package_original_version> versions = Controller.GetManager().get_original_version_list_from_original(original_apks[i].id);
                    if (versions.Count > 0)
                    {
                        option_text = versions[0].title + " - " + original_apks[i].pkg_packagename + " - " + versions[0].pkg_versionstring;
                        table_str += "<option value=\"" + versions[0].id.ToString() + "\">" + option_text + "</option>";
                    }
                }
            }
            return table_str;
        }

        /// <summary>
        /// 加载签名列表
        /// </summary>
        /// <returns></returns>
        public string load_keysore_list()
        {
            string table_str = string.Empty;
            List<table_repark_keystore> objs = Controller.GetManager().get_keystore_list();
            if (objs.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < objs.Count; i++)
                {
                    option_text = objs[i].title + " [" + objs[i].alias + "]";
                    table_str += "<option value=\"" + objs[i].id.ToString() + "\">" + option_text + "</option>";
                }
            }
            return table_str;
        }

        /// <summary>
        /// 加载友盟KEY
        /// </summary>
        /// <returns></returns>
        public string load_umeng_list()
        {
            string table_str = string.Empty;
            List<table_repark_umeng> objs = Controller.GetManager().get_umeng_key_list();
            if (objs.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < objs.Count; i++)
                {
                    option_text = objs[i].title + " [" + objs[i].umeng_key + "]";
                    table_str += "<option value=\"" + objs[i].id.ToString() + "\">" + option_text + "</option>";
                }
            }
            return table_str;
        }

        /// <summary>
        /// 加载广点通
        /// </summary>
        /// <returns></returns>
        public string load_gdt_list()
        {
            string table_str = string.Empty;
            List<table_reaprk_gdt> objs = Controller.GetManager().get_gdt_list();
            if (objs.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < objs.Count; i++)
                {
                    option_text = objs[i].title + " [" + objs[i].gdt_packagename + "] [" + objs[i].gdt_appid + "]";
                    table_str += "<option value=\"" + objs[i].id.ToString() + "\">" + option_text + "</option>";
                }
            }
            return table_str;
        }

        /// <summary>
        /// 加载3gpp
        /// </summary>
        /// <returns></returns>
        public string load_3gpp_list()
        {
            string table_str = string.Empty;
            List<table_repark_3gpp> objs = Controller.GetManager().get_3gpp_list();
            if (objs.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < objs.Count; i++)
                {
                    option_text = objs[i].title + " [" + objs[i].packagename + "] [" + objs[i].local_pid + "] [" + objs[i].local_cid + "]";
                    table_str += "<option value=\"" + objs[i].id.ToString() + "\">" + option_text + "</option>";
                }
            }
            return table_str;
        }

        /// <summary>
        /// 加载百度SSP
        /// </summary>
        /// <returns></returns>
        public string load_baidussp_list()
        {
            string table_str = string.Empty;
            List<table_repark_baidussp> objs = Controller.GetManager().get_baidussp_list();
            if (objs.Count > 0)
            {
                string option_text = string.Empty;
                for (int i = 0; i < objs.Count; i++)
                {
                    option_text = objs[i].title + " [" + objs[i].baidussp_appid + "]";
                    table_str += "<option value=\"" + objs[i].id.ToString() + "\">" + option_text + "</option>";
                }
            }
            return table_str;
        }



        /// <summary>
        /// 加载渠道
        /// </summary>
        /// <returns></returns>
        public string load_channel_list()
        {
            string table_str = string.Empty;
            List<table_repark_channel> objs = Controller.GetManager().get_channel_list();
            objs.Reverse();
            if (objs.Count > 0)
            {
                for (int i = 0; i < objs.Count; i++)
                {
                    table_str += "<div style=\"width:300px; height:50px; float:left; border:1px #999999 solid; margin-right:5px; margin-bottom:5px; background-color:#DDDDDD; cursor:pointer; position:relative;\" onclick=\"channel_click(" + i.ToString() + ");\">"
                        + "<div style=\"width:300px; height:20px; padding-top:5px;\">"
                        + "<input id=\"channel_id_" + i.ToString() + "\" name=\"channel_id_" + i.ToString() + "\" value=" + objs[i].id.ToString() + " type=\"checkbox\" style=\"display:none;\" />"
                        + "<span style=\"padding-left:10px;\">" + objs[i].channel_name + "</span></div>"
                        + "<div style=\"width:300px; height:20px;\"><span style=\"padding-left:10px;\">" + objs[i].channel_sign + "</span></div>"
                        + "<div style=\"top:5px; right:5px; position:absolute;\"><img id=\"image_channel_id_" + i.ToString() + "\" src=\"res/image/sure.png\" style=\"display:none;\"/></div>"
                        + "</div>";
                }
                table_str += "<input id=\"app_channle_count\" value=\"" + objs.Count.ToString() + "\" type=\"hidden\" />";
            }
            return table_str;
        }

    }
}