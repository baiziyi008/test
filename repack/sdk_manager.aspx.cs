using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class sdk_manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string load_sdk_list() {
            string table_str = string.Empty;
            List<table_repark_sdk> sdks = Controller.GetManager().get_sdk_list();
            if (sdks.Count > 0)
            {
                for (int i = 0; i < sdks.Count; i++)
                {
                    table_str +=
                    "<tr style=\"color:#333333; text-align:center;\"><td style=\"height:40px;\">" + sdks[i].id.ToString()
                    + "</td><td>" + sdks[i].sdk_title
                    + "</td><td>" + sdks[i].sdk_content
                    + "</td><td>" + sdks[i].sdk_path
                    + "</td><td>" + sdks[i].sdk_codeclass
                    + "</td><td>" + sdks[i].sdk_versioncode
                    + "</td><td><a href='javascript:on_delete(" + sdks[i].id.ToString() + ")'>删除</a></td></tr>";
                }
            }
            return table_str;
        }
    }
}