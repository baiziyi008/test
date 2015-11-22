using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace repack_tools
{
    /// <summary>
    /// 打包工具功能
    /// </summary>
    public class tool_func
    {
        /// <summary>
        /// APK信息
        /// </summary>
        private tool_apk_model DealApk = null;

        /// <summary>
        /// 初始化需要反编译的APK信息
        /// </summary>
        /// <param name="apkpath">APK路径</param>
        /// <returns>结果</returns>
        public tool_error InitApk(string apkpath,string signedApkpath,tool_keystore keystore) {
            tool_error error = tool_error.error_ok;
            DealApk = new tool_apk_model();
            DealApk.ApkPath = apkpath;
            DealApk.ApkFilename = Path.GetFileName(apkpath);
            DealApk.DecompilerFolder = Path.GetDirectoryName(tool_config.ToolConfig.apktool) + @"\" + Path.GetFileNameWithoutExtension(apkpath);
            DealApk.AndroidManifestPath = DealApk.DecompilerFolder + @"\AndroidManifest.xml";
            DealApk.ApktoolYml = DealApk.DecompilerFolder + @"\apktool.yml";
            DealApk.RepackUnsignedApkPath = DealApk.DecompilerFolder + @"\dist\" + DealApk.ApkFilename;
            DealApk.in_assets = DealApk.DecompilerFolder + @"\assets";
            DealApk.in_lib = DealApk.DecompilerFolder + @"\lib";
            DealApk.in_res = DealApk.DecompilerFolder + @"\res";
            DealApk.in_smali = DealApk.DecompilerFolder + @"\smali";
            DealApk.in_smali_classes2 = DealApk.DecompilerFolder + @"\smali_classes2";
            DealApk.in_unknown = DealApk.DecompilerFolder + @"\unknown";
            DealApk.SignedApkPath = signedApkpath;
            if(signedApkpath!="")
                DealApk.ZipalignApkPath = Path.GetDirectoryName(signedApkpath) + @"\" + Path.GetFileNameWithoutExtension(signedApkpath) + @"_zipalign.apk";
            DealApk.Keystore = keystore;
            return error;
        }

        /// <summary>
        /// 获取APK信息
        /// </summary>
        /// <returns></returns>
        public tool_apk_model GetApkInfo() {
            return DealApk;
        }

        /// <summary>
        /// 设置APK信息
        /// </summary>
        /// <param name="apkinfo">apk信息</param>
        public void SetApkInfo(tool_apk_model apkinfo) {
            DealApk = apkinfo;
        }

        /// <summary>
        /// 反编译APK
        /// </summary>
        /// <returns>结果</returns>
        public tool_error decompiler_apk() {
            tool_error error = tool_error.error_ok;
            String cmd = " -jar " + tool_config.ToolConfig.apktool + " d -f " + DealApk.ApkPath + " -o " + DealApk.DecompilerFolder;
            String ret = ExcuteCmd(tool_config.ToolConfig.javatool, cmd);
            return error;

        }

        /// <summary>
        /// 重新编译APK
        /// </summary>
        /// <returns></returns>
        public tool_error repack_apk(bool func = true) {
            tool_error error = tool_error.error_ok;
            String foldername = Path.GetFileNameWithoutExtension(DealApk.ApkFilename);
            if (func)
            {
                String cmd = " -jar " + tool_config.ToolConfig.apktool + " b " + DealApk.DecompilerFolder;
                String ret = ExcuteCmd(tool_config.ToolConfig.javatool, cmd);
            }
            else {
                String cmd = " b " + DealApk.DecompilerFolder;
                String ret = ExcuteCmd(tool_config.ToolConfig.apktoolbat, cmd);
            }
            return error;
        }

        /// <summary>
        /// 重新签名APK
        /// </summary>
        /// <returns></returns>
        public tool_error signed_apk() {
            tool_error error = tool_error.error_ok;
            String cmd = @"-verbose -keystore " + DealApk.Keystore.KeystorePath
                + " -signedjar " + DealApk.SignedApkPath + @" " + DealApk.RepackUnsignedApkPath + @" " + DealApk.Keystore.Alias
                + " -storepass " + DealApk.Keystore.Password1 + " -keypass " + DealApk.Keystore.Password2;
            String ret = ExcuteCmd(tool_config.ToolConfig.signtool, cmd);
            return error;
        }

        /// <summary>
        /// 重签名APK
        /// </summary>
        /// <param name="signed_path"></param>
        /// <returns></returns>
        public tool_error signed_apk(string signed_path)
        {
            tool_error error = tool_error.error_ok;
            String cmd = @"-verbose -keystore " + DealApk.Keystore.KeystorePath
                + " -signedjar " + signed_path + @" " + DealApk.RepackUnsignedApkPath + @" " + DealApk.Keystore.Alias
                + " -storepass " + DealApk.Keystore.Password1 + " -keypass " + DealApk.Keystore.Password2;
            String ret = ExcuteCmd(tool_config.ToolConfig.signtool, cmd);
            return error;
        }

        /// <summary>
        /// zipalign优化APK
        /// </summary>
        /// <returns></returns>
        public tool_error zipalign_apk() {
            tool_error error = tool_error.error_ok;
            String cmd = @"-v 4 " + DealApk.SignedApkPath + @" " + DealApk.ZipalignApkPath;
            String ret = ExcuteCmd(tool_config.ToolConfig.zipalign, cmd);
            return error;
        }

        /// <summary>
        /// zipalign优化APK
        /// </summary>
        /// <param name="signed_path"></param>
        /// <param name="zipalign_path"></param>
        /// <returns></returns>
        public tool_error zipalign_apk(string signed_path,string zipalign_path)
        {
            tool_error error = tool_error.error_ok;
            String cmd = @"-v 4 " + signed_path + @" " + zipalign_path;
            String ret = ExcuteCmd(tool_config.ToolConfig.zipalign, cmd);
            return error;
        }

        /// <summary>
        /// 清理反编译环境
        /// </summary>
        /// <returns></returns>
        public tool_error clear_env() {
            tool_error error = tool_error.error_ok;
            try
            {
                Directory.Delete(DealApk.DecompilerFolder, true);
                File.Delete(DealApk.SignedApkPath);
            }
            catch (Exception) { }
            return error;
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="exepath">程序路径</param>
        /// <param name="cmd">指令</param>
        /// <returns></returns>
        public static String ExcuteCmd(string exepath, string cmd)
        {
            string output = string.Empty;
            Process exe = new Process();
            exe.StartInfo.FileName = exepath;
            exe.StartInfo.Arguments = cmd;
            exe.StartInfo.UseShellExecute = false;
            exe.StartInfo.RedirectStandardInput = true;
            exe.StartInfo.RedirectStandardOutput = true;
            exe.StartInfo.RedirectStandardError = true;
            exe.StartInfo.CreateNoWindow = true;
            //exe.StartInfo.WorkingDirectory = Path.GetDirectoryName(exepath);
            exe.Start();
            output = exe.StandardOutput.ReadToEnd();
            exe.WaitForExit();
            exe.Close();
            return output;
        }
    }
}
