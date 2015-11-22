using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class original_package_manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string load_original_packages()
        {
            string table_str = string.Empty;
            List<table_repark_package_original> original_apks = Controller.GetManager().get_original_list();
            if (original_apks.Count > 0)
            {
                for (int i = 0; i < original_apks.Count; i++)
                {
                    List<table_repark_package_original_version> versions = Controller.GetManager().get_original_version_list_from_original(original_apks[i].id);
                    if (versions.Count > 0)
                    {
                        table_str +=
                        "<tr style=\"color:#333333; text-align:center;\"><td style=\"height:80px;\">" + original_apks[i].id.ToString()
                        + "</td><td><img width='60' height='60' src='" + versions[0].pkg_icon_path + "' />"
                        + "</td><td>" + versions[0].pkg_label
                        + "</td><td>" + original_apks[i].pkg_packagename
                        + "</td><td>" + "数字:" + versions[0].pkg_versioncode + " 字符:" + versions[0].pkg_versionstring
                        + "</td><td><a href=\""+ versions[0].pkg_path + "\">下载</a> | <a href='javascript:on_delete_version(" + versions[0].id.ToString() + ")'>删除</a></td></tr>";
                    }
                }
            }
            return table_str;
        }
    }
}