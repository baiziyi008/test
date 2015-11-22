using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class channel_manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string load_list()
        {
            string table_str = string.Empty;
            List<table_repark_channel> objs = Controller.GetManager().get_channel_list();
            if (objs.Count > 0)
            {
                for (int i = 0; i < objs.Count; i++)
                {
                    table_str +=
                    "<tr style=\"color:#333333; text-align:center;\"><td style=\"height:40px;\">" + objs[i].id.ToString()
                    + "</td><td>" + objs[i].channel_name
                    + "</td><td>" + objs[i].channel_sign
                    + "</td><td><a href='javascript:on_delete(" + objs[i].id.ToString() + ")'>删除</a></td></tr>";
                }
            }
            return table_str;
        }
    }
}