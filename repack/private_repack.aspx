<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="private_repack.aspx.cs" Inherits="repack.private_repack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打包平台-(友盟+广点通)</title>
    <style type="text/css">
        #File1 {
            width: 417px;
        }
        #Text1 {
            width: 311px;
        }
        #Text2 {
            width: 137px;
        }
        #Text3 {
            width: 166px;
        }
        #Text4 {
            width: 316px;
        }
        #Text5 {
            width: 316px;
        }
        #Text6 {
            width: 322px;
        }
        #Text7 {
            width: 329px;
        }
        #Submit1 {
            width: 83px;
            height: 33px;
        }
        #umeng_appkey {
            width: 233px;
        }
        #umeng_channel {
            width: 224px;
        }
        #gdt_appkey {
            width: 227px;
        }
        #gdt_appid {
            width: 235px;
        }
        #gdt_adid {
            width: 238px;
        }
        #gdt_adid0 {
            width: 238px;
        }
        #gdt_adid1 {
            width: 238px;
        }
        #gdt_adid2 {
            width: 238px;
        }
        #gdt_adid3 {
            width: 238px;
        }
        #umeng_appkey0 {
            width: 233px;
        }
        #umeng_push_messageid {
            width: 256px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        第一步：<br />
        <br />
        需要打包的APK（不超过100M）<br />
        <input id="apk" name="apk" type="file" accept=".apk" /><br />
        <br />
        第二步：<br />
        <br />
        <input id="display_check" name="display_check" checked="checked" type="checkbox" runat="server" />修改显示名<br />
        <input id="displayname" name="displayname" type="text" value="测试显示名" /><br />
        <br />
        <input id="version_check" name="version_check" checked="checked" type="checkbox" runat="server" />修改版本号<br />
        (数字)<input id="versioncode" name="versioncode" type="text" value="30" />(字符)<input id="versionstring" name="versionstring" type="text" value="3.0.0" /><br />
        <br />
        第三步：<br />
        <br />
        <input id="umeng_check" name="umeng_check" type="checkbox" runat="server" checked="checked" />友盟统计 
         
        <br />
        <input name="umeng_select" checked="checked" value="0" type="radio" />标准统计 
        <input name="umeng_select" value="1" type="radio" />游戏统计<br />
        AppKey:<input id="umeng_appkey" name="umeng_appkey" value="5632e539e0f55a7d780074a4" type="text" /><br />
        Channel:<input id="umeng_channel" name="umeng_channel" value="stiven" type="text" /><br />
        <br />
        <input id="umeng_push_check" name="umeng_push_check" type="checkbox" runat="server" checked="checked" />友盟Push<br />
        MessageID:<input id="umeng_push_messageid" name="umeng_push_messageid" value="3f309760e42617df4eb5403b4c795050" type="text" /><br />
        <br />
        <br />
        <input id="gdt_check" name="gdt_check" type="checkbox" checked="checked" runat="server" />广点通<br />
        AppKey:<input id="gdt_appkey" name="gdt_appkey" value="A610LRD46HznuG9iXq3l2GrZ" type="text" /><br />
        AppId:<input id="gdt_appid" name="gdt_appid" value="1101152570" type="text" /><br />
        AdId:<input id="gdt_adid" name="gdt_adid" value="8575134060152130849" type="text" /><br />
        <br />
        <input id="hao123_check" name="hao123_check" checked="checked" type="checkbox" runat="server" />hao123<br />
        <br />
        <input id="g3pp_check" name="g3pp_check" checked="checked" type="checkbox" runat="server" />3gpp<br />
        产品名：<input id="g3pp_appname" name="g3pp_appname" value="3gpp_youxi" type="text" /><br />
        产品ID：<input id="g3pp_pid" name="g3pp_pid" value="22100" type="text" /><br />
        渠道ID：<input id="g3pp_cid" name="g3pp_cid" value="32100" type="text" /><br />
        渠道名称：<input id="g3pp_channelname" name="g3pp_channelname" value="yingyongbao" type="text" /><br />
        <br />
        <input id="txsg_check" name="txsg_check" checked="checked" type="checkbox" runat="server" />腾讯手管<br />
        <br />
        <input id="Submit1" type="submit" value="提交" /></form>
</body>
</html>
