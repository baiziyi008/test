<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="repack.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册页面</title>
    <style type="text/css">
        #Text1 {
            width: 205px;
        }
        #Password1 {
            width: 207px;
        }
        #Password2 {
            width: 172px;
        }
        #Text2 {
            width: 203px;
        }
        #Submit1 {
            width: 103px;
            height: 34px;
        }
        #account {
            width: 250px;
        }
        #pwd1 {
            width: 250px;
        }
        #pwd2 {
            width: 250px;
        }
        #nickname {
            width: 250px;
        }
        #email {
            width: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        用户注册：<br />
        <br />
        用户名：<br />
        <input id="account" name="account" type="text" /><br />
        <br />
        密码：<br />
        <input id="pwd1" name="pwd1" type="password" /><br />
        <br />
        确认密码：<br />
        <input id="pwd2" name="pwd2" type="password" /><br />
        <br />
        称呼：<br />
        <input id="nickname" name="nickname" type="text" /><br />
        <br />
        邮箱：<br />
        <input id="email" name="email" type="text" /><br />
        <br />
        <input id="submit" name="submit" type="submit" value="注册" /></div>
    </form>
</body>
</html>
