using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellPublic
    {
        public string m_insert_activity = string.Empty;
        public string m_insert_application = string.Empty;

        public repark_transfer_data m_transfer_data = null;
        public repack_tools.tool_func m_Func = null;
        protected string m_Apkpath = string.Empty;
        protected string m_SignedApkpath = string.Empty;
        protected repack_tools.tool_keystore m_Keystore = null;

        protected XmlDocument m_AndroidManifest_doc = null;

        public ShellPublic() {
            
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="apkpath">APK路径</param>
        /// <param name="signedapkpath">签名之后的APK存放目录</param>
        /// <param name="keystore">签名信息</param>
        public virtual void Init(string apkpath, string signedapkpath, repack_tools.tool_keystore keystore) {
            m_Func = new repack_tools.tool_func();
            m_Func.InitApk(apkpath, signedapkpath, keystore);
            m_Apkpath = apkpath;
            m_SignedApkpath = signedapkpath;
            m_Keystore = keystore;
        }

        /// <summary>
        /// 获取APK信息
        /// </summary>
        /// <returns></returns>
        public virtual repack_tools.tool_apk_model GetApkInfo() {
            return m_Func.GetApkInfo();
        }

        /// <summary>
        /// 获取AndroidManifest文件XML对象
        /// </summary>
        /// <returns></returns>
        public virtual XmlDocument GetAndroidManifest() {
            if (m_AndroidManifest_doc == null)
            {
                m_AndroidManifest_doc = new XmlDocument();
                m_AndroidManifest_doc.Load(GetApkInfo().AndroidManifestPath);
            }
            return m_AndroidManifest_doc;
        }

        /// <summary>
        /// 重新加载AndroidManifest.xml文档
        /// </summary>
        public virtual void ReloadAndroidManifest() {
            m_AndroidManifest_doc = new XmlDocument();
            m_AndroidManifest_doc.Load(GetApkInfo().AndroidManifestPath);
        }

        /// <summary>
        /// 重新加载配置信息
        /// </summary>
        public virtual void ReloadSettings() {
            ReloadAndroidManifest();
            ReadSettings();
        }

        /// <summary>
        /// 读取配置信息，包括AndroidManifest.xml和apktool.yml以及其他信息
        /// </summary>
        public virtual void ReadSettings() {
            ReloadAndroidManifest();
            repack_tools.tool_apk_model apkinfo = GetApkInfo();
            repack_tools.AndroidManifest_model settings = new repack_tools.AndroidManifest_model();
            try
            {
                XmlElement root_AndroidManifest = GetAndroidManifest().DocumentElement;
                //获取权限列表
                XmlNodeList nodePermissions = root_AndroidManifest.ChildNodes;
                settings.Permissions.Clear();
                for (int i = 0; i < nodePermissions.Count; i++) {
                    if (nodePermissions[i].Name != "application") {
                        settings.Permissions.Add(nodePermissions[i].OuterXml);
                    }
                }
                //获取包名
                apkinfo.NewPackageName = apkinfo.PrePackageName = root_AndroidManifest.Attributes["package"].Value;
                settings.PackageName = apkinfo.PrePackageName;

                XmlElement setting_application_node = (XmlElement)root_AndroidManifest.SelectSingleNode("/manifest/application");
                if (null != setting_application_node)
                {
                    //获取AndroidApplication
                    if (setting_application_node.Attributes["android:name"] != null)
                    {
                        settings.AndroidApplication = setting_application_node.Attributes["android:name"].Value;
                        if (settings.AndroidApplication[0] == '.')
                        {
                            //补全
                            settings.AndroidApplication = settings.PackageName + settings.AndroidApplication;
                        }
                    }
                    else {
                        settings.AndroidApplication = string.Empty;
                    }
                    //获取appname
                    string appname = setting_application_node.Attributes["android:label"].Value;
                    if (appname.IndexOf("@string/") != -1)//从资源文件中寻找对应的appname
                    {
                        appname = appname.Replace("@string/", "");
                        string value_string_path = apkinfo.in_res + @"\values\strings.xml";
                        XmlDocument xml_values_strings = new XmlDocument();
                        xml_values_strings.Load(value_string_path);
                        XmlElement xml_values_strings_root = xml_values_strings.DocumentElement;
                        foreach (XmlNode node_string in xml_values_strings_root.ChildNodes)
                        {
                            if (((XmlElement)node_string).Attributes["name"].Value == appname)
                            {
                                settings.AppName = node_string.InnerText;
                                break;
                            }
                        }
                    }
                    else
                    {
                        settings.AppName = appname;//直接取节点值，即appname
                    }

                    //获取appicon
                    string appicon = setting_application_node.Attributes["android:icon"].Value;
                    if (appicon.IndexOf("@drawable/") != -1)//从资源文件中寻找对应的appicon
                    {
                        appicon = appicon.Replace("@drawable/", "");
                        List<string> icons = new List<string>();
                        utils.get_files_from_folder(apkinfo.in_res, ref icons, appicon + ".png");
                        if (icons.Count > 0)
                        {
                            appicon = icons[0];
                            settings.IconFileName = appicon;
                        }
                    }

                    //获取MainActivity
                    XmlNodeList activitys = setting_application_node.SelectNodes("/manifest/application/activity");
                    if (activitys.Count > 0)
                    {
                        foreach (XmlNode activity in activitys)
                        {
                            if (activity.InnerXml.IndexOf("android.intent.action.MAIN") != -1)
                            {
                                settings.MainActivity = ((XmlElement)activity).Attributes["android:name"].Value;
                                if (settings.MainActivity[0] == '.')
                                {
                                    //补全
                                    settings.MainActivity = settings.PackageName + settings.MainActivity;
                                }
                                break;
                            }
                        }
                    }
                }

                //获取R.smali文件路径
                List<string> search_R_smali = new List<string>();
                utils.get_files_from_folder(apkinfo.in_smali, ref search_R_smali, "R.smali", "*.smali");
                if (search_R_smali.Count > 0)
                {
                    foreach (string r_smali in search_R_smali)
                    {
                        if (Path.GetFileNameWithoutExtension(r_smali) == "R")
                        {
                            settings.R = r_smali;
                            break;
                        }
                    }
                }

                //获取版本信息
                Encoding enc_yml = TxtFileEncoder.GetEncoding(apkinfo.ApktoolYml);
                string apktool_yml_str = File.ReadAllText(apkinfo.ApktoolYml, enc_yml);
                List<string> yml_strings = apktool_yml_str.Split((char)0x0A).ToList();
                int yml_pos = 0;
                foreach (string yml_attribute in yml_strings)
                {
                    if (yml_attribute.IndexOf("versionCode:") != -1)
                    {
                        yml_pos = yml_attribute.IndexOf('\'');
                        settings.VersionCode = yml_attribute.Substring(yml_pos + 1, yml_attribute.LastIndexOf('\'') - yml_pos - 1);
                    }
                    if (yml_attribute.IndexOf("versionName:") != -1)
                    {
                        yml_pos = yml_attribute.IndexOf('\'');
                        if (yml_pos != -1)
                            settings.VersionString = yml_attribute.Substring(yml_pos + 1, yml_attribute.LastIndexOf('\'') - yml_pos - 1);
                        else
                            settings.VersionString = yml_attribute.Replace("versionName:", "").Replace(" ", "");
                    }
                }

                apkinfo.settings = settings;
                m_Func.SetApkInfo(apkinfo);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 补全AndroidManifest中的包名
        /// </summary>
        public virtual void RepairAndroidManifest() {
            ReloadAndroidManifest();
            XmlElement root_AndroidManifest = GetAndroidManifest().DocumentElement;
            XmlElement setting_application_node = (XmlElement)root_AndroidManifest.SelectSingleNode("/manifest/application");
            if (setting_application_node.Attributes["android:name"] != null)
            {
                setting_application_node.Attributes["android:name"].Value = GetApkInfo().settings.AndroidApplication;
            }
            //补全包名
            XmlNodeList childNodes = setting_application_node.ChildNodes;
            string android_name = string.Empty;
            if (childNodes.Count > 0)
            {
                for (int i = 0; i < childNodes.Count; i++) {
                    if (childNodes[i].Attributes["android:name"] != null)
                    {
                        if (childNodes[i].Name == "meta-data") {
                            continue;
                        }
                        android_name = ((XmlElement)childNodes[i]).Attributes["android:name"].Value;
                        if (android_name[0] == '.')
                            ((XmlElement)childNodes[i]).Attributes["android:name"].Value = GetApkInfo().settings.PackageName + android_name;
                        else if (android_name.IndexOf(".") == -1)
                        {
                            ((XmlElement)childNodes[i]).Attributes["android:name"].Value = GetApkInfo().settings.PackageName + "." + android_name;
                        }
                    }
                }
            }
            m_AndroidManifest_doc.Save(GetApkInfo().AndroidManifestPath);
            ReloadAndroidManifest();
        }

        /// <summary>
        /// 修改包名
        /// </summary>
        /// <param name="newPackageName"></param>
        /// <param name="level">修改包名等级，0:只在配置文件中修改 1:修改对应的文件夹结构</param>
        public virtual void RePackageName(string newPackageName,int level = 0) {
            ReloadAndroidManifest();
            Encoding enc = TxtFileEncoder.GetEncoding(GetApkInfo().AndroidManifestPath);
            string AndroidManifest_str = File.ReadAllText(GetApkInfo().AndroidManifestPath, enc);
            List<string> rules = new List<string>();
            rules.Add("package=\"");
            rules.Add("android:name=\"");
            if (rules.Count > 0) {
                foreach (string rule in rules) {
                    AndroidManifest_str = AndroidManifest_str.Replace(rule + GetApkInfo().settings.PackageName, rule + newPackageName);
                }
            }
            File.WriteAllText(GetApkInfo().AndroidManifestPath, AndroidManifest_str, enc);
            ReloadAndroidManifest();
            //还原Activity和Service的包名
            XmlElement root_AndroidManifest = GetAndroidManifest().DocumentElement;
            XmlElement setting_application_node = (XmlElement)root_AndroidManifest.SelectSingleNode("/manifest/application");
            setting_application_node.Attributes["android:name"].Value = GetApkInfo().settings.AndroidApplication;
            XmlNodeList childNodes = setting_application_node.ChildNodes;
            string android_name = string.Empty;
            if (childNodes.Count > 0)
            {
                for (int i = 0; i < childNodes.Count; i++)
                {
                    if (childNodes[i].Name == "activity" || childNodes[i].Name == "service")
                    {
                        android_name = ((XmlElement)childNodes[i]).Attributes["android:name"].Value;
                        ((XmlElement)childNodes[i]).Attributes["android:name"].Value = android_name.Replace(newPackageName, GetApkInfo().PrePackageName);
                    }
                }
            }
            m_AndroidManifest_doc.Save(GetApkInfo().AndroidManifestPath);
            ReloadAndroidManifest();
        }

        /// <summary>
        /// 修改显示名
        /// </summary>
        /// <param name="newDisplayName"></param>
        public virtual void ReDisplayName(string newDisplayName) {
            ReloadAndroidManifest();
            XmlElement setting_application_node = (XmlElement)m_AndroidManifest_doc.DocumentElement.SelectSingleNode("/manifest/application");
            if (null != setting_application_node)
            {
                string appname = string.Empty;
                //获取MainActivity
                XmlNodeList activitys = setting_application_node.SelectNodes("/manifest/application/activity");
                if (activitys.Count > 0)
                {
                    foreach (XmlNode activity in activitys)
                    {
                        if (activity.InnerXml.IndexOf("android.intent.action.MAIN") != -1)
                        {
                            if (((XmlElement)activity).Attributes["android:label"] != null) {
                                appname = ((XmlElement)activity).Attributes["android:label"].Value;
                                if (appname.IndexOf("@string/") != -1)//从资源文件中寻找对应的appname
                                {
                                    appname = appname.Replace("@string/", "");

                                    List<string> string_xmls = new List<string>();
                                    utils.get_files_from_folder(GetApkInfo().in_res, ref string_xmls, "strings.xml");
                                    for (int i = 0; i < string_xmls.Count; i++) {
                                        string value_string_path = string_xmls[i];
                                        XmlDocument xml_values_strings = new XmlDocument();
                                        xml_values_strings.Load(value_string_path);
                                        XmlElement xml_values_strings_root = xml_values_strings.DocumentElement;
                                        foreach (XmlNode node_string in xml_values_strings_root.ChildNodes)
                                        {
                                            if (((XmlElement)node_string).Attributes["name"].Value == appname)
                                            {
                                                node_string.InnerText = newDisplayName;
                                                xml_values_strings.Save(value_string_path);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ((XmlElement)activity).Attributes["android:label"].Value = newDisplayName;
                                }
                            }
                            break;
                        }
                    }
                }
                if (setting_application_node.Attributes["android:label"] != null)
                {
                    appname = setting_application_node.Attributes["android:label"].Value;
                    if (appname.IndexOf("@string/") != -1)//从资源文件中寻找对应的appname
                    {
                        appname = appname.Replace("@string/", "");

                        List<string> string_xmls = new List<string>();
                        utils.get_files_from_folder(GetApkInfo().in_res, ref string_xmls, "strings.xml");
                        for (int i = 0; i < string_xmls.Count; i++)
                        {
                            string value_string_path = string_xmls[i];
                            XmlDocument xml_values_strings = new XmlDocument();
                            xml_values_strings.Load(value_string_path);
                            XmlElement xml_values_strings_root = xml_values_strings.DocumentElement;
                            foreach (XmlNode node_string in xml_values_strings_root.ChildNodes)
                            {
                                if (((XmlElement)node_string).Attributes["name"].Value == appname)
                                {
                                    node_string.InnerText = newDisplayName;
                                    xml_values_strings.Save(value_string_path);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        setting_application_node.Attributes["android:label"].Value = newDisplayName;
                    }
                }
                m_AndroidManifest_doc.Save(GetApkInfo().AndroidManifestPath);
                ReloadAndroidManifest();
            }
        }

        /// <summary>
        /// 修改版本号
        /// </summary>
        /// <param name="version_string">新的版本号字符串（特别注意，版本号字符串必须为三段或者四段，如3.0.0或3.0.0.0，不能为3或3.0）</param>
        /// <param name="version_code">新的版本号数字</param>
        public virtual void ReVersion(string version_string, string version_code) {
            Encoding enc = TxtFileEncoder.GetEncoding(GetApkInfo().ApktoolYml);
            string yml_str = File.ReadAllText(GetApkInfo().ApktoolYml, enc);
            List<string> yml_strings = yml_str.Split((char)0x0A).ToList();
            yml_str = string.Empty;
            for (int i = 0; i < yml_strings.Count; i++)
            {
                if (yml_strings[i].IndexOf("versionCode:") != -1)
                {
                    yml_strings[i] = yml_strings[i].Replace(GetApkInfo().settings.VersionCode, version_code);
                }
                if (yml_strings[i].IndexOf("versionName:") != -1)
                {
                    yml_strings[i] = yml_strings[i].Replace(GetApkInfo().settings.VersionString, version_string);
                }
                yml_str += yml_strings[i] + ((char)0x0A).ToString();
            }
            yml_str = yml_str.TrimEnd((char)0x0A);
            File.WriteAllText(GetApkInfo().ApktoolYml, yml_str, enc);
        }

        /// <summary>
        /// 修改图标
        /// </summary>
        /// <param name="iconpath"></param>
        public virtual void ReIcon(string iconpath) {
            repack_tools.tool_apk_model apkinfo = GetApkInfo();
            if (apkinfo.settings.IconFileName != "") {
                List<string> icons = new List<string>();
                utils.get_files_from_folder(apkinfo.in_res, ref icons, Path.GetFileName(apkinfo.settings.IconFileName));
                if (icons.Count > 0) {
                    foreach (string icon in icons) {
                        try {
                            File.Delete(icon);
                            File.Copy(iconpath, icon);
                        } catch (Exception) { }
                    }
                }
            }
        }

        /// <summary>
        /// 数据传输
        /// </summary>
        /// <param name="transfer_data"></param>
        public virtual void SetTransferData(repark_transfer_data transfer_data) {
            m_transfer_data = transfer_data;
        }

        /// <summary>
        /// 运行打包任务，需要override
        /// </summary>
        public virtual void RunTask() {

        }

        public virtual void SetInsertActivity(string activity) {
            m_insert_activity = activity;
        }
        public virtual void SetInsertApplication(string application) {
            m_insert_application = application;
        }
    }
}
