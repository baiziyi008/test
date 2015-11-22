using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public interface ISdkMerge
    {
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <param name="sdk_decompiler_path">SDK反编译路径</param>
        /// <param name="apkinfo">目标APK信息</param>
        void InitSdk(string sdk_decompiler_path,repack_tools.tool_apk_model apkinfo);

        /// <summary>
        /// 合并AndroidManifest.xml
        /// </summary>
        void MergeAndroidManifest();

        /// <summary>
        /// 合并Assets
        /// </summary>
        void MergeAssets();

        /// <summary>
        /// 合并LIB
        /// </summary>
        void MergeLib();

        /// <summary>
        /// 合并Res
        /// </summary>
        void MergeRes();

        /// <summary>
        /// 合并Smali
        /// </summary>
        /// <param name="copy_folders">需要合并的文件夹</param>
        /// <param name="copy_files">需要合并的文件</param>
        void MergeSmali(List<string> copy_folders,List<string> copy_files);

        /// <summary>
        /// 植入Smali代码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="insert_activity"></param>
        /// <param name="insert_application"></param>
        void InsertSmali(SmaliInsertType type, string insert_activity, string insert_application);
    }
}
