using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellSdk_baidussp : ShellSdk
    {
        public new void InsertSmali(SmaliInsertType type, string insert_activity, string insert_application)
        {
            base.InsertSmali(type, insert_activity, insert_application);

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
                        insert_smali = "invoke-static {p0}, Lcom/sdk_preload/init_sdk_baidussp;->init(Landroid/app/Activity;)V";
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

        public void MergeAndroidManifest(string appid, string insertid, string startid)
        {
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
                if (apk_nodeApps[i].Attributes["android:name"].Value == "BaiduMobAd_APP_ID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = appid;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "BaiduMobAd_INSERT_ID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = "A" + insertid;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "BaiduMobAd_START_ID")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = "A" + startid;
                    continue;
                }
            }
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }

        public void MergeSmali()
        {
            List<string> copy_folders = new List<string>();
            copy_folders.Add(@"com\baidu\mobads");
            copy_folders.Add(@"android\support\v4");
            List<string> copy_files = new List<string>();
            copy_files.Add(@"com\sdk_preload\init_sdk_baidussp.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_baidussp$1.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_baidussp$2.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_baidussp$3.smali");
            base.MergeSmali(copy_folders, copy_files);
        }
    }
}
