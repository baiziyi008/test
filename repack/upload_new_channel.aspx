<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_new_channel.aspx.cs" Inherits="repack.upload_new_channel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 270px;
            text-align:right;
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
            width: 270px;
            height: 102px;
        }
        .auto-style10 {
            height: 102px;
        }
        .auto-style15 {
            width: 100%;
        }
        .auto-style16 {
            width: 270px;
            height: 23px;
        }
        .auto-style17 {
            height: 23px;
        }
        .auto-style18 {
            height: 46px;
        }
        </style>
</head>
<body style="margin:0px auto; width:986px; background-color:#eeeeee;">
    <form id="form1" runat="server">
    <div style="color:#666666; font-size:13px; font-family:'微软雅黑';" class="auto-style15">
        
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style18"></td>
                <td style="font-size:14px; font-weight:bold; color:#333333;" class="auto-style18">
                    添加渠道信息</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">渠道名：</td>
                <td>
                    <input id="channel_name" name="channel_name" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">渠道号：</td>
                <td>
                    <input id="channel_sign" name="channel_sign" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style10">
                    <input class="button" id="Button2" type="button" value="提交" onclick="insert_data();" /></td>
            </tr>
            <tr>
                <td class="auto-style16"></td>
                <td class="auto-style17">
                    </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function insert_data() {
        // FormData 对象
        var form = new FormData();
        form.append("type", "new_channel_form");
        form.append("channel_name", document.getElementById("channel_name").value);
        form.append("channel_sign", document.getElementById("channel_sign").value);
        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "insert_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "channel_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
