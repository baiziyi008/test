<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="create_package_task.aspx.cs" Inherits="repack.create_package_task" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
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
        .auto-style1 {
            width: 160px;
        }
        .auto-style5 {
            width: 160px;
            height: 36px;
        }
        .auto-style6 {
            height: 36px;
        }
        .auto-style9 {
            width: 400px;
            height: 24px;
        }
        .auto-style10 {
            width: 160px;
            height: 45px;
        }
        .auto-style11 {
            height: 45px;
        }
        .auto-style12 {
            width: 160px;
            height: 19px;
        }
        .auto-style13 {
            height: 19px;
        }
        .auto-style14 {
            width: 160px;
            height: 30px;
        }
        .auto-style15 {
            height: 30px;
        }
        .auto-style16 {
            width: 160px;
            height: 80px;
        }
        .auto-style17 {
            height: 80px;
        }
        .auto-style20 {
            width: 160px;
            height: 24px;
        }
        .auto-style22 {
            height: 39px;
        }
        .auto-style23 {
            width: 160px;
            height: 27px;
        }
        .auto-style24 {
            height: 27px;
        }
        .auto-style25 {
            height: 26px;
        }
        .auto-style26 {
            height: 45px;
            width: 626px;
        }
        .auto-style27 {
            width: 160px;
            height: 8px;
        }
        .auto-style28 {
            height: 8px;
        }
        </style>
