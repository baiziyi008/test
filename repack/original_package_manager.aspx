<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="original_package_manager.aspx.cs" Inherits="repack.original_package_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .menu-button {
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
<body style="margin:0px auto; width:986px;">
    <div style="width:100%; height:40px;">
        <div><input class="menu-button" type="button" value="上传原包" onclick="document.location.href = 'upload_new_original.aspx';" /></div>
    </div>
    <div style="width:100%; margin-top:10px;">
        <table style="width:100%; font-size:13px; font-family:'微软雅黑'; color:#666666; border:1px solid #666666;" border="0" cellspacing="0" cellpadding="0">
            <tr style="background-color:#145eb0; color:#eeeeee; text-align:center;">
                <td style="width:60px; height:40px;"> ID</td>
                <td style="width:80px;">ICON</td>
                <td style="width:150px;">应用名</td>
                <td style="width:200px;">包名</td>
                <td style="width:150px;">最后版本</td>
                <td style="width:144px;">操作</td>
            </tr>
            <% = load_original_packages() %>
        </table>
    </div>
</body>
</html>
<script type="text/javascript">
    function on_delete_version(n) {
        // FormData 对象
        var form = new FormData();
        form.append("type", "delete_original_package_form");
        form.append("id", n + "");

        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "delete_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("删除成功！");
                    document.location.href = "original_package_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
