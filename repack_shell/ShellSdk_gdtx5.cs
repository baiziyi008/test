using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    /// <summary>
    /// 广点通SDK
    /// </summary>
    public class ShellSdk_gdtx5 : ShellSdk
    {
        public new void InsertSmali(SmaliInsertType type, string insert_activity, string insert_application) {
            base.InsertSmali(type,insert_activity,insert_application);

            Encoding enc = null;
            //
            switch (type)
            {
                case SmaliInsertType.InsertSmali:
                    {
                        string MainActivity = string.Empty;
                        if (insert_activity != string.Empty)
                            MainActivity = insert_activity;
                        else
                            MainActivity = m_apkinfo.settings.MainActivity;
                        MainActivity = m_apkinfo.in_smali + @"\" + MainActivity.Replace(@".", @"\") + ".smali";
                        enc = TxtFileEncoder.GetEncoding(MainActivity);
                        string MainActivityContent = File.ReadAllText(MainActivity, enc);

                        string insert_smali = string.Empty;
                        bool ret = false;
                        //插入onCreate代码
                        insert_smali = "invoke-static {p0}, Lcom/sdk_preload/init_sdk_gdt;->init(Landroid/app/Activity;)V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onCreate,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onCreate], insert_smali);
                        }
                        File.WriteAllText(MainActivity, MainActivityContent, enc);
                    }
                    break;
                case SmaliInsertType.Inheritance:
                    {

                    }
                    break;
                case SmaliInsertType.Boot:
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appkey">x5内核广点通appkey</param>
        public void MergeAndroidManifest(string appkey,string appid,string insertid,string startid) {
            base.MergeAndroidManifest();
            //
            //填写appkey
            XmlDocument apk_doc = new XmlDocument();
            apk_doc.Load(m_apkinfo.AndroidManifestPath);
            XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
            XmlNodeList apk_nodeApps = apk_application_node.ChildNodes;
            for (int i = 0; i < apk_nodeApps.Count; i++)
            {
                if (apk_nodeApps[i].Attributes["android:name"] == null) continue;
                if (apk_nodeApps[i].Attributes["android:name"].Value == "QBSDKAppKey")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = appkey;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "SDK_GDT_APPID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = "A" + appid;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "SDK_GDT_INSERTID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = "A" + insertid;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "SDK_GDT_STARTID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = "A" + startid;
                    continue;
                }
            }
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid">广点通应用ID</param>
        /// <param name="adid">广点通广告位ID</param>
        public void MergeSmali() {
            List<string> copy_folders = new List<string>();
            copy_folders.Add(@"com\qq\e");
            copy_folders.Add(@"com\tencent\smtt");
            copy_folders.Add(@"com\tencent\tbs");
            copy_folders.Add(@"android\support\v4");
            List<string> copy_files = new List<string>();
            copy_files.Add(@"MTT\ThirdAppInfoNew.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_gdt.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_gdt$1.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_gdt$2.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_gdt$3.smali");
            base.MergeSmali(copy_folders, copy_files);
        }
    }
}
