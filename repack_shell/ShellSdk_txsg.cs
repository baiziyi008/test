using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_shell
{
    public class ShellSdk_txsg : ShellSdk
    {
        public new void InsertSmali(SmaliInsertType type,string insert_activity, string insert_application)
        {
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
                        insert_smali = "invoke-static {p0}, Lcom/mytx/util/MyUtils;->txOnCreate(Landroid/content/Context;)V";
                        ret = shell_utils.insert_smali_code(ref MainActivityContent,
                            SmaliInsertFunctionType.func_pos_onCreate,
                            shell_env.insert_smali_pos_return,
                            insert_smali,
                            InsertPosType.insert_before);
                        if (!ret)
                        {
                            shell_utils.insert_smali_function(ref MainActivityContent, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onCreate], insert_smali);
                        }

                        //插入onDestroy代码
                        insert_smali = "invoke-static {p0}, Lcom/mytx/util/MyUtils;->txonDestroy(Landroid/content/Context;)V";
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

                        //插入Application.smali代码
                        XmlDocument apk_doc = new XmlDocument();
                        apk_doc.Load(m_apkinfo.AndroidManifestPath);
                        XmlElement apk_application_node = (XmlElement)apk_doc.DocumentElement.SelectSingleNode("/manifest/application");
                        if (apk_application_node.Attributes["android:name"] != null)
                        {
                            string application_smali = apk_application_node.Attributes["android:name"].Value;
                            if (application_smali[0] == '.')
                            {
                                application_smali = m_apkinfo.settings.PackageName + application_smali;
                            }
                            string application_smali_path = m_apkinfo.in_smali + @"\" + application_smali.Replace(".", "\\") + ".smali";
                            if (File.Exists(application_smali_path))
                            {
                                enc = TxtFileEncoder.GetEncoding(application_smali_path);
                                string application_smali_content = File.ReadAllText(application_smali_path, enc);
                                insert_smali = "invoke-static {p0}, Lcom/mytx/util/MyUtils;->initForApplication(Landroid/content/Context;)V";

                                ret = shell_utils.insert_smali_code(ref application_smali_content,
                                    SmaliInsertFunctionType.func_pos_onCreate_app,
                                    shell_env.insert_smali_pos_return,
                                    insert_smali,
                                    InsertPosType.insert_before);
                                if (!ret)
                                {
                                    shell_utils.insert_smali_function(ref application_smali_content, shell_env.insert_smali_function_dict[SmaliInsertFunctionType.func_pos_onCreate_app], insert_smali);
                                }
                                File.WriteAllText(application_smali_path, application_smali_content, enc);
                            }
                        }
                        else
                        {
                            //
                            XmlDocument sdk_doc = new XmlDocument();
                            sdk_doc.Load(m_sdkinfo.AndroidManifestPath);
                            XmlElement sdk_application_node = (XmlElement)sdk_doc.DocumentElement.SelectSingleNode("/manifest/application");
                            XmlAttribute application_name_attr = sdk_application_node.Attributes["android:name"];
                            apk_application_node.Attributes.Append((XmlAttribute)apk_doc.ImportNode(application_name_attr, true));
                            apk_doc.Save(m_apkinfo.AndroidManifestPath);
                        }
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

        public void MergeSmali()
        {
            List<string> copy_folders = new List<string>();
            //
            copy_folders.Add(@"u\aly");
            copy_folders.Add(@"com\a\a\a");
            copy_folders.Add(@"com\mytx");
            copy_folders.Add(@"com\qq\taf\jce");
            copy_folders.Add(@"com\tencent");
            copy_folders.Add(@"com\tools\game");
            copy_folders.Add(@"com\umeng\analytics");
            copy_folders.Add(@"tmsdk");
            copy_folders.Add(@"tmsdkobf");
            copy_folders.Add(@"android\support\v4");
            List<string> copy_files = new List<string>();
            copy_files.Add(@"a\a\a.smali");
            copy_files.Add(@"MTT\ThirdAppInfoNew.smali");
            copy_files.Add(@"com\sdk_preload\init_sdk_txsg_application.smali");
            base.MergeSmali(copy_folders, copy_files);
        }
    }
}
