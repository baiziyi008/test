<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="repack.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户登录</title>
    <style type="text/css">
        #Submit1 {
            height: 27px;
            width: 91px;
        }
        #Text1 {
            width: 250px;
            height: 24px;
        }
        #Password1 {
            height: 22px;
            width: 249px;
        }

        .main-page {
        margin-top:100px;
        margin-left:auto;
        margin-right:auto;
        width:800px;
        height:600px;
        border:1px solid #dddddd;
        background-color:#eeeeee;
        font-family:'微软雅黑';
        }
        .auto-style1 {
            width: 60px;
        }
        .auto-style2 {
            width: 638px;
        }
        .auto-style3 {
            width: 60px;
            height: 80px;
        }
        .auto-style4 {
            width: 638px;
            height: 80px;
        }
        .auto-style5 {
            height: 80px;
        }
        .auto-style6 {
            height: 24px;
        }
        .auto-style9 {
            width: 500px;
            height: 36px;
        }
        .button {
            width:100px;
            height:40px;
            font-size:14px;
            background-color:#145eb0;
            color:#eeeeee;
            text-align:center;
            border:1px solid #eeeeee;
            cursor:pointer;
        }
        </style>
</head>
<body style="margin:0px auto;">
    <form method="post" runat="server">
    <div class="main-page">
        <table style="width:100%; height:100%;">
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style4" style="vertical-align:bottom; font-size:24px; font-weight:bold;">打包平台-用户登录</td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style6" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2" style="vertical-align: bottom">用户名</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <input id="account" name="account" type="text" class="auto-style9" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2" style="vertical-align: bottom">密码</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <input id="pwd" name="pwd" type="password" class="auto-style9" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <input class="button" id="btn_submit" type="submit" value="登录" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">
                    <span id="error_tips"><%=error_tips %></span></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

