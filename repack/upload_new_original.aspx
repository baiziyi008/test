<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload_new_original.aspx.cs" Inherits="repack.upload_new_original" %>

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
        .auto-style2 {
            width: 270px;
            height: 39px;
        }
        .auto-style3 {
            height: 39px;
        }
        .auto-style4 {
            width: 301px;
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
        .auto-style5 {
            width: 270px;
            height: 52px;
        }
        .auto-style6 {
            height: 52px;
        }
        .auto-style9 {
            width: 270px;
            height: 102px;
        }
        .auto-style10 {
            height: 102px;
        }
        .auto-style11 {
            width: 270px;
            height: 70px;
            text-align:right;
        }
        .auto-style12 {
            height: 70px;
        }
        .auto-style13 {
            width: 270px;
            height: 17px;
        }
        .auto-style14 {
            height: 17px;
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
                <td class="auto-style18"></td>
                <td style="font-size:14px; font-weight:bold; color:#333333;" class="auto-style19">
                    添加原包</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" style="text-align:right;">原包路径：</td>
                <td class="auto-style3">
                    <input id="apkfile" name="apkfile" class="auto-style4" type="file" accept=".apk" /></td>
            </tr>
            <tr>
                <td class="auto-style5"></td>
                <td class="auto-style6">
                    <input class="button" id="btn_load" name="btn_load" type="button" value="载入" onclick="UpladFile();" /></td>
            </tr>
            <tr>
                <td class="auto-style13"></td>
                <td class="auto-style14">
                    <img style="display:none;" id="load_img" alt="正在加载..." src="res/image/load.gif" /><br />
                    <span style="display:block;" id="load_text"></span>
                </td>
            </tr>
            <tr>
                <td class="auto-style11">Icon：</td>
                <td class="auto-style12">
                    <img id="apk_icon" width="60" height="60" alt="" src="res/image/failed.png" /></td>
            </tr>
            <tr>
                <td class="auto-style1">包名：</td>
                <td>
                    <input id="apk_packagename" name="apk_packagename" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">应用名：</td>
                <td>
                    <input id="apk_appname" name="apk_appname" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">版本（数字）：</td>
                <td>
                    <input id="apk_versioncode" name="apk_versioncode" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">版本（字符串）：</td>
                <td>
                    <input id="apk_versionstring" name="apk_versionstring" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">主Activity：</td>
                <td>
                    <input id="apk_activity" name="apk_activity" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">Application：</td>
                <td>
                    <input id="apk_application" name="apk_application" type="text" style="width:250px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td class="auto-style1">说明：</td>
                <td>
                    <input id="apk_title" name="apk_title" type="text" style="width:250px;" /></td>
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
    var jsonobject = null;
    function UpladFile() {
        document.getElementById("btn_load").disabled = true;
        document.getElementById("btn_load").style.cursor = "default";
        document.getElementById("btn_load").style.backgroundColor = "#666666";
        document.getElementById("load_img").style.display = "block";
        document.getElementById("apkfile").disabled = true;
        var fileObj = document.getElementById("apkfile").files[0]; // js 获取文件对象
        // FormData 对象
        var form = new FormData();
        form.append("type", "new_original_package");            // 可以增加表单数据
        form.append("file", fileObj);                           // 文件对象
        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "upload_file.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    jsonobject = result;
                    document.getElementById("load_text").innerText = "上传路径：" + jsonobject.apkinfo.filepath;
                    document.getElementById("load_img").style.display = "none";
                    document.getElementById("apk_icon").src = jsonobject.apkinfo.icon;
                    document.getElementById("apk_packagename").value = jsonobject.apkinfo.packagename;
                    document.getElementById("apk_appname").value = jsonobject.apkinfo.appname;
                    document.getElementById("apk_versioncode").value = jsonobject.apkinfo.versioncode;
                    document.getElementById("apk_versionstring").value = jsonobject.apkinfo.versionstring;
                    document.getElementById("apk_activity").value = jsonobject.apkinfo.activity;
                    document.getElementById("apk_application").value = jsonobject.apkinfo.application;
                    document.getElementById("apk_title").value = jsonobject.apkinfo.appname;
                    document.getElementById("apk_title").focus();
                }
            }
        };
        xhr.send(form);
    }

    function insert_data() {
        // FormData 对象
        var form = new FormData();
        form.append("type", "new_original_package_form");
        form.append("apk_path", jsonobject.apkinfo.filepath);
        form.append("apk_icon", jsonobject.apkinfo.icon);
        form.append("apk_packagename", jsonobject.apkinfo.packagename);
        form.append("apk_appname", jsonobject.apkinfo.appname);
        form.append("apk_versioncode", jsonobject.apkinfo.versioncode);
        form.append("apk_versionstring", jsonobject.apkinfo.versionstring);
        form.append("apk_activity", jsonobject.apkinfo.activity);
        form.append("apk_application", jsonobject.apkinfo.application);
        form.append("apk_title", document.getElementById("apk_title").value);

        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "insert_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "original_package_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
