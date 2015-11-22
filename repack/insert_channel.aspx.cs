using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class insert_channel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string timestamp = repack_shell.utils.get_time_stamp();
            for (int i = 2; i < 150; i++)
            {
                repack_shell.table_repark_channel obj = new repack_shell.table_repark_channel();
                obj.channel_name = "破解游戏_" + string.Format("{0:D3}", i);
                obj.channel_sign = "hj_" + string.Format("{0:D3}", i);
                obj.ctime = timestamp;
                string id = string.Empty;
                if (repack_shell.Controller.GetManager().add(obj, ref id))
                {
                    
                }
            }
        }
    }
}