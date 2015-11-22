<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_new_sdk.aspx.cs" Inherits="repack.upload_new_sdk" %>

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
            width: 255px;
            height:30px;
        }
        .auto-style21 {
            width: 270px;
            height: 38px;
        }
        .auto-style22 {
            height: 38px;
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
                    添加SDK</td>
            </tr>
            <tr>
                <td class="auto-style18" style="text-align:right;">SDK路径：</td>
                <td class="auto-style19">
                    <select id="sdk_path" name="sdk_path" class="auto-style20">
                        <option selected="selected" value="0">---请选择SDK路径---</option>
                        <%=load_sdk_paths() %>
                    </select>

                    <br />

                    (需要手工添加SDK到/data/sdk目录，并写好对应的处理脚本)
                </td>
            </tr>
            <tr>
                <td class="auto-style1">SDK名称：</td>
                <td>
                    <input id="sdk_title" name="sdk_title" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">SDK说明：</td>
                <td>
                    <input id="sdk_content" name="sdk_content" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">SDK版本（数字）：</td>
                <td>
                    <input id="sdk_version" name="sdk_version" type="text" style="width:250px;" /></td>
            </tr>
            <tr>
                <td class="auto-style1">SDK处理类名：</td>
                <td>
                    <input id="sdk_codeclass" name="sdk_codeclass" type="text" style="width:250px;" /> <span style="color:red;">*</span></td>
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
        if (document.getElementById("sdk_path").value == 0) {
            alert("请选择SDK路径！");
            return;
        }
        // FormData 对象
        var form = new FormData();
        form.append("type", "new_sdk_form");
        form.append("sdk_path", document.getElementById("sdk_path").value);
        form.append("sdk_title", document.getElementById("sdk_title").value);
        form.append("sdk_content", document.getElementById("sdk_content").value);
        form.append("sdk_version", document.getElementById("sdk_version").value);
        form.append("sdk_codeclass", document.getElementById("sdk_codeclass").value);
        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "insert_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "sdk_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
