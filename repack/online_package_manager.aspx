<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="online_package_manager.aspx.cs" Inherits="repack.online_package_manager" %>

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
        .menu-input-time {
            height:36px;
            width:180px;
            border:#CCCCCC solid 1px;
            padding-left:15px;
        }
        </style>
    <link rel="stylesheet" type="text/css" href="js/lq.datetimepick.css"/>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src='js/selectUi.js' type='text/javascript'></script>
    <script src='js/lq.datetimepick.js' type='text/javascript'></script>
    <script type="text/javascript">
        $(function () {

            $("#datetimepicker1").on("click", function (e) {
                e.stopPropagation();
                $(this).lqdatetimepicker({
                    css: 'datetime-hour'
                });
            });
            $("#datetimepicker2").on("click", function (e) {
                e.stopPropagation();
                $(this).lqdatetimepicker({
                    css: 'datetime-hour'
                });

            });

            $("#search_start").on("click", function (e) {
                e.stopPropagation();
                $(this).lqdatetimepicker({
                    css: 'datetime-day',
                    dateType: 'D',
                    selectback: function () {

                    }
                });

            });

            $("#search_end").on("click", function (e) {
                e.stopPropagation();
                $(this).lqdatetimepicker({
                    css: 'datetime-day',
                    dateType: 'D',
                    selectback: function () {

                    }
                });

            });
        });

        function search_publish_apk() {
            return true;
        }
</script>
</head>
<body style="margin:0px auto; width:986px;">
    <div style="width:100%; height:80px; background-color:#eeeeee; font-size:13px; font-family:'微软雅黑'; color:#666666;">
        <form action="online_package_manager.aspx" method="post" runat="server">
        <div style="margin-left:40px; margin-top:22px; float:left;">开始时间&nbsp; 
            <input id="search_start" name="search_start" readonly="true" type="text" class="menu-input-time" value="<%=dt_start.ToShortDateString().Replace(@"/",@"-")%>" /> 到&nbsp; 
            <input id="search_end" name="search_end" readonly="true" type="text" class="menu-input-time" value="<%=dt_end.ToShortDateString().Replace(@"/",@"-")%>" />&nbsp;
            <input id="search_button" type="submit" class="menu-button" value="查询" onclick="return search_publish_apk();" />
        </div>
        </form>
    </div>
    <div style="width:100%; margin-top:10px;">
        <table style="width:100%; font-size:13px; font-family:'微软雅黑'; color:#666666; border:0px;" border="0" cellspacing="0" cellpadding="0">
            <%=load_list() %>
        </table>
    </div>
    <div style="width:100%; margin-top:10px; height:400px;"></div>
</body>
</html>
<script type="text/javascript">
    function on_delete(n) {
        // FormData 对象
        var form = new FormData();
        form.append("type", "delete_online_form");
        form.append("id", n + "");

        // XMLHttpRequest 对象
        var xhr = new XMLHttpRequest();
        xhr.open("post", "delete_data.ashx", true);
        xhr.onload = function (oEvent) {
            if (xhr.status == 200) {
                var result = JSON.parse(xhr.responseText);
                if (result.state == 1) {
                    alert("删除成功！");
                    document.location.href = "online_package_manager.aspx";
                } else {
                    alert(result.message);
                }
            }
        };
        xhr.send(form);
    }
</script>
