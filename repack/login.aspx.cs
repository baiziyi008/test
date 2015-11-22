using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class login : System.Web.UI.Page
    {
        public string error_tips = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                do
                {
                    string account = Request["account"].ToString();
                    string pwd = Request["pwd"].ToString();
                    repack_shell.table_repark_user userinfo = new repack_shell.table_repark_user();
                    if (repack_shell.Controller.GetManager().login(account, pwd,ref userinfo))
                    {
                        Session["repark_uid"] = userinfo.id.ToString();
                        Session["repark_account"] = userinfo.account;
                        Session["repark_nickname"] = userinfo.nickname;
                        Response.Redirect("home.aspx");
                    }
                    else {
                        error_tips = "登录失败";
                    }
                } while (false);
            }
        }
    }
}