</head>
<body style="margin:0px auto; width:986px; background-color:#eeeeee;">
    <form id="form1" runat="server">
    <div style="color:#555555; font-size:13px; font-family:'微软雅黑';">
        
        <table style="width:100%;">
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right"></td>
                <td style="font-size:20px; font-weight:bold; color:#333333;" colspan="2">
                    创建新的打包任务</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">选择原包：</td>
                <td class="auto-style6" colspan="2">
                    <select id="package_original" class="auto-style9" name="package_original" onchange="select_original(this);">
                        <option selected="selected" value="0">--------------请选择原包--------------</option>
                        <%=load_original_package_info() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style12" style="text-align: right"></td>
                <td class="auto-style13" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="auto-style13" style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style12" style="text-align: right">&nbsp;</td>
                <td class="auto-style13" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style14" style="text-align: right"></td>
                <td class="auto-style15" style="font-size:15px; font-weight:bold; color:#333333;" colspan="2">
                    第一步：修改基本信息</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="base_name_check" name="base_name_check" type="checkbox" /><label for="base_name_check">修改名字</label></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">应用名：</td>
                <td class="auto-style11" colspan="2">
                    <input id="app_name" name="app_name" type="text" class="auto-style9" /> </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="base_version_check" name="base_version_check" type="checkbox" /><label for="base_version_check">修改版本信息</label></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">版本数字：</td>
                <td class="auto-style11" colspan="2">
                    <input id="app_version_code" name="app_version_code" type="text" class="auto-style9" /></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">版本字符：</td>
                <td class="auto-style11" colspan="2">
                    <input id="app_version_string" name="app_version_string" type="text" class="auto-style9" /></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="base_icon_check" name="base_icon_check" type="checkbox" /><label for="base_icon_check">修改ICON图标</label></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">ICON图标：</td>
                <td class="auto-style11" colspan="2">
                    <input id="app_icon_file" name="app_icon_file" accept=".png" class="auto-style9" type="file" /></td>
            </tr>
            <tr>
                <td class="auto-style16" style="text-align: right"></td>
                <td class="auto-style17" colspan="2">
                    <img id="app_icon" alt="ICON图标" style="width:80px; height:80px;" src="res/image/failed.png" /></td>
            </tr>
            <tr>
                <td class="auto-style22" style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style20" style="text-align: right"></td>
                <td class="auto-style15" style="font-size:15px; font-weight:bold; color:#333333;" colspan="2">
                    第二步：选择签名</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">选择签名：</td>
                <td class="auto-style11" colspan="2">
                    <select id="app_keystore" name="app_keystore" class="auto-style9" name="D2">
                        <option selected="selected" value="0">--------------请选择签名--------------</option>
                        <%=load_keysore_list() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style23" style="text-align: right"></td>
                <td class="auto-style24" style="font-size:15px; font-weight:bold; color:#333333;" colspan="2">
                    </td>
            </tr>
            <tr>
                <td class="auto-style25" style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" style="font-size:15px; font-weight:bold; color:#333333;" colspan="2">
                    第三步：选择需要接入的SDK</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="umeng_check" name="umeng_check" checked="checked" type="checkbox" /><label for="umeng_check">接入友盟统计</label> <input id="umeng_check_push" name="umeng_check_push" type="checkbox" /><label for="umeng_check_push">接入友盟PUSH</label>

                </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">统计类型：</td>
                <td class="auto-style11" colspan="2">
                    <input id="umeng_type_aly" name="umeng_type" checked="checked" type="radio" value="0" /><label for="umeng_type_aly">标准统计</label>&nbsp;&nbsp; 
                    <input id="umeng_type_game" name="umeng_type" type="radio" value="1" /><label for="umeng_type_game">游戏统计</label></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">友盟Key：</td>
                <td class="auto-style11" colspan="2">
                    <select id="umeng_key" name="umeng_key" class="auto-style9">
                        <option selected="selected" value="0">--------------请选择友盟Key--------------</option>
                        <%=load_umeng_list() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style26">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="gdt_check" name="gdt_check" type="checkbox" /><label for="gdt_check">接入广点通</label>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">类型：</td>
                <td class="auto-style11" colspan="2">
                    <input id="gdt_type_x5" checked="checked" name="gdt_type" type="radio" value="1" /><label for="gdt_type_x5">X5内核版</label>&nbsp;&nbsp; 
                    <input id="gdt_type_normal" name="gdt_type" type="radio" value="0" /><label for="gdt_type_normal">标准版</label>&nbsp;
                    </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">广点通Key：</td>
                <td class="auto-style11" colspan="2">
                    <select id="gdt_key" class="auto-style9" name="gdt_key">
                        <option selected="selected" value="0">--------------请选择广点通Key--------------</option>
                        <%=load_gdt_list() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="hao123_check" name="hao123_check" type="checkbox" /><label for="hao123_check">接入hao123</label>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="g3pp_check" name="g3pp_check" type="checkbox" /><label for="g3pp_check">接入3GPP</label>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">类型：</td>
                <td class="auto-style11" colspan="2">
                    <input id="g3pp_type_normal" checked="checked" name="g3pp_type" type="radio" value="1" /><label for="g3pp_type_normal">标准版</label>&nbsp;&nbsp; 
                    <input id="g3pp_type_game" name="g3pp_type" type="radio" value="0" /><label for="g3pp_type_game">游戏版（仅限胡杰买量的破解游戏使用）</label>&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">3GPP Key：</td>
                <td class="auto-style11" colspan="2">
                    <select id="g3pp_key" class="auto-style9" name="g3pp_key">
                        <option selected="selected" value="0">--------------请选择广点通Key--------------</option>
                        <%=load_3gpp_list() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="baidussp_check" name="baidussp_check" type="checkbox" /><label for="baidussp_check">接入百度SPP</label>&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">百度SSP Key：</td>
                <td class="auto-style11" colspan="2">
                    <select id="baidussp_key" class="auto-style9" name="baidussp_key">
                        <option selected="selected" value="0">--------------请选择百度SSP Key--------------</option>
                        <%=load_baidussp_list() %>
                    </select></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="txsg_check" name="txsg_check" type="checkbox" /><label for="txsg_check">接入腾讯手管（需要桂军的签名）</label>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <hr style="border:1px dashed #666;" />
                </td>
                <td class="auto-style11">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input id="txpush_check" name="txpush_check" type="checkbox" /><label for="txpush_check">接入腾讯Push（需要桂军的签名）</label>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2" style="font-size:15px; font-weight:bold; color:#333333;">
                    第四步：选择需要打的渠道</td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11">
                    <div style="width:625px;">
                        <%=load_channel_list() %>
                    </div>
                </td>
                <td class="auto-style11" style="font-size:15px; font-weight:bold; color:#333333; cursor:pointer;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="text-align: right" colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="auto-style27" style="text-align: right"></td>
                <td class="auto-style28" colspan="2">
                    </td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <input class="button" id="btn_submit" type="button" value="提交" onclick="insert_data();" /></td>
            </tr>
            <tr>
                <td class="auto-style10" style="text-align: right">&nbsp;</td>
                <td class="auto-style11" colspan="2">
                    <img id="load_img" style="display:none;" alt="" src="res/image/load.gif" /></td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right"></td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="text-align: right"></td>
                <td colspan="2">
                    </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function channel_click(n) {
        document.getElementById("channel_id_" + n).click();
        if (document.getElementById("channel_id_" + n).checked == true) {
            document.getElementById("image_channel_id_" + n).style.display = "block";
        } else {
            document.getElementById("image_channel_id_" + n).style.display = "none";
        }
    }

    var select_package_info = null;
    function select_original(obj) {
        var pkg_version_id = obj.value;
        if (pkg_version_id != 0) {
            var form = new FormData();
            form.append("type", "get_original_version_info");
            form.append("id", pkg_version_id + "");
            // XMLHttpRequest 对象
            var xhr = new XMLHttpRequest();
            xhr.open("post", "get_info.ashx", true);
            xhr.onload = function (oEvent) {
                if (xhr.status == 200) {
                    var result = JSON.parse(xhr.responseText);
                    if (result.state == 1) {
                        select_package_info = result.info;
                        document.getElementById("app_name").value = select_package_info.pkg_label;
                        document.getElementById("app_version_code").value = select_package_info.pkg_versioncode;
                        document.getElementById("app_version_string").value = select_package_info.pkg_versionstring;
                        document.getElementById("app_icon").src = select_package_info.pkg_icon_path;
                        
                    } else {
                        alert("获取信息失败！");
                    }
                }
            };
            xhr.send(form);
        }
    }

    function insert_data() {
        // FormData 对象
        var form = new FormData();
        var icon_file = document.getElementById("app_icon_file").files[0];
        form.append("package_original", document.getElementById("package_original").value);
        form.append("base_name_check", (document.getElementById("base_name_check").checked == true) ? "1" : "0");
        form.append("app_name", document.getElementById("app_name").value);
        form.append("base_version_check", (document.getElementById("base_version_check").checked == true) ? "1" : "0");
        form.append("app_version_code", document.getElementById("app_version_code").value);
        form.append("app_version_string", document.getElementById("app_version_string").value);
        form.append("base_icon_check", (document.getElementById("base_icon_check").checked == true) ? "1" : "0");
        form.append("app_keystore", document.getElementById("app_keystore").value);
        form.append("umeng_check", (document.getElementById("umeng_check").checked == true) ? "1" : "0");
        form.append("umeng_check_push", (document.getElementById("umeng_check_push").checked == true) ? "1" : "0");
        var umeng_type = "aly";
        if (document.getElementById("umeng_type_aly").checked == true)
            umeng_type = "aly";
        if (document.getElementById("umeng_type_game").checked == true)
            umeng_type = "game";
        form.append("umeng_type", umeng_type);
        form.append("umeng_key", document.getElementById("umeng_key").value);
        form.append("gdt_check", (document.getElementById("gdt_check").checked == true) ? "1" : "0");
        var gdt_type = "normal";
        if (document.getElementById("gdt_type_normal").checked == true)
            gdt_type = "normal";
        if (document.getElementById("gdt_type_x5").checked == true)
            gdt_type = "x5";
        form.append("gdt_type", gdt_type);
        form.append("gdt_key", document.getElementById("gdt_key").value);
        form.append("hao123_check", (document.getElementById("hao123_check").checked == true) ? "1" : "0");
        form.append("g3pp_check", (document.getElementById("g3pp_check").checked == true) ? "1" : "0");
        var g3pp_type = "normal";
        if (document.getElementById("g3pp_type_normal").checked == true)
            g3pp_type = "normal";
        if (document.getElementById("g3pp_type_game").checked == true)
            g3pp_type = "game";
        form.append("g3pp_type", g3pp_type);
        form.append("g3pp_key", document.getElementById("g3pp_key").value);
        form.append("txsg_check", (document.getElementById("txsg_check").checked == true) ? "1" : "0");
        form.append("txpush_check", (document.getElementById("txpush_check").checked == true) ? "1" : "0");
        form.append("baidussp_check", (document.getElementById("baidussp_check").checked == true) ? "1" : "0");
        form.append("baidussp_key", document.getElementById("baidussp_key").value);
        var app_channle_count = document.getElementById("app_channle_count").value;
        var app_channle_checked_count = 0;
        var app_channle_list = "";
        for (var i = 0; i < app_channle_count; i++) {
            var eid = "channel_id_" + i;
            var state = (document.getElementById(eid).checked == true) ? "1" : "0";
            if (state == "1") {
                app_channle_list += document.getElementById(eid).value + ",";
                app_channle_checked_count++;
            }
        }
        if (app_channle_list != "") app_channle_list = app_channle_list.substring(0,app_channle_list.length -1);
        form.append("app_channle_list", app_channle_list);
        form.append("file", icon_file);

        //判断
        if (document.getElementById("package_original").value == "0") {
            alert("请选择底包！");
            return;
        }
        if (icon_file == null && document.getElementById("base_icon_check").checked) {
            alert("请选择ICON图标");
            return;
        }
        if (document.getElementById("app_keystore").value == "0") {
            alert("请选择签名！");
            return;
        }
        if ((document.getElementById("umeng_check").checked || document.getElementById("umeng_check_push").checked)
            && document.getElementById("umeng_key").value == "0") {
            alert("请选择友盟Key！");
            return;
        }
        if (document.getElementById("gdt_check").checked
            && document.getElementById("gdt_key").value == "0") {
            alert("请选择广点通Key！");
            return;
        }
        if (document.getElementById("g3pp_check").checked
            && document.getElementById("g3pp_key").value == "0") {
            alert("请选择3GPP Key！");
            return;
        }
        if (document.getElementById("baidussp_check").checked
            && document.getElementById("baidussp_key").value == "0") {
            alert("请选择百度SSP Key！");
            return;
        }
        
        if (app_channle_checked_count == 0) {
            alert("请至少选择一个渠道！");
            return;
        }

        document.getElementById("package_original").disabled = true;
        document.getElementById("btn_submit").disabled = true;
        document.getElementById("btn_submit").style.cursor = "default";
        document.getElementById("btn_submit").style.backgroundColor = "#666666";
        document.getElementById("load_img").style.display = "block";
        

        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "create_pack_task.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("添加成功！");
                    document.location.href = "online_package_manager.aspx";
                } else {
                    alert(result.message);
                }
                document.getElementById("load_img").style.display = "none";
            }
        };
        xhr.send(form);
    }
</script>
