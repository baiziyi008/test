using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["repark_account"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }
            string account = Session["repark_account"].ToString();
            table_repark_user user = Controller.GetManager().get_user(account);
            showName = user.nickname;
            if (user.level == "0") {
                showName += " (超级管理员)    ";
            }
        }
        public string showName = string.Empty;
    }
}