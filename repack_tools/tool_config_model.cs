using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_tools
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public enum tool_error
    {
        /// <summary>
        /// 正常
        /// </summary>
        error_ok = 0x0000,
        /// <summary>
        /// 反编译出错
        /// </summary>
        error_decompiler = 0x0001,
        /// <summary>
        /// 未初始化工具
        /// </summary>
        error_uninit = 0x0002
    }

    /// <summary>
    /// 工具配置信息
    /// </summary>
    public class tool_config_model
    {
        /// <summary>
        /// apktool工具路径
        /// </summary>
        public string apktool = string.Empty;
        /// <summary>
        /// apktool工具路径-对应批处理文件
        /// </summary>
        public string apktoolbat = string.Empty;
        /// <summary>
        /// 签名工具路径
        /// </summary>
        public string signtool = string.Empty;
        /// <summary>
        /// zipalign优化工具路径
        /// </summary>
        public string zipalign = string.Empty;
        /// <summary>
        /// java.exe的路径
        /// </summary>
        public string javatool = string.Empty;
    }

    /// <summary>
    /// APK信息
    /// </summary>
    public class tool_apk_model {
        /// <summary>
        /// 选择的APK路径
        /// </summary>
        public string ApkPath = string.Empty;
        /// <summary>
        /// 文件名
        /// </summary>
        public string ApkFilename = string.Empty;
        /// <summary>
        /// 反编译文件夹
        /// </summary>
        public string DecompilerFolder = string.Empty;
        /// <summary>
        /// 原包名
        /// </summary>
        public string PrePackageName = string.Empty;
        /// <summary>
        /// 新包名
        /// </summary>
        public string NewPackageName = string.Empty;
        /// <summary>
        /// AndroidManifest.xml路径
        /// </summary>
        public string AndroidManifestPath = string.Empty;
        /// <summary>
        /// 版本配置文件
        /// </summary>
        public string ApktoolYml = string.Empty;
        /// <summary>
        /// 重新打包APK路径
        /// </summary>
        public string RepackUnsignedApkPath = string.Empty;

        public string in_assets             = string.Empty;
        public string in_lib                = string.Empty;
        public string in_res                = string.Empty;
        public string in_smali              = string.Empty;
        public string in_smali_classes2     = string.Empty;
        public string in_unknown            = string.Empty;

        /// <summary>
        /// 签名信息
        /// </summary>
        public tool_keystore Keystore = null;

        /// <summary>
        /// 签名导出包
        /// </summary>
        public string SignedApkPath = string.Empty;

        /// <summary>
        /// zipalign优化包
        /// </summary>
        public string ZipalignApkPath = string.Empty;

        /// <summary>
        /// AndroidManifest配置信息
        /// </summary>
        public AndroidManifest_model settings = null;
    }

    /// <summary>
    /// 签名信息
    /// </summary>
    public class tool_keystore {
        /// <summary>
        /// 签名文件路径
        /// </summary>
        public string KeystorePath = string.Empty;
        /// <summary>
        /// 密码1
        /// </summary>
        public string Password1 = string.Empty;
        /// <summary>
        /// 密码2
        /// </summary>
        public string Password2 = string.Empty;
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias = string.Empty;
    }

    /// <summary>
    /// APK配置文件信息
    /// </summary>
    public class AndroidManifest_model {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName = string.Empty;
        /// <summary>
        /// 主Activity
        /// </summary>
        public string MainActivity = string.Empty;
        /// <summary>
        /// 运行的Application类
        /// </summary>
        public string AndroidApplication = string.Empty;
        /// <summary>
        /// 应用名
        /// </summary>
        public string AppName = string.Empty;
        /// <summary>
        /// Icon文件名
        /// </summary>
        public string IconFileName = string.Empty;
        /// <summary>
        /// 资源Smali文件
        /// </summary>
        public string R = string.Empty;
        /// <summary>
        /// 版本字符串
        /// </summary>
        public string VersionString = string.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionCode = string.Empty;

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions = new List<string>();
    }

    
}
