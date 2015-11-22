using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class Controller
    {
        static private Controller _control = null;
        static public Controller GetManager() {
            if (_control == null)
            {
                _control = new Controller();
            }
            return _control;
        }

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool add(object obj, ref string id)
        {
            return SqliteManager.GetManager().table_data_insert(obj, ref id);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool delete(object obj) {
            return SqliteManager.GetManager().table_data_delete(obj);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool login(string account, string pwd,ref table_repark_user userinfo) {
            string sql = "select * from repark_user where account='" + account + "' and password='" + utils.CreateMD5Hash(pwd).ToLower() + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null) {
                if (reader.Read()) {
                    userinfo.id = int.Parse(reader["id"].ToString());
                    userinfo.account = reader["account"].ToString();
                    userinfo.password =reader["password"].ToString();
                    userinfo.nickname =reader["nickname"].ToString();
                    userinfo.level = reader["level"].ToString();
                    userinfo.group_ids = reader["group_ids"].ToString();
                    userinfo.email = reader["email"].ToString();
                    userinfo.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public table_repark_user get_user(string account) {
            string sql = "select * from repark_user where account='" + account + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_user userinfo = new table_repark_user();
                    userinfo.id = int.Parse(reader["id"].ToString());
                    userinfo.account = reader["account"].ToString();
                    userinfo.password = reader["password"].ToString();
                    userinfo.nickname = reader["nickname"].ToString();
                    userinfo.email = reader["email"].ToString();
                    userinfo.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return userinfo;
                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在原包
        /// </summary>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public bool exist_original_package(string packagename) {
            string sql = "select * from repark_package_original where pkg_packagename='" + packagename.ToLower() + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取原包列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_package_original> get_original_list() {
            List<table_repark_package_original> original_apks = new List<table_repark_package_original>();
            string sql = "select * from repark_package_original order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_package_original original_apk = new table_repark_package_original();
                    original_apk.id = int.Parse(reader["id"].ToString());
                    original_apk.path = reader["path"].ToString();
                    original_apk.uid = reader["uid"].ToString();
                    original_apk.gid = reader["gid"].ToString();
                    original_apk.content = reader["content"].ToString();
                    original_apk.pkg_packagename = reader["pkg_packagename"].ToString();
                    original_apk.state = int.Parse(reader["state"].ToString());
                    original_apk.ctime = reader["ctime"].ToString();
                    original_apks.Add(original_apk);
                }
                reader.Close();
            }
            return original_apks;
        }

        /// <summary>
        /// 获取原包信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_package_original get_original(int id) {
            string sql = "select * from repark_package_original where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_package_original obj = new table_repark_package_original();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.path = reader["path"].ToString();
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.pkg_packagename = reader["pkg_packagename"].ToString();
                    obj.state = int.Parse(reader["state"].ToString());
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 获取原包版本列表
        /// </summary>
        /// <param name="original_id"></param>
        /// <returns></returns>
        public List<table_repark_package_original_version> get_original_version_list_from_original(int original_id) {
            List<table_repark_package_original_version> versions = new List<table_repark_package_original_version>();
            string sql = "select * from repark_package_original_version where pkg_id=" + original_id + " order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_package_original_version version = new table_repark_package_original_version();
                    version.id = int.Parse(reader["id"].ToString());
                    version.pkg_id = int.Parse(reader["pkg_id"].ToString());
                    version.pkg_path = reader["pkg_path"].ToString();
                    version.pkg_versioncode = reader["pkg_versioncode"].ToString();
                    version.pkg_versionstring = reader["pkg_versionstring"].ToString();
                    version.pkg_label = reader["pkg_label"].ToString();
                    version.pkg_icon_path = reader["pkg_icon_path"].ToString();
                    version.pkg_main_activity = reader["pkg_main_activity"].ToString();
                    version.pkg_application_name = reader["pkg_application_name"].ToString();
                    version.title = reader["title"].ToString();
                    version.content = reader["content"].ToString();
                    version.ctime = reader["ctime"].ToString();
                    versions.Add(version);
                }
                reader.Close();
            }
            return versions;
        }

        /// <summary>
        /// 获取原包版本信息
        /// </summary>
        /// <param name="versionid"></param>
        /// <returns></returns>
        public table_repark_package_original_version get_original_version(int versionid)
        {
            string sql = "select * from repark_package_original_version where id=" + versionid;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_package_original_version version = new table_repark_package_original_version();
                    version.id = int.Parse(reader["id"].ToString());
                    version.pkg_id = int.Parse(reader["pkg_id"].ToString());
                    version.pkg_path = reader["pkg_path"].ToString();
                    version.pkg_versioncode = reader["pkg_versioncode"].ToString();
                    version.pkg_versionstring = reader["pkg_versionstring"].ToString();
                    version.pkg_label = reader["pkg_label"].ToString();
                    version.pkg_icon_path = reader["pkg_icon_path"].ToString();
                    version.pkg_main_activity = reader["pkg_main_activity"].ToString();
                    version.pkg_application_name = reader["pkg_application_name"].ToString();
                    version.title = reader["title"].ToString();
                    version.content = reader["content"].ToString();
                    version.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return version;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在SDK
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool exist_sdk(string path,string sdkclass) {
            string sql = "select * from repark_sdk where sdk_path='" + path + "' or sdk_codeclass='" + sdkclass + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取SDK信息
        /// </summary>
        /// <param name="sdkclass"></param>
        /// <returns></returns>
        public table_repark_sdk get_sdk(string sdkclass) {
            string sql = "select * from repark_sdk where sdk_codeclass='" + sdkclass +"'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_sdk sdk = new table_repark_sdk();
                    sdk.id = int.Parse(reader["id"].ToString());
                    sdk.uid = reader["uid"].ToString();
                    sdk.gid = reader["gid"].ToString();
                    sdk.sdk_title = reader["sdk_title"].ToString();
                    sdk.sdk_content = reader["sdk_content"].ToString();
                    sdk.ctime = reader["ctime"].ToString();
                    sdk.sdk_versioncode = reader["sdk_versioncode"].ToString();
                    sdk.sdk_path = reader["sdk_path"].ToString();
                    sdk.sdk_codeclass = reader["sdk_codeclass"].ToString();
                    reader.Close();
                    return sdk;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 获取SDK列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_sdk> get_sdk_list() {
            List<table_repark_sdk> sdks = new List<table_repark_sdk>();
            string sql = "select * from repark_sdk order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_sdk sdk = new table_repark_sdk();
                    sdk.id = int.Parse(reader["id"].ToString());
                    sdk.uid = reader["uid"].ToString();
                    sdk.gid = reader["gid"].ToString();
                    sdk.sdk_title = reader["sdk_title"].ToString();
                    sdk.sdk_content = reader["sdk_content"].ToString();
                    sdk.ctime = reader["ctime"].ToString();
                    sdk.sdk_versioncode = reader["sdk_versioncode"].ToString();
                    sdk.sdk_path = reader["sdk_path"].ToString();
                    sdk.sdk_codeclass = reader["sdk_codeclass"].ToString();
                    sdks.Add(sdk);
                }
                reader.Close();
            }
            return sdks;
        }

        /// <summary>
        /// 获取SDK信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_sdk get_sdk(int id) {
            string sql = "select * from repark_sdk where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_sdk sdk = new table_repark_sdk();
                    sdk.id = int.Parse(reader["id"].ToString());
                    sdk.uid = reader["uid"].ToString();
                    sdk.gid = reader["gid"].ToString();
                    sdk.sdk_title = reader["sdk_title"].ToString();
                    sdk.sdk_content = reader["sdk_content"].ToString();
                    sdk.ctime = reader["ctime"].ToString();
                    sdk.sdk_versioncode = reader["sdk_versioncode"].ToString();
                    sdk.sdk_path = reader["sdk_path"].ToString();
                    sdk.sdk_codeclass = reader["sdk_codeclass"].ToString();
                    reader.Close();
                    return sdk;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在签名
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public bool exist_keystore(string md5) {
            string sql = "select * from repark_keystore where md5='" + md5 + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取签名列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_keystore> get_keystore_list() {
            List<table_repark_keystore> objs = new List<table_repark_keystore>();
            string sql = "select * from repark_keystore order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_keystore obj = new table_repark_keystore();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.path = reader["path"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    obj.alias = reader["alias"].ToString();
                    obj.pwd1 = reader["pwd1"].ToString();
                    obj.pwd2 = reader["pwd2"].ToString();
                    obj.md5 = reader["md5"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取签名信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_keystore get_keystore(int id) {
            string sql = "select * from repark_keystore where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_keystore obj = new table_repark_keystore();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.path = reader["path"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    obj.alias = reader["alias"].ToString();
                    obj.pwd1 = reader["pwd1"].ToString();
                    obj.pwd2 = reader["pwd2"].ToString();
                    obj.md5 = reader["md5"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在对应渠道
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public bool exist_channel(string name, string sign) {
            string sql = "select * from repark_channel where channel_name='" + name + "' or channel_sign='" + sign + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_channel> get_channel_list() {
            List<table_repark_channel> objs = new List<table_repark_channel>();
            string sql = "select * from repark_channel order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_channel obj = new table_repark_channel();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.channel_name = reader["channel_name"].ToString();
                    obj.channel_sign = reader["channel_sign"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取渠道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_channel get_channel(int id) {
            string sql = "select * from repark_channel where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_channel obj = new table_repark_channel();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.channel_name = reader["channel_name"].ToString();
                    obj.channel_sign = reader["channel_sign"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在友盟KEY
        /// </summary>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public bool exist_umeng_key(string appkey) {
            string sql = "select * from repark_umeng where umeng_key='" + appkey + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取友盟KEY列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_umeng> get_umeng_key_list() {
            List<table_repark_umeng> objs = new List<table_repark_umeng>();
            string sql = "select * from repark_umeng order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_umeng obj = new table_repark_umeng();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.umeng_key = reader["umeng_key"].ToString();
                    obj.umeng_message_key = reader["umeng_message_key"].ToString();
                    obj.umeng_channel = reader["umeng_channel"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取友盟KEY信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_umeng get_umeng_key(int id) {
            string sql = "select * from repark_umeng where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_umeng obj = new table_repark_umeng();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.umeng_key = reader["umeng_key"].ToString();
                    obj.umeng_message_key = reader["umeng_message_key"].ToString();
                    obj.umeng_channel = reader["umeng_channel"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在3gpp
        /// </summary>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public bool exist_3gpp(string packagename) {
            string sql = "select * from repark_3gpp where packagename='" + packagename + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取3gpp列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_3gpp> get_3gpp_list() {
            List<table_repark_3gpp> objs = new List<table_repark_3gpp>();
            string sql = "select * from repark_3gpp order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_3gpp obj = new table_repark_3gpp();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.packagename = reader["packagename"].ToString();
                    obj.appkey = reader["appkey"].ToString();
                    obj.g3pp_pid = reader["g3pp_pid"].ToString();
                    obj.g3pp_cid = reader["g3pp_cid"].ToString();
                    obj.g3pp_product_name = reader["g3pp_product_name"].ToString();
                    obj.g3pp_channel_name = reader["g3pp_channel_name"].ToString();
                    obj.local_pid = reader["local_pid"].ToString();
                    obj.local_cid = reader["local_cid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取3gpp信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_3gpp get_3gpp(int id) {
            string sql = "select * from repark_3gpp where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_3gpp obj = new table_repark_3gpp();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.packagename = reader["packagename"].ToString();
                    obj.appkey = reader["appkey"].ToString();
                    obj.g3pp_pid = reader["g3pp_pid"].ToString();
                    obj.g3pp_cid = reader["g3pp_cid"].ToString();
                    obj.g3pp_product_name = reader["g3pp_product_name"].ToString();
                    obj.g3pp_channel_name = reader["g3pp_channel_name"].ToString();
                    obj.local_pid = reader["local_pid"].ToString();
                    obj.local_cid = reader["local_cid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在广点通
        /// </summary>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public bool exist_gdt(string packagename) {
            string sql = "select * from reaprk_gdt where gdt_packagename='" + packagename + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取广点通列表
        /// </summary>
        /// <returns></returns>
        public List<table_reaprk_gdt> get_gdt_list() {
            List<table_reaprk_gdt> objs = new List<table_reaprk_gdt>();
            string sql = "select * from reaprk_gdt order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_reaprk_gdt obj = new table_reaprk_gdt();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.gdt_packagename = reader["gdt_packagename"].ToString();
                    obj.gdt_appkey = reader["gdt_appkey"].ToString();
                    obj.gdt_appid = reader["gdt_appid"].ToString();
                    obj.gdt_insert_adid = reader["gdt_insert_adid"].ToString();
                    obj.gdt_start_adid = reader["gdt_start_adid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取广点通信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_reaprk_gdt get_gdt(int id) {
            string sql = "select * from reaprk_gdt where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_reaprk_gdt obj = new table_reaprk_gdt();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.gdt_packagename = reader["gdt_packagename"].ToString();
                    obj.gdt_appkey = reader["gdt_appkey"].ToString();
                    obj.gdt_appid = reader["gdt_appid"].ToString();
                    obj.gdt_insert_adid = reader["gdt_insert_adid"].ToString();
                    obj.gdt_start_adid = reader["gdt_start_adid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在百度SSP
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public bool exist_baidussp(string appid) {
            string sql = "select * from repark_baidussp where baidussp_appid='" + appid + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取百度SSP信息列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_baidussp> get_baidussp_list() {
            List<table_repark_baidussp> objs = new List<table_repark_baidussp>();
            string sql = "select * from repark_baidussp order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_baidussp obj = new table_repark_baidussp();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.baidussp_appid = reader["baidussp_appid"].ToString();
                    obj.baidussp_insert_adid = reader["baidussp_insert_adid"].ToString();
                    obj.baidussp_start_adid = reader["baidussp_start_adid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取百度SSP信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_baidussp get_baidussp(int id) {
            string sql = "select * from repark_baidussp where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_baidussp obj = new table_repark_baidussp();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.baidussp_appid = reader["baidussp_appid"].ToString();
                    obj.baidussp_insert_adid = reader["baidussp_insert_adid"].ToString();
                    obj.baidussp_start_adid = reader["baidussp_start_adid"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 获取发布列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_package_published> get_publish_list() {
            List<table_repark_package_published> objs = new List<table_repark_package_published>();
            string sql = "select * from repark_package_published order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_package_published obj = new table_repark_package_published();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.original_id = reader["original_id"].ToString();
                    obj.g3pp_id = reader["g3pp_id"].ToString();
                    obj.umeng_id = reader["umeng_id"].ToString();
                    obj.channel_id = reader["channel_id"].ToString();
                    obj.keystore_id = reader["keystore_id"].ToString();
                    obj.user_id = reader["user_id"].ToString();
                    obj.sdk_ids = reader["sdk_ids"].ToString();
                    obj.path = reader["path"].ToString();
                    obj.app_name = reader["app_name"].ToString();
                    obj.app_package_name = reader["app_package_name"].ToString();
                    obj.app_icon = reader["app_icon"].ToString();
                    obj.app_version_code = reader["app_version_code"].ToString();
                    obj.app_version_string = reader["app_version_string"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取发布列表
        /// </summary>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public List<table_repark_package_published> get_publish_list(string start_time,string end_time)
        {
            List<table_repark_package_published> objs = new List<table_repark_package_published>();
            string sql = "select * from repark_package_published where ctime>" + start_time + " and ctime<=" + end_time + " order by id desc";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_package_published obj = new table_repark_package_published();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.original_id = reader["original_id"].ToString();
                    obj.g3pp_id = reader["g3pp_id"].ToString();
                    obj.umeng_id = reader["umeng_id"].ToString();
                    obj.channel_id = reader["channel_id"].ToString();
                    obj.keystore_id = reader["keystore_id"].ToString();
                    obj.user_id = reader["user_id"].ToString();
                    obj.sdk_ids = reader["sdk_ids"].ToString();
                    obj.path = reader["path"].ToString();
                    obj.app_name = reader["app_name"].ToString();
                    obj.app_package_name = reader["app_package_name"].ToString();
                    obj.app_icon = reader["app_icon"].ToString();
                    obj.app_version_code = reader["app_version_code"].ToString();
                    obj.app_version_string = reader["app_version_string"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取online包
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_package_published get_publish(int id)
        {
            string sql = "select * from repark_package_published where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_package_published obj = new table_repark_package_published();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.gid = reader["gid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.original_id = reader["original_id"].ToString();
                    obj.g3pp_id = reader["g3pp_id"].ToString();
                    obj.umeng_id = reader["umeng_id"].ToString();
                    obj.channel_id = reader["channel_id"].ToString();
                    obj.keystore_id = reader["keystore_id"].ToString();
                    obj.user_id = reader["user_id"].ToString();
                    obj.sdk_ids = reader["sdk_ids"].ToString();
                    obj.path = reader["path"].ToString();
                    obj.app_name = reader["app_name"].ToString();
                    obj.app_package_name = reader["app_package_name"].ToString();
                    obj.app_icon = reader["app_icon"].ToString();
                    obj.app_version_code = reader["app_version_code"].ToString();
                    obj.app_version_string = reader["app_version_string"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;

                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// 是否存在分组
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool exist_group(string title) {
            string sql = "select * from repark_group where title='" + title + "'";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 获取某用户的分组列表
        /// </summary>
        /// <param name="uid">用户ID（0为公共分组）</param>
        /// <returns></returns>
        public List<table_repark_group> get_group_list(int uid) {
            List<table_repark_group> objs = new List<table_repark_group>();
            string sql = "select * from repark_group where uid=" + uid.ToString();
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_group obj = new table_repark_group();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <returns></returns>
        public List<table_repark_group> get_group_list() {
            List<table_repark_group> objs = new List<table_repark_group>();
            string sql = "select * from repark_group";
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                while (reader.Read())
                {
                    table_repark_group obj = new table_repark_group();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    objs.Add(obj);
                }
                reader.Close();
            }
            return objs;
        }

        /// <summary>
        /// 获取分组信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public table_repark_group get_group(int id) {
            string sql = "select * from repark_group where id=" + id;
            SQLiteDataReader reader = SqliteManager.GetManager().get_reader(sql);
            if (reader != null)
            {
                if (reader.Read())
                {
                    table_repark_group obj = new table_repark_group();
                    obj.id = int.Parse(reader["id"].ToString());
                    obj.uid = reader["uid"].ToString();
                    obj.title = reader["title"].ToString();
                    obj.content = reader["content"].ToString();
                    obj.ctime = reader["ctime"].ToString();
                    reader.Close();
                    return obj;
                }
                reader.Close();
            }
            return null;
        }
    }
}
