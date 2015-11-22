using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellSdk_3gppgame : ShellSdk
    {
        public new void InsertSmali(SmaliInsertType type, string insert_activity, string insert_application)
        {
            base.InsertSmali(type, insert_activity, insert_activity);

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
                        insert_smali = "invoke-static {p0}, Lcom/sdk_preload/init_sdk_3gpp;->Init3gpp(Landroid/content/Context;)V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onCreate,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onCreate], insert_smali);
                        }
                        //插入onPause代码
                        insert_smali = "invoke-static {}, Lcom/sdk_preload/init_sdk_3gpp;->onPause()V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onPause,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onPause], insert_smali);
                        }
                        //插入onResume代码
                        insert_smali = "invoke-static {}, Lcom/sdk_preload/init_sdk_3gpp;->onResume()V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onResume,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onResume], insert_smali);
                        }

                        //插入onDestroy代码
                        insert_smali = "invoke-static {}, Lcom/sdk_preload/init_sdk_3gpp;->onDestroy()V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onDestroy,
                            shell_env.insert_smali_pos_smali_begin,
                            insert_smali,
                            InsertPosType.insert_after);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onDestroy], insert_smali);
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

        public void MergeAndroidManifest(string appkey, string pid, string cid)
        {
            base.MergeAndroidManifest();
            //替换包名
            string AgentString = "#PACKAGE_NAME#";
            Encoding enc = TxtFileEncoder.GetEncoding(m_apkinfo.AndroidManifestPath);
            string AndroidManifestContent = File.ReadAllText(m_apkinfo.AndroidManifestPath, enc);
            AndroidManifestContent = AndroidManifestContent.Replace(AgentString, m_apkinfo.settings.PackageName);
            File.WriteAllText(m_apkinfo.AndroidManifestPath, AndroidManifestContent, enc);
            //
            //填写appkey
            XmlDocument apk_doc = new XmlDocument();
            apk_doc.Load(m_apkinfo.AndroidManifestPath);
            XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
            XmlNodeList apk_nodeApps = apk_application_node.ChildNodes;
            for (int i = 0; i < apk_nodeApps.Count; i++)
            {
                if (apk_nodeApps[i].Attributes["android:name"] == null) continue;
                if (apk_nodeApps[i].Attributes["android:name"].Value == "JPUSH_APPKEY")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = appkey;
                    continue;
                }

                if (apk_nodeApps[i].Attributes["android:name"].Value == "adp_pid")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = pid;
                    continue;
                }

                if (apk_nodeApps[i].Attributes["android:name"].Value == "adp_cid")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = cid;
                    continue;
                }
            }
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }

        public void MergeSmali()
        {
            List<string> copy_folders = new List<string>();
            copy_folders.Add(@"MTT");
            copy_folders.Add(@"cn\jpush");
            copy_folders.Add(@"com\adog");
            copy_folders.Add(@"com\bfac");
            copy_folders.Add(@"com\common");
            copy_folders.Add(@"com\tencent\tmsecurelite");
            copy_folders.Add(@"com\xgdata");
            copy_folders.Add(@"android\support\v4");
            List<string> copy_files = new List<string>();
            copy_files.Add(@"com\sdk_preload\init_sdk_3gpp.smali");
            base.MergeSmali(copy_folders, copy_files);
        }
    }
}
