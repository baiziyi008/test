using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class shell_router
    {
        /// <summary>
        /// 智能适配路由，需手工配置
        /// </summary>
        public static Dictionary<string, string> router_dict = new Dictionary<string, string>()
        {
            //生存战争
            { "com.owspace.wezeit","repack_shell.package_com_owspace_wezeit"}
        };

        /// <summary>
        /// 智能路由方法选择
        /// </summary>
        /// <param name="packagename">包名</param>
        /// <returns></returns>
        public static ShellPublic smart_run_shell(string packagename = "normal") {
            if (router_dict.ContainsKey(packagename)) {
                return (ShellPublic)(Activator.CreateInstance(Type.GetType(router_dict[packagename])));
            }
            return new package_universal();
        }
    }
}
