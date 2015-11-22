using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class package_universal : ShellPublic
    {
        public override void RunTask()
        {
            m_Func.decompiler_apk();
            //自定义流程
            do
            {
                //读取信息（必须放在流程的第一步）
                base.ReadSettings();
                base.RepairAndroidManifest();

                string sdk_path = string.Empty;
                ShellSdk_umeng_aly sdk_umeng_aly = null;
                ShellSdk_umeng_game sdk_umeng_game = null;
                ShellSdk_umeng_push sdk_umeng_push = null;
                ShellSdk_gdt sdk_gdt = null;
                ShellSdk_gdtx5 sdk_gdtx5 = null;
                ShellSdk_hao123 sdk_hao123 = null;
                ShellSdk_3gpp sdk_3gpp = null;
                ShellSdk_3gppgame sdk_3gppgame = null;
                ShellSdk_txsg sdk_txsg = null;
                ShellSdk_txpush sdk_txpush = null;
                ShellSdk_baidussp sdk_baidussp = null;

                //接入友盟统计-标准版
                if (m_transfer_data.sdks.IndexOf(SdkType.umeng_aly) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.umeng_aly]).sdk_path;
                    sdk_umeng_aly = new ShellSdk_umeng_aly();
                    sdk_umeng_aly.InitSdk(sdk_path, GetApkInfo());
                    sdk_umeng_aly.MergeAndroidManifest(m_transfer_data.umeng_key.umeng_key, "#USE_CHANNEL#");
                    sdk_umeng_aly.MergeAssets();
                    sdk_umeng_aly.MergeLib();
                    sdk_umeng_aly.MergeRes();
                    sdk_umeng_aly.MergeSmali();
                    sdk_umeng_aly.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入友盟统计-游戏版
                if (m_transfer_data.sdks.IndexOf(SdkType.umeng_game) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.umeng_game]).sdk_path;
                    sdk_umeng_game = new ShellSdk_umeng_game();
                    sdk_umeng_game.InitSdk(sdk_path, GetApkInfo());
                    sdk_umeng_game.MergeAndroidManifest(m_transfer_data.umeng_key.umeng_key, "");
                    sdk_umeng_game.MergeAssets();
                    sdk_umeng_game.MergeLib();
                    sdk_umeng_game.MergeRes();
                    sdk_umeng_game.MergeSmali();
                    sdk_umeng_game.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入友盟PUSH
                if (m_transfer_data.sdks.IndexOf(SdkType.umeng_push) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.umeng_push]).sdk_path;
                    sdk_umeng_push = new ShellSdk_umeng_push();
                    sdk_umeng_push.InitSdk(sdk_path, GetApkInfo());
                    sdk_umeng_push.MergeAndroidManifest(m_transfer_data.umeng_key.umeng_key, m_transfer_data.umeng_key.umeng_message_key);
                    sdk_umeng_push.MergeAssets();
                    sdk_umeng_push.MergeLib();
                    sdk_umeng_push.MergeRes();
                    sdk_umeng_push.MergeSmali();
                    sdk_umeng_push.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入hao123
                if (m_transfer_data.sdks.IndexOf(SdkType.hao123) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.hao123]).sdk_path;
                    sdk_hao123 = new ShellSdk_hao123();
                    sdk_hao123.InitSdk(sdk_path, GetApkInfo());
                    sdk_hao123.MergeAndroidManifest();
                    sdk_hao123.MergeAssets();
                    sdk_hao123.MergeLib();
                    sdk_hao123.MergeRes();
                    sdk_hao123.MergeSmali();
                    sdk_hao123.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入3gpp-标准版
                if (m_transfer_data.sdks.IndexOf(SdkType.g3pp) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.g3pp]).sdk_path;
                    sdk_3gpp = new ShellSdk_3gpp();
                    sdk_3gpp.InitSdk(sdk_path, GetApkInfo());
                    sdk_3gpp.MergeAndroidManifest(m_transfer_data.g3pp_key.appkey, m_transfer_data.g3pp_key.local_pid, m_transfer_data.g3pp_key.local_cid);
                    sdk_3gpp.MergeAssets();
                    sdk_3gpp.MergeLib();
                    sdk_3gpp.MergeRes();
                    sdk_3gpp.MergeSmali();
                    sdk_3gpp.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入3gpp-游戏版
                if (m_transfer_data.sdks.IndexOf(SdkType.g3ppgame) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.g3ppgame]).sdk_path;
                    sdk_3gppgame = new ShellSdk_3gppgame();
                    sdk_3gppgame.InitSdk(sdk_path, GetApkInfo());
                    sdk_3gppgame.MergeAndroidManifest(m_transfer_data.g3pp_key.appkey, m_transfer_data.g3pp_key.local_pid, m_transfer_data.g3pp_key.local_cid);
                    sdk_3gppgame.MergeAssets();
                    sdk_3gppgame.MergeLib();
                    sdk_3gppgame.MergeRes();
                    sdk_3gppgame.MergeSmali();
                    sdk_3gppgame.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入腾讯手管
                if (m_transfer_data.sdks.IndexOf(SdkType.txsg) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.txsg]).sdk_path;
                    sdk_txsg = new ShellSdk_txsg();
                    sdk_txsg.InitSdk(sdk_path, GetApkInfo());
                    sdk_txsg.MergeAndroidManifest();
                    sdk_txsg.MergeAssets();
                    sdk_txsg.MergeLib();
                    sdk_txsg.MergeRes();
                    sdk_txsg.MergeSmali();
                    sdk_txsg.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入腾讯Push
                if (m_transfer_data.sdks.IndexOf(SdkType.txpush) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.txpush]).sdk_path;
                    sdk_txpush = new ShellSdk_txpush();
                    sdk_txpush.InitSdk(sdk_path, GetApkInfo());
                    sdk_txpush.MergeAndroidManifest();
                    sdk_txpush.MergeAssets();
                    sdk_txpush.MergeLib();
                    sdk_txpush.MergeRes();
                    sdk_txpush.MergeSmali();
                    sdk_txpush.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入广点通标准版
                if (m_transfer_data.sdks.IndexOf(SdkType.gdt) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.gdt]).sdk_path;
                    sdk_gdt = new ShellSdk_gdt();
                    sdk_gdt.InitSdk(sdk_path, GetApkInfo());
                    sdk_gdt.MergeAndroidManifest(m_transfer_data.gdt_key.gdt_appid, m_transfer_data.gdt_key.gdt_insert_adid, m_transfer_data.gdt_key.gdt_start_adid);
                    sdk_gdt.MergeAssets();
                    sdk_gdt.MergeLib();
                    sdk_gdt.MergeRes();
                    sdk_gdt.MergeSmali();
                    sdk_gdt.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入广点通x5内核版
                if (m_transfer_data.sdks.IndexOf(SdkType.gdtx5) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.gdtx5]).sdk_path;
                    sdk_gdtx5 = new ShellSdk_gdtx5();
                    sdk_gdtx5.InitSdk(sdk_path, GetApkInfo());
                    sdk_gdtx5.MergeAndroidManifest(m_transfer_data.gdt_key.gdt_appkey, m_transfer_data.gdt_key.gdt_appid, m_transfer_data.gdt_key.gdt_insert_adid, m_transfer_data.gdt_key.gdt_start_adid);
                    sdk_gdtx5.MergeAssets();
                    sdk_gdtx5.MergeLib();
                    sdk_gdtx5.MergeRes();
                    sdk_gdtx5.MergeSmali();
                    sdk_gdtx5.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }
                //接入百度SSP
                if (m_transfer_data.sdks.IndexOf(SdkType.baidussp) != -1)
                {
                    sdk_path = AppDomain.CurrentDomain.BaseDirectory + Controller.GetManager().get_sdk(shell_env.sdk_class_dict[SdkType.baidussp]).sdk_path;
                    sdk_baidussp = new ShellSdk_baidussp();
                    sdk_baidussp.InitSdk(sdk_path, GetApkInfo());
                    sdk_baidussp.MergeAndroidManifest(m_transfer_data.baidussp_key.baidussp_appid, m_transfer_data.baidussp_key.baidussp_insert_adid, m_transfer_data.baidussp_key.baidussp_start_adid);
                    sdk_baidussp.MergeAssets();
                    sdk_baidussp.MergeLib();
                    sdk_baidussp.MergeRes();
                    sdk_baidussp.MergeSmali();
                    sdk_baidussp.InsertSmali(SmaliInsertType.InsertSmali, m_insert_activity, m_insert_application);
                }

                //改名
                if (m_transfer_data.use_displayname)
                {
                    base.ReDisplayName(m_transfer_data.displayname);
                }

                //修改版本号
                if (m_transfer_data.use_version)
                {
                    base.ReVersion(m_transfer_data.versionstring, m_transfer_data.versioncode);
                }

                //修改图标
                if (m_transfer_data.use_icon)
                {
                    base.ReIcon(m_transfer_data.iconpath);
                }

                //修改渠道号，并打包
                if (m_transfer_data.channel_and_path_dict.Count > 0)
                {
                    foreach (var pair in m_transfer_data.channel_and_path_dict)
                    {
                        if (sdk_umeng_aly != null)
                            sdk_umeng_aly.ReChannel(pair.Key);
                        if (sdk_umeng_game != null)
                            sdk_umeng_game.ReChannel(pair.Key);
                        m_Func.repack_apk(false);
                        string signed_path = pair.Value + ".signed";
                        m_Func.signed_apk(signed_path);
                        m_Func.zipalign_apk(signed_path, pair.Value);
                        try { File.Delete(signed_path); } catch (Exception) { }
                    }
                }
                m_Func.clear_env();

            } while (false);
        }
    }
}
