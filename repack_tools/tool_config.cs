using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// 打包工具-类库
/// </summary>
namespace repack_tools
{
    /// <summary>
    /// 打包工具配置类
    /// </summary>
    public class tool_config
    {
        static private tool_config_model _config = null;

        /// <summary>
        /// 打包工具配置
        /// </summary>
        static public tool_config_model ToolConfig
        {
            get
            {
                if (_config == null)
                    _config = new tool_config_model();
                return _config;
            }
        }

        /// <summary>
        /// 初始化打包工具
        /// </summary>
        static public void init()
        {
            JObject json = new JObject();
            json["apktool"] = AppDomain.CurrentDomain.BaseDirectory + @"tools\apktool\apktool.jar";
            json["apktoolbat"] = AppDomain.CurrentDomain.BaseDirectory + @"tools\apktool\apktool.bat";
            json["zipalign"] = AppDomain.CurrentDomain.BaseDirectory + @"tools\zipalign.exe";

            ToolConfig.apktool = json["apktool"].ToString();
            ToolConfig.zipalign = json["zipalign"].ToString();
            ToolConfig.apktoolbat = json["apktoolbat"].ToString();
            string java_home = ToolConfig.signtool = Environment.GetEnvironmentVariable("JAVA_HOME");
            java_home = java_home.TrimEnd(';');
            ToolConfig.signtool = java_home + @"\bin\jarsigner.exe";
            ToolConfig.javatool = java_home + @"\bin\java.exe";
        }
    }
}
