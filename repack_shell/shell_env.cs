using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    /// <summary>
    /// 插入位置方法类型
    /// </summary>
    public enum SmaliInsertFunctionType
    {
        func_pos_onCreate           = 0x00,
        func_pos_onPause            = 0x01,
        func_pos_onResume           = 0x02,
        func_pos_onDestroy          = 0x03,
        func_pos_onBackPressed      = 0x04,
        func_pos_onRestart          = 0x05,
        func_pos_onCreate_app       = 0x06,
        func_pos_onStop             = 0x07
    }

    /// <summary>
    /// 插入位置类型
    /// </summary>
    public enum InsertPosType {
        insert_before   = 0x00,
        insert_after    = 0x01
    }

    /// <summary>
    /// SDK类型
    /// </summary>
    public enum SdkType {
        umeng_aly,
        umeng_game,
        umeng_push,
        hao123,
        g3pp,
        g3ppgame,
        gdt,
        gdtx5,
        txsg,
        txpush,
        baidussp
    }

    /// <summary>
    /// 环境相关
    /// </summary>
    public class shell_env
    {
        /// <summary>
        /// SDK和类型名称对应关系
        /// </summary>
        public static Dictionary<SdkType, string> sdk_class_dict = new Dictionary<SdkType, string>() {
            {SdkType.umeng_aly,     "ShellSdk_umeng_aly"        },
            {SdkType.umeng_game,    "ShellSdk_umeng_game"       },
            {SdkType.umeng_push,    "ShellSdk_umeng_push"       },
            {SdkType.hao123,        "ShellSdk_hao123"           },
            {SdkType.g3pp,          "ShellSdk_3gpp"             },
            {SdkType.g3ppgame,      "ShellSdk_3gppgame"         },
            {SdkType.gdt,           "ShellSdk_gdt"              },
            {SdkType.gdtx5,         "ShellSdk_gdtx5"            },
            {SdkType.txsg,          "ShellSdk_txsg"             },
            {SdkType.txpush,        "ShellSdk_txpush"           },
            {SdkType.baidussp,      "ShellSdk_baidussp"         }
        };

        /// <summary>
        /// 插入位置方法类型 和 对应查找字符串 对应关系
        /// </summary>
        public static Dictionary<SmaliInsertFunctionType, string> insert_smali_function_title_dict = 
            new Dictionary<SmaliInsertFunctionType, string>() {
            { SmaliInsertFunctionType.func_pos_onCreate,        ".method public onCreate(Landroid/os/Bundle;)V"     },
            { SmaliInsertFunctionType.func_pos_onPause,         ".method public onPause()V"                         },
            { SmaliInsertFunctionType.func_pos_onResume,        ".method public onResume()V"                        },
            { SmaliInsertFunctionType.func_pos_onDestroy,       ".method public onDestroy()V"                       },
            { SmaliInsertFunctionType.func_pos_onBackPressed,   ".method public onBackPressed()V"                   },
            { SmaliInsertFunctionType.func_pos_onRestart,       ".method public onRestart()V"                       },
            { SmaliInsertFunctionType.func_pos_onCreate_app,    ".method public onCreate()V"                        },
            { SmaliInsertFunctionType.func_pos_onStop,          ".method public onStop()V"                          }
        };

        /// <summary>
        /// 插入位置方法类型 和 对应完整方法 对应关系
        /// </summary>
        public static Dictionary<SmaliInsertFunctionType, string> insert_smali_function_dict =
            new Dictionary<SmaliInsertFunctionType, string>() {
            { SmaliInsertFunctionType.func_pos_onCreate,
                    ".method public onCreate(Landroid/os/Bundle;)V\r\n" +
                    "    .locals 0\r\n" +
                    "    .param p1, \"savedInstanceState\"    # Landroid/os/Bundle;\r\n\r\n"+
                    "    .prologue\r\n" +
                    "    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onPause,
                    ".method public onPause()V\r\n" +
                    "    .locals 0\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onPause()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onResume,
                    ".method public onResume()V\r\n" +
                    "    .locals 0\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onResume()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onDestroy,
                    ".method public onDestroy()V\r\n" +
                    "    .locals 0\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onDestroy()V\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onBackPressed,
                    ".method public onBackPressed()V\r\n" +
                    "    .locals 0\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onBackPressed()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onRestart,
                    ".method public onRestart()V\r\n" +
                    "    .locals 0\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onRestart()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onCreate_app,
                    ".method public onCreate()V\r\n" +
                    "    .locals 1\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Application;->onCreate()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                },
            { SmaliInsertFunctionType.func_pos_onStop,
                    ".method public onStop()V\r\n" +
                    "    .locals 1\r\n\r\n" +
                    "    .prologue\r\n" +
                    "    invoke-super {p0}, Landroid/app/Activity;->onStop()V\r\n\r\n" +
                    "    #INSERT_SMALI_CODE#\r\n\r\n" +
                    "    return-void\r\n" +
                    ".end method\r\n"
                }
        };

        public static string insert_smali_find_end_function = ".end method";
        public static string insert_smali_pos_return = "return-void";
        public static string insert_smali_pos_onDestroy = "invoke-super {p0}, Landroid/app/Activity;->onDestroy()V";
        public static string insert_smali_pos_smali_begin = ".prologue";
    }
}
