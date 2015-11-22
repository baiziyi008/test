using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class online_package_manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dt_end = dt_end.AddDays(1);
            dt_end = dt_end.AddSeconds(-1);
            if (IsPostBack) {
                string str_dt_start = Request["search_start"].ToString();
                string str_dt_end = Request["search_end"].ToString();
                dt_start = DateTime.Parse(str_dt_start);
                dt_end = DateTime.Parse(str_dt_end);
                dt_end = dt_end.AddDays(1);
                dt_end = dt_end.AddSeconds(-1);
            }
        }
        public DateTime dt_start = DateTime.Parse(DateTime.Now.ToShortDateString());
        public DateTime dt_end = DateTime.Parse(DateTime.Now.ToShortDateString());

        public string load_list()
        {
            string table_str = string.Empty;
            List<table_repark_package_published> apks = Controller.GetManager().get_publish_list(utils.get_time_stamp(dt_start),utils.get_time_stamp(dt_end));
            if (apks.Count > 0)
            {
                for (int i = 0; i < apks.Count; i++)
                {
                    table_repark_package_original_version version = Controller.GetManager().get_original_version(int.Parse(apks[i].original_id));
                    string label_name = apks[i].app_name;
                    if (label_name == "") {
                        if (version != null)
                        {
                            label_name = version.pkg_label;
                        }
                        else {
                            label_name = "";
                        }
                    }
                    List<string> channel_ids = apks[i].channel_id.Split(',').ToList();
                    List<string> path_list = apks[i].path.Split(',').ToList();
                    List<table_repark_channel> channel_objs = new List<table_repark_channel>();
                    foreach (string cid in channel_ids) {
                        channel_objs.Add(Controller.GetManager().get_channel(int.Parse(cid)));
                    }
                    int channel_rows = channel_ids.Count / 3 + ((channel_ids.Count % 3) > 0 ? 1 : 0);
                    int height = 150;
                    height += (channel_rows - 1) * 20;
                    table_str += "<tr><td><div style=\"width: 984px; height:" + height.ToString() + "px; border: 1px solid #dddddd; background-color:#ffffff; position:relative; margin-bottom:10px;\" onmouseover=\"style.background='#EEEEEE'\" onmouseout=\"style.background='#ffffff'\">";
                    table_str += "<div style=\"float:left; position: absolute; top: 10px; left: 10px; width: 80px; height: 80px; \">";
                    table_str += "<img style=\"width: 80px; height: 80px; border: 0px; \" src=\"" + apks[i].app_icon + "\" />";
                    table_str += "</div>";
                    table_str += "<div style=\"float:left; position: absolute; top: 10px; left: 100px; width: 250px; height: 20px; \">" + label_name + "</div>";
                    table_str += "<div style=\"float:left; position: absolute; top: 35px; left: 100px; width: 250px; height: 20px; \">" + apks[i].app_package_name + "</div>";
                    table_str += "<div style=\"float:left; position: absolute; top: 60px; left: 100px; width: 250px; height: 20px; \">" + apks[i].app_version_string + " (" + apks[i].app_version_code.ToString() + ")" + "</div>";
                    table_str += "<div style=\"float:left; position: absolute; top: 10px; left: 350px; width: 590px; height: 80px; \">" + apks[i].content.Replace("\r\n","<br/>") + "</div>";
                    table_str += "<div style=\"float:left; position: absolute; top: 10px; left: 940px; width: 30px; height: 30px; \"><a href=\"javascript:on_delete(" + apks[i].id.ToString() + ");\">删除</a></div>";

                    table_str += "<div style=\"float:left; position: absolute; top: 100px; left: 20px; width: 950px; \">";
                    for (int j = 0; j < channel_objs.Count; j++) {
                        if (channel_objs[j] != null)
                        {
                            table_str += "<div style=\"float:left; margin-top:10px; width: 300px; height: 20px; \"><a href=\"" + path_list[j] + "\">" + channel_objs[j].channel_name + "(" + channel_objs[j].channel_sign + ")" + "</a></div>";
                        }
                        else {
                            table_str += "<div style=\"float:left; margin-top:10px; width: 300px; height: 20px; \"><a href=\"" + path_list[j] + "\">(渠道已删除)</a></div>";
                        }
                    }
                    table_str += "</div>";

                    table_str += "</div></td></tr>";
                }
            }
            return table_str;
        }
    }
}