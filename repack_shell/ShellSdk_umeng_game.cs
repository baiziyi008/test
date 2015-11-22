using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellSdk_umeng_game : ShellSdk
    {
        /// <summary>
        /// 合并SDK初始化代码
        /// </summary>
        /// <param name="type"></param>
        public new void InsertSmali(SmaliInsertType type, string insert_activity, string insert_application)
        {
            base.InsertSmali(type,insert_activity,insert_application);
            //
            Encoding enc = null;

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
                        //插入onPause代码
                        insert_smali = "invoke-static {p0}, Lcom/umeng/analytics/game/UMGameAgent;->onPause(Landroid/content/Context;)V";
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
                        insert_smali = "invoke-static {p0}, Lcom/umeng/analytics/game/UMGameAgent;->onResume(Landroid/content/Context;)V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onResume,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onResume], insert_smali);
                        }

                        //插入onCreate代码
                        insert_smali = "invoke-static {p0}, Lcom/umeng/analytics/game/UMGameAgent;->init(Landroid/content/Context;)V";
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
        /// 合并AndroidManifest
        /// </summary>
        /// <param name="umeng_appkey">友盟AppKey</param>
        /// <param name="umeng_channel">友盟渠道号</param>
        public void MergeAndroidManifest(string umeng_appkey, string umeng_channel)
        {
            base.MergeAndroidManifest();
            //
            //填写UMengKey和ChannelID
            XmlDocument apk_doc = new XmlDocument();
            apk_doc.Load(m_apkinfo.AndroidManifestPath);
            XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
            XmlNodeList apk_nodeApps = apk_application_node.ChildNodes;
            for (int i = 0; i < apk_nodeApps.Count; i++)
            {
                if (apk_nodeApps[i].Attributes["android:name"] == null) continue;
                if (apk_nodeApps[i].Attributes["android:name"].Value == "UMENG_APPKEY")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = umeng_appkey;
                    continue;
                }
                if (apk_nodeApps[i].Attributes["android:name"].Value == "UMENG_CHANNEL")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = umeng_channel;
                }
            }
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }

        public void MergeSmali()
        {
            List<string> copy_folders = new List<string>();
            copy_folders.Add(@"u\aly");
            copy_folders.Add(@"com\umeng\analytics");
            copy_folders.Add(@"android\support\v4");
            base.MergeSmali(copy_folders, null);
        }

        public new void ReChannel(string channel)
        {
            //填写UMengKey和ChannelID
            XmlDocument apk_doc = new XmlDocument();
            apk_doc.Load(m_apkinfo.AndroidManifestPath);
            XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
            XmlNodeList apk_nodeApps = apk_application_node.ChildNodes;
            for (int i = 0; i < apk_nodeApps.Count; i++)
            {
                if (apk_nodeApps[i].Attributes["android:name"] == null) continue;
                if (apk_nodeApps[i].Attributes["android:name"].Value == "UMENG_CHANNEL")
                {
                    apk_nodeApps[i].Attributes["android:value"].Value = channel;
                    break;
                }
            }
            apk_doc.Save(m_apkinfo.AndroidManifestPath);
        }
    }
}
