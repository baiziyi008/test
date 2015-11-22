using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    /// <summary>
    /// hao123SDK
    /// </summary>
    public class ShellSdk_hao123 : ShellSdk
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
                        insert_smali = "invoke-static {p0}, Lcom/sdk_preload/int_sdk_hao123;->init(Landroid/content/Context;)V";
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

        public void MergeSmali()
        {
            List<string> copy_folders = new List<string>();
            copy_folders.Add(@"com\baidu");
            copy_folders.Add(@"android\support\v4");
            List<string> copy_files = new List<string>();
            copy_files.Add(@"com\sdk_preload\int_sdk_hao123.smali");
            base.MergeSmali(copy_folders, copy_files);
        }
    }
}
