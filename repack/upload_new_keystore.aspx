<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_new_keystore.aspx.cs" Inherits="repack.upload_new_keystore" %>

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
            width: 270px;
            height: 107px;
        }
        .auto-style19 {
            height: 107px;
        }
        .auto-style20 {
            width: 254px;
        }
        .auto-style21 {
            width: 270px;
            height: 33px;
        }
        .auto-style22 {
            height: 33px;
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
                <td>&nbsp;</td>
            </tr>
                <tr>
                <td class="auto-style21"></td>
                <td style="font-size:14px; font-weight:bold; color:#333333;" class="auto-style22">
                    添加签名信息</td>
            </tr>
            <tr>
                <td class="auto-style18" style="text-align:right;">签名路径：</td>
                <td class="auto-style19">
                    <input id="path" name="path" class="auto-style20" type="file" accept=".keystore" /></td>
            </tr>
            <tr>
                <td class="auto-style1">名称：</td>
                <td>
                    <input id="keystore_title" name="sdk_title" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">密码：</td>
                <td>
                    <input id="keystore_pwd" name="keystore_pwd" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">Key别名：</td>
                <td>
                    <input id="keystore_key_alias" name="keystore_key_alias" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">Key密码：</td>
                <td>
                    <input id="keystore_key_pwd" name="keystore_key_pwd" type="text" style="width:250px;" /> </td>
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
        var fileObj = document.getElementById("path").files[0]; // js 获取文件对象
        // FormData 对象
        var form = new FormData();
        form.append("type", "new_keystore_form");
        form.append("keystore_title", document.getElementById("keystore_title").value);
        form.append("keystore_pwd", document.getElementById("keystore_pwd").value);
        form.append("keystore_key_alias", document.getElementById("keystore_key_alias").value);
        form.append("keystore_key_pwd", document.getElementById("keystore_key_pwd").value);
        form.append("file", fileObj);
        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "insert_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "keystore_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
