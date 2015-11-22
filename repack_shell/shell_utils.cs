using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace repack_shell
{
    public class shell_utils
    {
        /// <summary>
        /// 快速排序-比较方法（pxm.id_low比较）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int pxm_compare_id_low(public_xml_model a, public_xml_model b)
        {
            if (a.id_low > b.id_low) return 1;
            else if (a.id_low == b.id_low) return 0;
            else return -1;
        }

        /// <summary>
        /// 快速排序-比较方法（pxm.id_high比较）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int pxm_compare_id_high(public_xml_model a, public_xml_model b)
        {
            if (a.id_high > b.id_high) return 1;
            else if (a.id_high == b.id_high) return 0;
            else return -1;
        }

        /// <summary>
        /// 是否存在name属性，在public中
        /// </summary>
        /// <param name="pxms"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool exist_pxm_name(List<public_xml_model> pxms,string name) {
            bool ret = false;
            if (pxms.Count > 0) {
                foreach (public_xml_model pxm in pxms) {
                    if (pxm.name == name) {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取appkey
        /// </summary>
        /// <param name="appname">产品名称</param>
        /// <param name="packagename">产品包名</param>
        /// <param name="channelname">渠道名</param>
        /// <param name="pid">产品ID</param>
        /// <param name="cid">渠道ID</param>
        /// <returns></returns>
        public static string get_3gpp_appkey(string appname,string packagename,string channelname,string pid,string cid) {
            string appkey = "";
            string url = "http://fifa.v3.3gshow.cn/api/createapi.ashx?pname=" 
                + HttpUtility.UrlEncode(appname) 
                + "&pid=" + pid 
                + "&packagename=" + HttpUtility.UrlEncode(packagename) 
                + "&cname=" + HttpUtility.UrlEncode(channelname) 
                + "&cid=" + cid;
            string jsonstr = Encoding.UTF8.GetString(HttpHelper.HttpGet(url));
            JObject jsonResult = (JObject)JsonConvert.DeserializeObject(jsonstr);
            if (jsonResult["code"] != null && jsonResult["code"].ToString() == "100")
            {
                if (jsonResult["appkey"] != null) {
                    appkey = jsonResult["appkey"].ToString();
                }
            }
            return appkey;
        }


        /// <summary>
        /// 插入smali方法
        /// </summary>
        /// <param name="smali_content">被插入的smali文件内容</param>
        /// <param name="func_content">插入函数内容</param>
        /// <param name="insert_smali">植入的smali语句</param>
        public static void insert_smali_function(ref string smali_content, string func_content, string insert_smali)
        {
            List<string> smali_lines = smali_content.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            if (smali_lines.Count > 0)
            {
                List<string> insert_func_lines = func_content.Replace("#INSERT_SMALI_CODE#", insert_smali).Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                smali_lines.AddRange(insert_func_lines);
                smali_content = string.Empty;
                foreach (string line in smali_lines)
                {
                    smali_content += line + "\r\n";
                }
            }
        }

        /// <summary>
        /// 插入smali语句
        /// </summary>
        /// <param name="smali_content">被插入的smali文件内容</param>
        /// <param name="insert_type">插入函数名</param>
        /// <param name="insert_find">插入的位置</param>
        /// <param name="insert_smali">植入的smali语句</param>
        /// <param name="pos_type">插入在目标位置之前还是之后</param>
        /// <returns>是否植入smali代码成功</returns>
        public static bool insert_smali_code(ref string smali_content, SmaliInsertFunctionType insert_type, string insert_find,string insert_smali, InsertPosType pos_type)
        {
            List<string> smali_lines = smali_content.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            do
            {
                if (smali_lines.Count > 0)
                {
                    if (shell_env.insert_smali_function_title_dict.ContainsKey(insert_type))
                    {
                        List<string> find_functions = new List<string>();
                        find_functions.Add(shell_env.insert_smali_function_title_dict[insert_type]);
                        find_functions.Add(shell_env.insert_smali_function_title_dict[insert_type].Replace("public", "protected"));
                        find_functions.Add(shell_env.insert_smali_function_title_dict[insert_type].Replace("public", "private"));
                        int func_start_pos = -1;
                        int func_end_pos = -1;
                        int insert_pos = -1;
                        foreach (string find_func in find_functions)
                        {
                            if ((func_start_pos = get_smali_code_index(smali_lines, find_func)) != -1)
                                break;
                        }
                        if (func_start_pos == -1) break;
                        for (int i = func_start_pos; i < smali_lines.Count; i++)
                        {
                            if (smali_lines[i].IndexOf(shell_env.insert_smali_find_end_function) != -1)
                            {
                                if (func_end_pos == -1)
                                    func_end_pos = i;
                            }
                            if (smali_lines[i].IndexOf(insert_find) != -1)
                            {
                                if (insert_pos == -1)
                                    insert_pos = i;
                            }
                            if (func_end_pos != -1 && insert_pos != -1) break;

                        }
                        if (func_end_pos == -1 || insert_pos == -1) break;

                        if (insert_pos < func_end_pos && insert_pos > func_start_pos)
                        {
                            bool flag = false;
                            for (int i = func_start_pos; i < func_end_pos; i++)
                            {
                                if (smali_lines[i].IndexOf(insert_smali) != -1)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                switch (pos_type) {
                                    case InsertPosType.insert_before:
                                        break;
                                    case InsertPosType.insert_after:
                                        insert_pos++;
                                        break;
                                    default:
                                        break;
                                }
                                smali_lines.Insert(insert_pos, insert_smali);

                                smali_content = string.Empty;
                                foreach (string line in smali_lines)
                                {
                                    smali_content += line + "\r\n";
                                }
                                return true;
                            }
                        }
                    }
                }
            } while (false);
            return false;
        }

        /// <summary>
        /// 查找smali代码的位置
        /// </summary>
        /// <param name="smali_lines"></param>
        /// <param name="smali_code"></param>
        /// <returns></returns>
        public static int get_smali_code_index(List<string> smali_lines, string smali_code) {
            int ret = -1;
            if (smali_lines.Count > 0) {
                for (int i = 0; i < smali_lines.Count; i++) {
                    if (smali_lines[i].IndexOf(smali_code) != -1) {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
