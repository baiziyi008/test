/// Author:stiven
/// qq:670435718
/// email:670435718@qq.com
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellSdk : ISdkMerge
    {
        public string m_sdk_path = string.Empty;
        public repack_tools.tool_apk_model m_apkinfo = null;
        public repack_tools.tool_apk_model m_sdkinfo = null;
        public XmlDocument m_AndroidManifest_doc = null;

        public virtual void InitSdk(string sdk_decompiler_path, repack_tools.tool_apk_model apkinfo)
        {
            m_sdk_path = sdk_decompiler_path;
            m_apkinfo = apkinfo;

            m_sdkinfo = new repack_tools.tool_apk_model();
            m_sdkinfo.DecompilerFolder = sdk_decompiler_path;
            m_sdkinfo.AndroidManifestPath = sdk_decompiler_path + @"\AndroidManifest.xml";
            m_sdkinfo.ApktoolYml = sdk_decompiler_path + @"\apktool.yml";
            m_sdkinfo.in_assets = sdk_decompiler_path + @"\assets";
            m_sdkinfo.in_lib = sdk_decompiler_path + @"\lib";
            m_sdkinfo.in_res = sdk_decompiler_path + @"\res";
            m_sdkinfo.in_smali = sdk_decompiler_path + @"\smali";
            m_sdkinfo.in_unknown = sdk_decompiler_path + @"\unknown";
            m_sdkinfo.settings = new repack_tools.AndroidManifest_model();

            m_AndroidManifest_doc = new XmlDocument();
            m_AndroidManifest_doc.Load(m_sdkinfo.AndroidManifestPath);
            //获取SDK包名
            m_sdkinfo.PrePackageName = m_sdkinfo.NewPackageName = m_AndroidManifest_doc.DocumentElement.Attributes["package"].Value;
            m_sdkinfo.settings.PackageName = apkinfo.PrePackageName;

            //获取权限列表
            XmlNodeList nodePermissions = m_AndroidManifest_doc.DocumentElement.ChildNodes;
            m_sdkinfo.settings.Permissions.Clear();
            for (int i = 0; i < nodePermissions.Count; i++)
            {
                if (nodePermissions[i].Name != "application")
                {
                    m_sdkinfo.settings.Permissions.Add(nodePermissions[i].OuterXml);
                }
            }
        }

        public virtual void MergeAndroidManifest()
        {
            string node_typename = string.Empty;
            //读取SDK配置信息
            XmlElement sdk_application_node = (XmlElement)m_AndroidManifest_doc.DocumentElement.SelectSingleNode("/manifest/application");
            Dictionary<string, List<XmlElement>> dict_sdk_app = new Dictionary<string, List<XmlElement>>();
            Dictionary<string, List<XmlElement>> dict_sdk_permission = new Dictionary<string, List<XmlElement>>();
            XmlNodeList sdk_nodeApps = sdk_application_node.ChildNodes;
            for (int i = 0; i < sdk_nodeApps.Count; i++) {
                if (sdk_nodeApps[i].InnerXml.IndexOf("android.intent.action.MAIN") != -1)
                    continue;
                node_typename = sdk_nodeApps[i].Name;
                if (!dict_sdk_app.ContainsKey(node_typename))
                    dict_sdk_app.Add(node_typename, new List<XmlElement>());
                dict_sdk_app[node_typename].Add((XmlElement)sdk_nodeApps[i]);
            }
            XmlNodeList sdk_nodePermissions = m_AndroidManifest_doc.DocumentElement.ChildNodes;
            for (int i = 0; i < sdk_nodePermissions.Count; i++) {
                node_typename = sdk_nodePermissions[i].Name;
                if (node_typename == "application")
                    continue;
                if (!dict_sdk_permission.ContainsKey(node_typename))
                    dict_sdk_permission.Add(node_typename, new List<XmlElement>());
                dict_sdk_permission[node_typename].Add((XmlElement)sdk_nodePermissions[i]);
            }
            //读取APK配置信息
            XmlDocument apk_doc = new XmlDocument();
            apk_doc.Load(m_apkinfo.AndroidManifestPath);
            XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
            Dictionary<string, List<XmlElement>> dict_apk_app = new Dictionary<string, List<XmlElement>>();
            Dictionary<string, List<XmlElement>> dict_apk_permission = new Dictionary<string, List<XmlElement>>();
            XmlNodeList apk_nodeApps = apk_application_node.ChildNodes;
            for (int i = 0; i < apk_nodeApps.Count; i++) {
                if (apk_nodeApps[i].InnerXml.IndexOf("android.intent.action.MAIN") != -1)
                    continue;
                node_typename = apk_nodeApps[i].Name;
                if (!dict_apk_app.ContainsKey(node_typename))
                    dict_apk_app.Add(node_typename, new List<XmlElement>());
                dict_apk_app[node_typename].Add((XmlElement)apk_nodeApps[i]);
            }
            XmlNodeList apk_nodePermissions = apk_doc.DocumentElement.ChildNodes;
            for (int i = 0; i < apk_nodePermissions.Count; i++) {
                node_typename = apk_nodePermissions[i].Name;
                if (node_typename == "application")
                    continue;
                if (!dict_apk_permission.ContainsKey(node_typename))
                    dict_apk_permission.Add(node_typename, new List<XmlElement>());
                dict_apk_permission[node_typename].Add((XmlElement)apk_nodePermissions[i]);
            }
            //合并app节点内容
            bool flag = false;
            foreach (string key in dict_sdk_app.Keys)
            {
                if (dict_apk_app.ContainsKey(key))
                {
                    foreach (XmlElement node_sdk_app in dict_sdk_app[key])
                    {
                        flag = false;
                        foreach (XmlElement node_apk_app in dict_apk_app[key])
                        {
                            if (node_apk_app.Attributes["android:name"].Value == node_sdk_app.Attributes["android:name"].Value)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            apk_application_node.AppendChild(apk_doc.ImportNode(node_sdk_app, true));
                        }
                    }
                }
                else
                {
                    foreach (XmlElement node_sdk_app in dict_sdk_app[key])
                    {
                        apk_application_node.AppendChild(apk_doc.ImportNode(node_sdk_app, true));
                    }
                }
            }
            //合并permission内容
            foreach (string key in dict_sdk_permission.Keys) {
                if (dict_apk_permission.ContainsKey(key)) {
                    foreach (XmlElement node_sdk_permission in dict_sdk_permission[key])
                    {
                        flag = false;
                        foreach (XmlElement node_apk_permission in dict_apk_permission[key])
                        {
                            if (node_apk_permission.Attributes["android:name"].Value == node_sdk_permission.Attributes["android:name"].Value)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            apk_doc.DocumentElement.AppendChild(apk_doc.ImportNode(node_sdk_permission, true));
                        }
                    }
                }
                else
                {
                    foreach (XmlElement node_sdk_permission in dict_sdk_permission[key])
                    {
                        apk_doc.DocumentElement.AppendChild(apk_doc.ImportNode(node_sdk_permission, true));
                    }
                }
            }
            //保存AndroidManifest.xml
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }

        public virtual void MergeAssets()
        {
            if (!Directory.Exists(m_sdkinfo.in_assets))
                return;
            if (!Directory.Exists(m_apkinfo.in_assets))
                Directory.CreateDirectory(m_apkinfo.in_assets);
            List<string> sdk_assets_folders = Directory.GetDirectories(m_sdkinfo.in_assets).ToList();
            List<string> sdk_assets_files = Directory.GetFiles(m_sdkinfo.in_assets).ToList();
            List<string> apk_assets_folders = Directory.GetDirectories(m_apkinfo.in_assets).ToList();
            List<string> apk_assets_files = Directory.GetFiles(m_apkinfo.in_assets).ToList();
            //合并文件夹（只合并一级）
            bool flag = false;
            string path_name1 = string.Empty;
            string path_name2 = string.Empty;
            do
            {
                if (sdk_assets_folders.Count == 0) break;
                foreach (string sdk_key in sdk_assets_folders)
                {
                    path_name1 = (new DirectoryInfo(sdk_key)).Name;
                    if (apk_assets_folders.Count > 0)
                    {
                        flag = false;
                        foreach (string apk_key in apk_assets_folders)
                        {
                            path_name2 = (new DirectoryInfo(apk_key)).Name;
                            if (path_name1.ToUpper() == path_name2.ToUpper())
                            {
                                flag = true;
                                break;
                            }
                            if (!flag)
                            {
                                utils.copy_folder(sdk_key, m_apkinfo.in_assets);
                            }
                        }
                    }
                    else
                    {
                        utils.copy_folder(sdk_key, m_apkinfo.in_assets);
                    }
                }
            } while (false);
            //合并文件（只合并一级）
            do
            {
                if (sdk_assets_files.Count == 0) break;
                foreach (string sdk_key in sdk_assets_files) {
                    if (apk_assets_files.Count > 0)
                    {
                        flag = false;
                        foreach (string apk_key in apk_assets_files)
                        {
                            if (Path.GetFileName(sdk_key).ToUpper() == Path.GetFileName(apk_key).ToUpper())
                            {
                                flag = true;
                                break;
                            }
                            if (!flag)
                            {
                                File.Copy(sdk_key, m_apkinfo.in_assets + @"\" + Path.GetFileName(sdk_key), true);
                            }
                        }
                    }
                    else {
                        File.Copy(sdk_key, m_apkinfo.in_assets + @"\" + Path.GetFileName(sdk_key), true);
                    }
                }
            } while (false);
        }

        public virtual void MergeLib()
        {
            if (!Directory.Exists(m_sdkinfo.in_lib))
                return;
            List<string> lib_folders = new List<string>();
            lib_folders.Add("armeabi");//通用so
            lib_folders.Add("armeabi-v7a");
            lib_folders.Add("x86");
            string lib_folder_name = string.Empty;
            List<string> sdk_lib_folder = Directory.GetDirectories(m_sdkinfo.in_lib).ToList();
            if (sdk_lib_folder.Count == 0)
                return;
            if (!Directory.Exists(m_apkinfo.in_lib))
            {
                Directory.CreateDirectory(m_apkinfo.in_lib);
                foreach (string sdk_lib in sdk_lib_folder)
                {
                    utils.copy_folder(sdk_lib, m_apkinfo.in_lib);
                }
            }
            else
            {
                List<string> apk_lib_folder = Directory.GetDirectories(m_apkinfo.in_lib).ToList();
                bool flag = false;
                foreach (string apk_lib in apk_lib_folder)
                {
                    lib_folder_name = m_sdkinfo.in_lib + @"\" + (new DirectoryInfo(apk_lib)).Name;
                    if (Directory.Exists(lib_folder_name))
                    {
                        List<string> so_list = Directory.GetFiles(lib_folder_name).ToList();
                        foreach (string so in so_list)
                        {
                            if (!flag) flag = true;
                            File.Copy(so, apk_lib + @"\" + Path.GetFileName(so), true);
                        }
                    }
                }
                if (!flag) {
                    flag = false;
                    foreach (string key in lib_folders) {
                        lib_folder_name = m_sdkinfo.in_lib + @"\" + key;
                        if (Directory.Exists(lib_folder_name))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) {
                        List<string> so_list = Directory.GetFiles(lib_folder_name).ToList();
                        foreach (string so in so_list)
                        {
                            foreach (string apkkey in apk_lib_folder) {
                                File.Copy(so, apkkey + @"\" + Path.GetFileName(so), true);
                            }
                        }
                    }
                }
                do
                {
                    //从armeabi复制多余的so到其它APP_ABI
                    List<string> so_normal_list = new List<string>();
                    lib_folder_name = m_apkinfo.in_lib + @"\armeabi";
                    if (Directory.Exists(m_apkinfo.in_lib + @"\armeabi"))
                    {
                        lib_folder_name = m_apkinfo.in_lib + @"\armeabi";
                        so_normal_list = Directory.GetFiles(m_apkinfo.in_lib + @"\armeabi").ToList();
                    }
                    else if (Directory.Exists(m_apkinfo.in_lib + @"\armeabi-v7a"))
                    {
                        lib_folder_name = m_apkinfo.in_lib + @"\armeabi-v7a";
                        so_normal_list = Directory.GetFiles(m_apkinfo.in_lib + @"\armeabi-v7a").ToList();
                    }else if (Directory.Exists(m_apkinfo.in_lib + @"\x86"))
                    {
                        lib_folder_name = m_apkinfo.in_lib + @"\x86";
                        so_normal_list = Directory.GetFiles(m_apkinfo.in_lib + @"x86").ToList();
                    }
                    else break;
                    if (so_normal_list.Count > 0)
                    {
                        foreach (string apk_lib in apk_lib_folder)
                        {
                            if (apk_lib == lib_folder_name) continue;
                            foreach (string so in so_normal_list)
                            {
                                if(!File.Exists(apk_lib + @"\" + Path.GetFileName(so)))
                                    File.Copy(so, apk_lib + @"\" + Path.GetFileName(so), true);
                            }
                        }
                    }
                } while (false);
            }
        }

        public virtual void MergeRes()
        {
            if (!Directory.Exists(m_sdkinfo.in_res)) return;
            string sdk_public = m_sdkinfo.in_res + @"\values\public.xml";
            string apk_public = m_apkinfo.in_res + @"\values\public.xml";
            SortedDictionary<string, List<public_xml_model>> sdk_dict = new SortedDictionary<string, List<public_xml_model>>();
            SortedDictionary<string, List<public_xml_model>> apk_dict = new SortedDictionary<string, List<public_xml_model>>();
            XmlDocument sdk_public_doc = new XmlDocument();
            sdk_public_doc.Load(sdk_public);
            XmlNodeList sdk_nodes = sdk_public_doc.DocumentElement.ChildNodes;
            if (sdk_nodes.Count > 0)
            {
                foreach (XmlNode node in sdk_nodes)
                {
                    public_xml_model pxm = new public_xml_model();
                    pxm.type = node.Attributes["type"].Value;
                    pxm.name = node.Attributes["name"].Value;
                    pxm.id = node.Attributes["id"].Value;
                    pxm.id_high = Convert.ToInt32(pxm.id.Substring(2, 4), 16);
                    pxm.id_low = Convert.ToInt32(pxm.id.Substring(6, 4), 16);
                    if (!sdk_dict.ContainsKey(pxm.type))
                        sdk_dict.Add(pxm.type, new List<public_xml_model>());
                    sdk_dict[pxm.type].Add(pxm);
                }
            }
            XmlDocument apk_public_doc = new XmlDocument();
            apk_public_doc.Load(apk_public);
            XmlNodeList apk_nodes = apk_public_doc.DocumentElement.ChildNodes;
            if (apk_nodes.Count > 0)
            {
                foreach (XmlNode node in apk_nodes)
                {
                    public_xml_model pxm = new public_xml_model();
                    pxm.type = node.Attributes["type"].Value;
                    pxm.name = node.Attributes["name"].Value;
                    pxm.id = node.Attributes["id"].Value;
                    pxm.id_high = Convert.ToInt32(pxm.id.Substring(2, 4), 16);
                    pxm.id_low = Convert.ToInt32(pxm.id.Substring(6, 4), 16);
                    if (!apk_dict.ContainsKey(pxm.type))
                        apk_dict.Add(pxm.type, new List<public_xml_model>());
                    apk_dict[pxm.type].Add(pxm);
                }
            }

            //调整dict集合内数据
            if (sdk_dict.Count > 0)
            {
                for (int sdk_pos = 0; sdk_pos < sdk_dict.Count; sdk_pos++)
                {
                    KeyValuePair<string, List<public_xml_model>> item = sdk_dict.ElementAt(sdk_pos);
                    List<public_xml_model> pxms = item.Value;
                    utils.quick_sort_fast<public_xml_model>(ref pxms, 0, pxms.Count - 1, shell_utils.pxm_compare_id_low);
                    sdk_dict[item.Key] = pxms;
                }
            }
            if (apk_dict.Count > 0) {
                for (int apk_pos = 0; apk_pos < apk_dict.Count; apk_pos++)
                {
                    KeyValuePair<string, List<public_xml_model>> item = apk_dict.ElementAt(apk_pos);
                    List<public_xml_model> pxms = item.Value;
                    utils.quick_sort_fast<public_xml_model>(ref pxms, 0, pxms.Count - 1, shell_utils.pxm_compare_id_low);
                    apk_dict[item.Key] = pxms;
                }
            }
            //获取ID-HIGH最大值
            int sdk_id_high_max = 0;
            int apk_id_high_max = 0;
            if (sdk_dict.Count > 0)
            {
                List<public_xml_model> pxms = new List<public_xml_model>();
                for (int sort_pos = 0; sort_pos < sdk_dict.Count; sort_pos++)
                {
                    pxms.Add(sdk_dict.ElementAt(sort_pos).Value[0]);
                }
                utils.quick_sort_fast<public_xml_model>(ref pxms, 0, pxms.Count - 1, shell_utils.pxm_compare_id_high);
                pxms.Reverse();
                sdk_id_high_max = pxms[0].id_high;
            }
            if (apk_dict.Count > 0)
            {
                List<public_xml_model> pxms = new List<public_xml_model>();
                for (int m = 0; m < apk_dict.Count; m++)
                {
                    pxms.Add(apk_dict.ElementAt(m).Value[0]);
                }
                utils.quick_sort_fast<public_xml_model>(ref pxms, 0, pxms.Count - 1, shell_utils.pxm_compare_id_high);
                pxms.Reverse();
                apk_id_high_max = pxms[0].id_high;
            }

            //合并public
            if (sdk_dict.Count > 0) {
                int insert_id_high = apk_id_high_max;
                int insert_id_low = 0;
                foreach (string sdk_type in sdk_dict.Keys) {
                    if (apk_dict.ContainsKey(sdk_type))
                    {
                        foreach (public_xml_model pxm in sdk_dict[sdk_type]) {
                            if (!shell_utils.exist_pxm_name(apk_dict[sdk_type], pxm.name)) {
                                public_xml_model insert_pxm = pxm;
                                insert_pxm.id_high = apk_dict[sdk_type].Last().id_high;
                                insert_pxm.id_low = apk_dict[sdk_type].Last().id_low + 1;
                                insert_pxm.id = string.Format("0x{0:X4}", insert_pxm.id_high).ToLower() + string.Format("{0:X4}", insert_pxm.id_low).ToLower();
                                apk_dict[sdk_type].Add(insert_pxm);
                            }
                        }
                    }
                    else {
                        insert_id_high++;
                        insert_id_low = 0;
                        apk_dict[sdk_type] = new List<public_xml_model>();
                        foreach (public_xml_model pxm in sdk_dict[sdk_type])
                        {
                            public_xml_model insert_pxm = pxm;
                            insert_pxm.id_high = insert_id_high;
                            insert_pxm.id_low = insert_id_low++;
                            insert_pxm.id = string.Format("0x{0:X4}", insert_pxm.id_high).ToLower() + string.Format("{0:X4}", insert_pxm.id_low).ToLower();
                            apk_dict[sdk_type].Add(insert_pxm);
                        }
                    }
                }
            }
            //重写public.xml
            if (apk_dict.Count > 0)
            {
                List<public_xml_model> pxms = new List<public_xml_model>();
                for (int m = 0; m < apk_dict.Count; m++)
                {
                    pxms.Add(apk_dict.ElementAt(m).Value[0]);
                }
                utils.quick_sort_fast<public_xml_model>(ref pxms, 0, pxms.Count - 1, shell_utils.pxm_compare_id_high);
                apk_public_doc.DocumentElement.RemoveAll();
                foreach (public_xml_model pxm in pxms) {
                    foreach (public_xml_model insert_pxm in apk_dict[pxm.type]) {
                        XmlElement insert_element = apk_public_doc.CreateElement("public");
                        insert_element.SetAttribute("type", insert_pxm.type);
                        insert_element.SetAttribute("name", insert_pxm.name);
                        insert_element.SetAttribute("id", insert_pxm.id);
                        apk_public_doc.DocumentElement.AppendChild(insert_element);
                    }
                }
                apk_public_doc.Save(apk_public);
            }
            //合并res文件
            List<string> sdk_folders = Directory.GetDirectories(m_sdkinfo.in_res).ToList();
            List<string> apk_folders = Directory.GetDirectories(m_apkinfo.in_res).ToList();
            string sdk_folder_name = string.Empty;
            string apk_file_name = string.Empty;
            foreach (string sdk_folder in sdk_folders) {
                sdk_folder_name = (new DirectoryInfo(sdk_folder)).Name;
                if (sdk_folder_name.IndexOf("values") != -1)
                    continue;
                if (!Directory.Exists(m_apkinfo.in_res + @"\" + sdk_folder_name))
                {
                    utils.copy_folder(sdk_folder, m_apkinfo.in_res);
                }
                else {
                    List<string> sdk_folder_files = Directory.GetFiles(sdk_folder).ToList();
                    foreach (string sdk_folder_file in sdk_folder_files) {
                        apk_file_name = m_apkinfo.in_res + @"\" + sdk_folder_name + @"\" + Path.GetFileName(sdk_folder_file);
                        if (!File.Exists(apk_file_name)) {
                            File.Copy(sdk_folder_file, apk_file_name);
                        }
                    }
                }
            }
            //合并values
            List<string> sdk_value_files = Directory.GetFiles(m_sdkinfo.in_res+@"\values").ToList();
            List<string> apk_value_files = Directory.GetFiles(m_apkinfo.in_res+@"\values").ToList();
            if (sdk_value_files.Count > 0) {
                foreach (string sdk_value_file in sdk_value_files) {
                    apk_file_name = m_apkinfo.in_res + @"\values\" + Path.GetFileName(sdk_value_file);
                    if (Path.GetFileNameWithoutExtension(sdk_value_file) == "public") continue;
                    if (!File.Exists(apk_file_name))
                    {
                        File.Copy(sdk_value_file, apk_file_name);
                    }
                    else {
                        XmlDocument sdk_values_doc = new XmlDocument();
                        sdk_values_doc.Load(sdk_value_file);
                        XmlDocument apk_values_doc = new XmlDocument();
                        apk_values_doc.Load(apk_file_name);
                        if (sdk_values_doc.DocumentElement.ChildNodes.Count > 0) {
                            bool flag = false;
                            foreach (XmlNode sdk_values_node in sdk_values_doc.DocumentElement.ChildNodes)
                            {
                                flag = false;
                                foreach (XmlNode apk_values_node in apk_values_doc.DocumentElement.ChildNodes)
                                {
                                    if (apk_values_node.Attributes["name"].Value == sdk_values_node.Attributes["name"].Value)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    apk_values_doc.DocumentElement.AppendChild(apk_values_doc.ImportNode(sdk_values_node, true));
                                }
                            }
                            apk_values_doc.Save(apk_file_name);
                        }

                    }
                }
            }
        }

        public virtual void MergeSmali(List<string> copy_folders, List<string> copy_files)
        {
            //合并R.smali（从新合并的public.xml中合并）
            do
            {
                if (!Directory.Exists(m_sdkinfo.in_res))
                    break;
                //寻找R.smali文件和对应的拓展文件
                List<string> sdk_smalis = new List<string>();
                string sdk_R = m_sdkinfo.in_smali + @"\" + m_sdkinfo.PrePackageName.Replace(".",@"\") + @"\R.smali";
                if(!File.Exists(sdk_R))
                    throw new Exception("sdk has muiti R.smali");
                string sdk_R_class = @"L" + Path.GetDirectoryName(sdk_R).Replace(m_sdkinfo.in_smali, "").Replace(@"\", @"/").TrimStart('/');

                List<string> apk_smalis = new List<string>();
                string dex_class_smali_folder = m_apkinfo.in_smali;
                string apk_R = string.Empty;
                utils.get_files_from_folder(m_apkinfo.in_smali, ref apk_smalis, "R.smali");
                if (apk_smalis.Count == 0) {
                    if (Directory.Exists(m_apkinfo.in_smali_classes2)) {
                        utils.get_files_from_folder(m_apkinfo.in_smali_classes2, ref apk_smalis, "R.smali");
                        dex_class_smali_folder = m_apkinfo.in_smali_classes2;
                    }
                }
                foreach (string key in apk_smalis)
                {
                    string tempname = Path.GetDirectoryName(key).Replace(dex_class_smali_folder + @"\", "").Replace(@"\", ".");
                    if (m_apkinfo.settings.MainActivity.IndexOf(tempname) != -1 || m_apkinfo.settings.PackageName == tempname || apk_smalis.Count == 1)
                    {
                        apk_R = key;
                        break;
                    }
                }
                if (apk_R != string.Empty)
                {
                    utils.get_files_from_folder(Path.GetDirectoryName(apk_R), ref apk_smalis, "R$", "*.smali");
                    if (apk_smalis.Count > 0)
                    {
                        do
                        {
                            bool rm_flag = false;
                            for (int m = 0; m < apk_smalis.Count; m++)
                            {
                                if (Path.GetFileName(apk_smalis[m]).Substring(0, 2) != "R$")
                                {
                                    apk_smalis.RemoveAt(m);
                                    rm_flag = true;
                                    break;
                                }
                            }
                            if (rm_flag == false)
                                break;
                        } while (true);
                    }
                }
                else
                {
                    throw new Exception("apk has muiti R.smali");
                }
                string apk_R_class = @"L" + Path.GetDirectoryName(apk_R).Replace(dex_class_smali_folder, "").Replace(@"\", @"/").TrimStart('/');
                //搜集sdk中R.smali和apk中R.smali中的type
                Encoding enc = null;
                enc = TxtFileEncoder.GetEncoding(sdk_R);
                List<string> sdk_R_lines = File.ReadAllText(sdk_R, enc).Replace("\r\n", "*").Split('*').ToList();
                enc = TxtFileEncoder.GetEncoding(apk_R);
                List<string> apk_R_lines = File.ReadAllText(apk_R, enc).Replace("\r\n", "*").Split('*').ToList();
                List<string> sdk_R_class_types = new List<string>();
                List<string> apk_R_class_types = new List<string>();

                string sdk_public = m_sdkinfo.in_res + @"\values\public.xml";
                SortedDictionary<string, List<public_xml_model>> sdk_dict = new SortedDictionary<string, List<public_xml_model>>();
                XmlDocument sdk_public_doc = new XmlDocument();
                sdk_public_doc.Load(sdk_public);
                XmlNodeList sdk_nodes = sdk_public_doc.DocumentElement.ChildNodes;
                if (sdk_nodes.Count > 0)
                {
                    foreach (XmlNode node in sdk_nodes)
                    {
                        public_xml_model pxm = new public_xml_model();
                        pxm.type = node.Attributes["type"].Value;
                        pxm.name = node.Attributes["name"].Value;
                        pxm.id = node.Attributes["id"].Value;
                        pxm.id_high = Convert.ToInt32(pxm.id.Substring(2, 4), 16);
                        pxm.id_low = Convert.ToInt32(pxm.id.Substring(6, 4), 16);
                        if (!sdk_dict.ContainsKey(pxm.type))
                            sdk_dict.Add(pxm.type, new List<public_xml_model>());
                        sdk_dict[pxm.type].Add(pxm);
                    }
                }
                foreach (string sdk_key in sdk_dict.Keys)
                {
                    sdk_R_class_types.Add(sdk_key);
                }
                int insert_pos = -1;
                foreach (string apk_R_line in apk_R_lines)
                {
                    if (apk_R_line.IndexOf(apk_R_class + @"/R$") != -1)
                    {
                        if (insert_pos == -1)
                            insert_pos = apk_R_lines.IndexOf(apk_R_line);
                        apk_R_class_types.Add(apk_R_line.Replace(" ", "").Replace(@";", "").Replace(@",", "").Replace(apk_R_class + @"/R$", ""));
                    }
                }
                //向apk中R.smali添加新的type，并复制新的R$type.smali
                string insert_line = string.Empty;
                string copy_content = string.Empty;
                string copy_from_path = string.Empty;
                string copy_to_path = string.Empty;
                foreach (string sdk_type in sdk_R_class_types)
                {
                    copy_from_path = Path.GetDirectoryName(sdk_R) + @"\R$" + sdk_type + @".smali";
                    copy_to_path = Path.GetDirectoryName(apk_R) + @"\R$" + sdk_type + @".smali";
                    if (!File.Exists(copy_to_path))
                    {
                        File.Copy(copy_from_path, copy_to_path);
                        enc = TxtFileEncoder.GetEncoding(copy_to_path);
                        copy_content = File.ReadAllText(copy_to_path, enc);
                        copy_content = copy_content.Replace(sdk_R_class, apk_R_class);
                        File.WriteAllText(copy_to_path, copy_content, enc);
                    }
                    if (apk_R_class_types.Count == 0 || insert_pos == -1)
                        break;
                    if (apk_R_class_types.IndexOf(sdk_type) == -1)
                    {
                        insert_line = "        " + apk_R_class + @"/R$" + sdk_type + @";,";
                        apk_R_lines.Insert(insert_pos, insert_line);
                    }
                }
                string apk_R_new_content = string.Empty;
                for (int i = 0; i < apk_R_lines.Count; i++)
                {
                    apk_R_new_content += apk_R_lines[i] + "\r\n";
                }
                enc = TxtFileEncoder.GetEncoding(apk_R);
                File.WriteAllText(apk_R, apk_R_new_content, enc);
                //根据public.xml来调整R.smali
                string apk_public = m_apkinfo.in_res + @"\values\public.xml";
                SortedDictionary<string, List<public_xml_model>> apk_dict = new SortedDictionary<string, List<public_xml_model>>();
                XmlDocument apk_public_doc = new XmlDocument();
                apk_public_doc.Load(apk_public);
                XmlNodeList apk_nodes = apk_public_doc.DocumentElement.ChildNodes;
                if (apk_nodes.Count > 0)
                {
                    foreach (XmlNode node in apk_nodes)
                    {
                        public_xml_model pxm = new public_xml_model();
                        pxm.type = node.Attributes["type"].Value;
                        pxm.name = node.Attributes["name"].Value;
                        pxm.id = node.Attributes["id"].Value;
                        pxm.id_high = Convert.ToInt32(pxm.id.Substring(2, 4), 16);
                        pxm.id_low = Convert.ToInt32(pxm.id.Substring(6, 4), 16);
                        if (!apk_dict.ContainsKey(pxm.type))
                            apk_dict.Add(pxm.type, new List<public_xml_model>());
                        apk_dict[pxm.type].Add(pxm);
                    }
                }
                if (apk_dict.Count > 0)
                {
                    string apk_R_type_path = string.Empty;
                    string apk_R_type_content = string.Empty;
                    int apk_R_type_start_pos = -1;
                    int apk_R_type_end_pos = -1;
                    List<string> apk_R_type_lines = new List<string>();
                    string apk_R_type_insert = string.Empty;
                    List<public_xml_model> apk_R_type_pxms = null;
                    foreach (string apk_type in apk_dict.Keys)
                    {
                        apk_R_type_path = Path.GetDirectoryName(apk_R) + @"\R$" + apk_type + @".smali";
                        if (!File.Exists(apk_R_type_path)) continue;
                        enc = TxtFileEncoder.GetEncoding(apk_R_type_path);
                        apk_R_type_content = File.ReadAllText(apk_R_type_path, enc);
                        apk_R_type_lines = apk_R_type_content.Replace("\r\n", "*").Split('*').ToList();
                        apk_R_type_start_pos = apk_R_type_lines.IndexOf(@"# static fields") + 1;
                        apk_R_type_end_pos = apk_R_type_lines.IndexOf(@"# direct methods");
                        apk_R_type_lines.RemoveRange(apk_R_type_start_pos, apk_R_type_end_pos - apk_R_type_start_pos);
                        apk_R_type_pxms = apk_dict[apk_type];
                        apk_R_type_pxms.Reverse();
                        foreach (public_xml_model pxm in apk_R_type_pxms)
                        {
                            apk_R_type_insert = @".field public static final " + pxm.name.Replace(".", "_") + @":I = " + pxm.id;
                            apk_R_type_lines.Insert(apk_R_type_start_pos, apk_R_type_insert);
                            apk_R_type_lines.Insert(apk_R_type_start_pos + 1, "");
                        }
                        apk_R_type_end_pos = apk_R_type_lines.IndexOf(@"# direct methods");
                        apk_R_type_lines.Insert(apk_R_type_end_pos, "");
                        apk_R_type_content = string.Empty;
                        foreach (string apk_R_type_line in apk_R_type_lines)
                        {
                            apk_R_type_content += apk_R_type_line + "\r\n";
                        }
                        File.WriteAllText(apk_R_type_path, apk_R_type_content, enc);
                    }
                }
            } while (false);
            //合并smali文件夹和文件
            if (copy_folders != null)
            {
                string from_folder = string.Empty;
                string to_folder = string.Empty;
                foreach (string folder in copy_folders)
                {
                    from_folder = m_sdkinfo.in_smali + @"\" + folder;
                    to_folder = m_apkinfo.in_smali;
                    if (Directory.Exists(from_folder))
                    {
                        List<string> split_folders = folder.Split('\\').ToList();
                        for (int i = 0; i < split_folders.Count; i++)
                        {
                            if ((i + 1) == split_folders.Count)
                            {
                                utils.copy_folder(from_folder, to_folder);
                            }
                            else
                            {
                                to_folder += @"\" + split_folders[i];
                                if (!Directory.Exists(to_folder))
                                    Directory.CreateDirectory(to_folder);
                            }
                        }
                    }
                }
            }
            if (copy_files != null) {
                try
                {
                    string from_file = string.Empty;
                    string to_file = string.Empty;
                    foreach (string file in copy_files)
                    {
                        from_file = m_sdkinfo.in_smali + @"\" + file;
                        to_file = m_apkinfo.in_smali;
                        if (File.Exists(from_file))
                        {
                            List<string> split_folders = file.Split('\\').ToList();
                            for (int i = 0; i < split_folders.Count; i++)
                            {
                                to_file += @"\" + split_folders[i];
                                if ((i + 1) == split_folders.Count)
                                {
                                    if (File.Exists(to_file))
                                        File.Delete(to_file);
                                    File.Copy(from_file, to_file);
                                }
                                else
                                {
                                    if (!Directory.Exists(to_file))
                                        Directory.CreateDirectory(to_file);
                                }
                            }
                        }
                    }
                }
                catch (Exception e) {
                    e.ToString();
                }
            }
            //植入smali代码
            //需要在每个游戏合并类中自定义合并代码
        }

        /// <summary>
        /// 植入smali代码
        /// </summary>
        /// <param name="type"></param>
        public void InsertSmali(SmaliInsertType type,string insert_activity,string insert_application)
        {
            
        }

        /// <summary>
        /// 修改渠道号
        /// </summary>
        /// <param name="channel"></param>
        public virtual void ReChannel(string channel) {

        }
    }
}
