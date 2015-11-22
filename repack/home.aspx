<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="repack.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打包平台</title>
    <style type="text/css">
    .header-bg {
		margin-left:auto;
        margin-right:auto;
        width:100%;
        background-color:#145eb0;
	}
	.header {
		margin-left:auto;
        margin-right:auto;
        width:1200px;
        height:100px;
	}
    .header-line {
        margin-left:auto;
        margin-right:auto;
        background-color:#145eb0;
        height:10px;
        }
    .main-page {
        margin-top:10px;
        margin-left:auto;
        margin-right:auto;
        width:1200px;
        }
	.left-menu {
		width: 200px;
		height: 100%;
		float: left;
        background-color:#eeeeee;
	}

    .left-menu-cell {
        width:200px;
        height:40px;
        font-size:13px;
        font-weight:bold;
        text-align:center;
        vertical-align:middle;
        line-height:40px;
        float:left;
        cursor:pointer;
    }

    .left-menu-line {
        width:200px;
        height:1px;
        float:left;
        background-color:#333333;
        margin-top:10px;
        margin-bottom:10px;
    }

    .left-menu-blank {
        width:200px;
        height:40px;
        font-size:13px;
        font-weight:bold;
        text-align:center;
        vertical-align:middle;
        line-height:40px;
        float:left;
    }



    .left-menu-cell-normal {
        background-color:#eeeeee;
        color:#333333;
    }

    .left-menu-cell-hot {
        background-color:#145eb0;
        color:#ffffff;
        margin-left:-5px;
        width:210px;
    }


	.right-main {
        width:986px;
		float:right;
		height: 100%;
	}
</style>
</head>
<body style="margin:0px auto;">
    <div class="header-bg">
        <div class="header">
            <div style="font-size:36px; color:#FFFFFF; float:left; margin-top:20px; font-family:'微软雅黑'; font-weight:bold;">打包平台</div>
            <div style="font-size:14px; color:#FFFFFF; float:right; margin-top:40px;">您好，<%=showName %> <a href="logout.aspx" target="_self" style="color:#FFFFFF; font-weight:bold;">退出</a></div>
        </div>
    </div>
    <div id="main_page_div" class="main-page">
        <div class="left-menu">
            <div class="left-menu-blank"></div>
            <div id="left_menu_0" onclick="show_menu_page(0);" class="left-menu-cell left-menu-cell-hot">新建打包任务</div>
            <div id="left_menu_1" onclick="show_menu_page(1);" class="left-menu-cell">上线包管理</div>
            <div id="left_menu_2" onclick="show_menu_page(2);" class="left-menu-cell">原包管理</div>
            <div id="left_menu_3" onclick="show_menu_page(3);" class="left-menu-cell">SDK管理</div>
            <div id="left_menu_4" onclick="show_menu_page(4);" class="left-menu-cell">签名管理</div>
            <div id="left_menu_5" onclick="show_menu_page(5);" class="left-menu-cell">渠道管理</div>
            <div class="left-menu-line"></div>
            <div id="left_menu_6" onclick="show_menu_page(6);" class="left-menu-cell">UMENG KEY管理</div>
            <div id="left_menu_7" onclick="show_menu_page(7);" class="left-menu-cell">3GPP KEY管理</div>
            <div id="left_menu_8" onclick="show_menu_page(8);" class="left-menu-cell">广点通 KEY管理</div>
            <div id="left_menu_9" onclick="show_menu_page(9);" class="left-menu-cell">百度SSP KEY管理</div>
            <div class="left-menu-blank"></div>
        </div>
        <div class="right-main">
            <iframe id="main_area_page" name="main_area_page" style="border:0px;" src="create_package_task.aspx" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" onLoad="iFrameHeight()" onchange="iFrameHeight()"></iframe>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    function show_menu_page(n) {
        var menuid = "left_menu_" + n;
        for (var i = 0; i <=9; i++) {
            document.getElementById("left_menu_" + i).className = "left-menu-cell left-menu-cell-normal";
        }
        document.getElementById(menuid).className = "left-menu-cell left-menu-cell-hot";
        switch (n) {
            case 0:
                document.getElementById("main_area_page").src = "create_package_task.aspx";
                break;
            case 1:
                document.getElementById("main_area_page").src = "online_package_manager.aspx";
                break;
            case 2:
                document.getElementById("main_area_page").src = "original_package_manager.aspx";
                break;
            case 3:
                document.getElementById("main_area_page").src = "sdk_manager.aspx";
                break;
            case 4:
                document.getElementById("main_area_page").src = "keystore_manager.aspx";
                break;
            case 5:
                document.getElementById("main_area_page").src = "channel_manager.aspx";
                break;

            case 6:
                document.getElementById("main_area_page").src = "key_umeng_manager.aspx";
                break;
            case 7:
                document.getElementById("main_area_page").src = "key_3gpp_manager.aspx";
                break;
            case 8:
                document.getElementById("main_area_page").src = "key_gdt_manager.aspx";
                break;
            case 9:
                document.getElementById("main_area_page").src = "key_baidussp_manager.aspx";
                break;
            default: break;
        }
    }

    function iFrameHeight() {
        var ifm = document.getElementById("main_area_page");
        var subWeb = document.frames ? document.frames["main_area_page"].document : ifm.contentDocument;
        if (ifm != null && subWeb != null) {
            ifm.height = subWeb.body.scrollHeight;
            ifm.width = subWeb.body.scrollWidth;
        }
    }
    
</script>

