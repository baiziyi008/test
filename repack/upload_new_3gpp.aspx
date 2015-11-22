<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_new_3gpp.aspx.cs" Inherits="repack.upload_new_3gpp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 84px;
            text-align: right;
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
        .auto-style9 {
            width: 84px;
            height: 102px;
        }
        .auto-style10 {
            height: 102px;
        }
        .auto-style15 {
            width: 100%;
        }
        .auto-style16 {
            width: 84px;
            height: 23px;
        }
        .auto-style17 {
            height: 23px;
        }
        .auto-style18 {
            width: 263px;
        }
        .auto-style19 {
            height: 102px;
            width: 263px;
        }
        .auto-style20 {
            height: 23px;
            width: 263px;
        }
        .auto-style21 {
            width: 123px;
        }
        .auto-style22 {
            height: 102px;
            width: 123px;
        }
        .auto-style23 {
            height: 23px;
            width: 123px;
        }
        .auto-style24 {
            width: 84px;
            text-align: right;
            height: 44px;
        }
        .auto-style25 {
            height: 44px;
            width: 263px;
        }
        .auto-style26 {
            height: 44px;
            width: 123px;
        }
        .auto-style27 {
            height: 44px;
        }
        </style>
</head>
<body style="margin:0px auto; width:986px; background-color:#eeeeee;">
    <form id="form1" runat="server">
    <div style="color:#666666; font-size:13px; font-family:'微软雅黑';" class="auto-style15">
        
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style21" style="text-align: right">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style24"></td>
                <td class="auto-style25"  style="font-size:14px; font-weight:bold; color:#333333;">
                    申请新的3GPP配置信息</td>
                <td class="auto-style26" style="text-align: right">
                    </td>
                <td class="auto-style27"  style="font-size:14px; font-weight:bold; color:#333333;">
                    添加已经申请好的3GPP配置信息</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style18">
                    &nbsp;</td>
                <td class="auto-style21" style="text-align: right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">名称：</td>
                <td class="auto-style18">
                    <input id="new_name" name="new_name" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    名称：</td>
                <td>
                    <input id="old_name" name="old_name" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">包名：</td>
                <td class="auto-style18">
                    <input id="new_packagename" name="new_packagename" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    包名：</td>
                <td>
                    <input id="old_packagename" name="old_packagename" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">产品名：</td>
                <td class="auto-style18">
                    <input id="new_productname" name="new_productname" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    AppKey：</td>
                <td>
                    <input id="old_appkey" name="old_appkey" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">PID：</td>
                <td class="auto-style18">
                    <input id="new_pid" name="new_pid" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    产品名：</td>
                <td>
                    <input id="old_productname" name="old_productname" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">渠道名：</td>
                <td class="auto-style18">
                    <input id="new_channel_name" name="new_channel_name" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    PID：</td>
                <td>
                    <input id="old_pid" name="old_pid" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">CID：</td>
                <td class="auto-style18">
                    <input id="new_cid" name="new_cid" type="text" style="width:250px;" /></td>
                <td class="auto-style21" style="text-align: right">
                    渠道名：</td>
                <td>
                    <input id="old_channel_name" name="old_channel_name" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style18">
                    &nbsp;</td>
                <td class="auto-style21" style="text-align: right">
                    CID：</td>
                <td>
                    <input id="old_cid" name="old_cid" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style18">
                    &nbsp;</td>
                <td class="auto-style21" style="text-align: right">
                    3GPP-PID：</td>
                <td>
                    <input id="old_3gpp_pid" name="old_3gpp_pid" type="text" style="width:250px;" value="0" /></td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style18">
                    &nbsp;</td>
                <td class="auto-style21" style="text-align: right">
                    3GPP-CID：</td>
                <td>
                    <input id="old_3gpp_cid" name="old_3gpp_cid" type="text" style="width:250px;" value="0" /></td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style19">
                    <input class="button" id="Button2" type="button" value="添加" onclick="insert_data(0);" /></td>
                <td class="auto-style22" style="text-align: right">
                    &nbsp;</td>
                <td class="auto-style10">
                    <input class="button" id="Button3" type="button" value="添加" onclick="insert_data(1);" /></td>
            </tr>
            <tr>
                <td class="auto-style16"></td>
                <td class="auto-style20">
                    </td>
                <td class="auto-style23" style="text-align: right">
                    </td>
                <td class="auto-style17">
                    </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function insert_data(n) {
        // FormData 对象
        var form = new FormData();
        switch (n) {
            case 0:
                form.append("type", "new_3gpp_new_form");
                form.append("new_name", document.getElementById("new_name").value);
                form.append("new_packagename", document.getElementById("new_packagename").value);
                form.append("new_productname", document.getElementById("new_productname").value);
                form.append("new_pid", document.getElementById("new_pid").value);
                form.append("new_channel_name", document.getElementById("new_channel_name").value);
                form.append("new_cid", document.getElementById("new_cid").value);
                break;
            case 1:
                form.append("type", "new_3gpp_old_form");
                form.append("old_name", document.getElementById("old_name").value);
                form.append("old_packagename", document.getElementById("old_packagename").value);
                form.append("old_appkey", document.getElementById("old_appkey").value);
                form.append("old_productname", document.getElementById("old_productname").value);
                form.append("old_pid", document.getElementById("old_pid").value);
                form.append("old_channel_name", document.getElementById("old_channel_name").value);
                form.append("old_cid", document.getElementById("old_cid").value);
                form.append("old_3gpp_pid", document.getElementById("old_3gpp_pid").value);
                form.append("old_3gpp_cid", document.getElementById("old_3gpp_cid").value);
                break;
            default:
                break;
        }
        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "insert_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "key_3gpp_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
