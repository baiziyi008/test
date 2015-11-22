using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public enum SmaliInsertType
    {
        /// <summary>
        /// 直接植入
        /// </summary>
        InsertSmali = 0x00,
        /// <summary>
        /// 继承
        /// </summary>
        Inheritance = 0x01,
        /// <summary>
        /// 引导
        /// </summary>
        Boot = 0x02,
    }

    /// <summary>
    /// public.xml文件节点
    /// </summary>
    public class public_xml_model
    {
        public string type = string.Empty;
        public string name = string.Empty;
        public string id = string.Empty;
        public int id_high = 0;
        public int id_low = 0;
    }

    /// <summary>
    /// 友盟统计类型
    /// </summary>
    public enum UmengType {
        /// <summary>
        /// 应用
        /// </summary>
        App = 0x00,
        /// <summary>
        /// 游戏
        /// </summary>
        Game = 0x01
    }

    /// <summary>
    /// 广点通类型
    /// </summary>
    public enum GdtType {
        /// <summary>
        /// 标准版
        /// </summary>
        Normal = 0x00,
        /// <summary>
        /// X5内核版
        /// </summary>
        X5 = 0x01
    }

    public enum G3ppType {
        /// <summary>
        /// 标准版
        /// </summary>
        Normal = 0x00,
        /// <summary>
        /// 游戏版
        /// </summary>
        Game = 0x01
    }

    /// <summary>
    /// 数据交换
    /// </summary>
    public class repark_transfer_data {
        public List<SdkType> sdks = new List<SdkType>();
        public Dictionary<string, string> channel_and_path_dict = new Dictionary<string, string>();
        public bool use_displayname = false;
        public bool use_version = false;
        public bool use_icon = false;
        public string displayname = string.Empty;
        public string versioncode = string.Empty;
        public string versionstring = string.Empty;
        public string iconpath = string.Empty;
        public table_repark_umeng umeng_key = null;
        public table_reaprk_gdt gdt_key = null;
        public table_repark_3gpp g3pp_key = null;
        public table_repark_baidussp baidussp_key = null;
    }

    #region 数据库表
    public class _table_struct
    {
        public int id = 0;
        public string ctime = string.Empty;
        public string table_name = string.Empty;
    }

    public class table_repark_user : _table_struct
    {
        public new string table_name = "repark_user";
        public string account = string.Empty;
        public string password = string.Empty;
        public string nickname = string.Empty;
        public string level = "0";
        public string group_ids = "0";
        public string email = string.Empty;
    }

    public class table_repark_group : _table_struct
    {
        public new string table_name = "repark_group";
        public string uid = "0";
        public string title = string.Empty;
        public string content = string.Empty;
    }

    public class table_repark_keystore : _table_struct
    {
        public new string table_name = "repark_keystore";
        public string uid = "0";
        public string gid = "0";
        public string title = string.Empty;
        public string path = string.Empty;
        public string alias = string.Empty;
        public string pwd1 = string.Empty;
        public string pwd2 = string.Empty;
        public string md5 = string.Empty;
    }

    public class table_repark_channel : _table_struct
    {
        public new string table_name = "repark_channel";
        public string uid = "0";
        public string gid = "0";
        public string channel_name = string.Empty;
        public string channel_sign = string.Empty;
    }

    public class table_repark_sdk : _table_struct
    {
        public new string table_name = "repark_sdk";
        public string uid = "0";
        public string gid = "0";
        public string sdk_title = string.Empty;
        public string sdk_content = string.Empty;
        public string sdk_versioncode = string.Empty;
        public string sdk_path = string.Empty;
        public string sdk_codeclass = string.Empty;
    }

    public class table_repark_package_original : _table_struct {
        public new string table_name = "repark_package_original";
        public string uid = "0";
        public string gid = "0";
        public string path = string.Empty;
        public string content = string.Empty;
        public string pkg_packagename = string.Empty;
        public int state = 0;
    }

    public class table_repark_package_original_version : _table_struct {
        public new string table_name = "repark_package_original_version";
        public int pkg_id = 0;
        public string pkg_path = string.Empty;
        public string pkg_versioncode = string.Empty;
        public string pkg_versionstring = string.Empty;
        public string pkg_label = string.Empty;
        public string pkg_icon_path = string.Empty;
        public string pkg_main_activity = string.Empty;
        public string pkg_application_name = string.Empty;
        public string title = string.Empty;
        public string content = string.Empty;
    }

    public class table_repark_package_published : _table_struct {
        public new string table_name = "repark_package_published";
        public string uid = "0";
        public string gid = "0";
        public string g3pp_id = string.Empty;
        public string umeng_id = string.Empty;
        public string original_id = string.Empty;
        public string channel_id = string.Empty;
        public string keystore_id = string.Empty;
        public string user_id = string.Empty;
        public string sdk_ids = string.Empty;
        public string title = string.Empty;
        public string content = string.Empty;
        public string path = string.Empty;
        public string app_name = string.Empty;
        public string app_package_name = string.Empty;
        public string app_icon = string.Empty;
        public string app_version_code = string.Empty;
        public string app_version_string = string.Empty;
    }

    public class table_reaprk_gdt : _table_struct {
        public new string table_name = "reaprk_gdt";
        public string title = string.Empty;
        public string uid = "0";
        public string gid = "0";
        public string content = string.Empty;
        public string gdt_packagename = string.Empty;
        public string gdt_appkey = string.Empty;
        public string gdt_appid = string.Empty;
        public string gdt_insert_adid = string.Empty;
        public string gdt_start_adid = string.Empty;
    }

    public class table_repark_baidussp : _table_struct
    {
        public new string table_name = "repark_baidussp";
        public string uid = "0";
        public string gid = "0";
        public string title = string.Empty;
        public string content = string.Empty;
        public string baidussp_appid = string.Empty;
        public string baidussp_insert_adid = string.Empty;
        public string baidussp_start_adid = string.Empty;
    }

    public class table_repark_3gpp : _table_struct {
        public new string table_name = "repark_3gpp";
        public string title = string.Empty;
        public string uid = "0";
        public string gid = "0";
        public string content = string.Empty;
        public string packagename = string.Empty;
        public string appkey = string.Empty;
        public string g3pp_pid = string.Empty;
        public string g3pp_cid = string.Empty;
        public string g3pp_product_name = string.Empty;
        public string g3pp_channel_name = string.Empty;
        public string local_pid = string.Empty;
        public string local_cid = string.Empty;
    }

    public class table_repark_umeng : _table_struct {
        public new string table_name = "repark_umeng";
        public string uid = "0";
        public string gid = "0";
        public string title = string.Empty;
        public string content = string.Empty;
        public string umeng_key = string.Empty;
        public string umeng_message_key = string.Empty;
        public string umeng_channel = string.Empty;
    }
    #endregion
}
