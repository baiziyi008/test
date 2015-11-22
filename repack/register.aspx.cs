using repack_shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["repark_account"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            table_repark_user user = Controller.GetManager().get_user(Session["repark_account"].ToString());
            if (user.level != "0")
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (IsPostBack)
            {
                do
                {
                    string account = Request["account"].ToString();
                    string pwd1 = Request["pwd1"].ToString();
                    string pwd2 = Request["pwd2"].ToString();
                    string nickname = Request["nickname"].ToString();
                    string email = Request["email"].ToString();
                    if (pwd1 != pwd2)
                    {
                        Response.Write("两次密码输入不一致！请重试！");
                        break;
                    }
                    repack_shell.table_repark_user userinfo = new repack_shell.table_repark_user();
                    userinfo.account = account;
                    userinfo.password = repack_shell.utils.CreateMD5Hash(pwd1).ToLower();
                    userinfo.nickname = nickname;
                    userinfo.email = email;
                    userinfo.ctime = repack_shell.utils.get_time_stamp();
                    string uid = string.Empty;
                    if (repack_shell.Controller.GetManager().add(userinfo, ref uid))
                    {
                        Response.Write("<a href=\"login.aspx\" target=\"_self\">注册成功，点击跳转到登录页面</a>");
                    }
                    else
                    {
                        Response.Write("注册失败！请重试！");
                    }
                } while (false);
            }
        }
    }
}