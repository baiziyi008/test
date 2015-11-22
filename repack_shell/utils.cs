using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class utils
    {
        /// <summary>
        /// 搜索文件列表（模糊搜索）
        /// </summary>
        /// <param name="folder">搜寻目录</param>
        /// <param name="files">符合条件的文件列表</param>
        /// <param name="search_keywords">搜索关键字</param>
        /// <param name="extentions">文件后缀名</param>
        public static void get_files_from_folder(string folder,ref List<string> files,string search_keywords,string extentions)
        {
            List<string> folders = Directory.GetDirectories(folder).ToList();
            if (folders.Count > 0) {
                foreach (string child_folder in folders) {
                    get_files_from_folder(child_folder, ref files, search_keywords, extentions);
                }
            }
            List<string> child_files = Directory.GetFiles(folder).ToList();
            List<string> exts = extentions.ToUpper().Split('|').ToList();
            string ext = string.Empty;
            string filename = string.Empty;
            foreach (string child_file in child_files)
            {
                ext = @"*" + Path.GetExtension(child_file).ToUpper();
                filename = Path.GetFileName(child_file).ToUpper();
                if (filename.IndexOf(search_keywords.ToUpper()) != -1 || search_keywords == string.Empty)
                {
                    if (exts.IndexOf("*.*") != -1)
                    {
                        files.Add(child_file);
                        continue;
                    }
                    if (exts.IndexOf(ext) != -1)
                    {
                        files.Add(child_file);
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// 搜索文件列表（指定文件名搜索）
        /// </summary>
        /// <param name="folder">搜寻目录</param>
        /// <param name="files">符合条件的文件列表</param>
        /// <param name="search_filename">文件名</param>
        public static void get_files_from_folder(string folder, ref List<string> files,string search_filename) {
            List<string> folders = Directory.GetDirectories(folder).ToList();
            if (folders.Count > 0)
            {
                foreach (string child_folder in folders)
                {
                    get_files_from_folder(child_folder, ref files, search_filename);
                }
            }
            List<string> child_files = Directory.GetFiles(folder).ToList();
            string filename = string.Empty;
            foreach (string child_file in child_files)
            {
                filename = Path.GetFileName(child_file);
                if (filename == search_filename)
                {
                    files.Add(child_file);
                }
            }
        }

        /// <summary>
        /// 复制文件夹（递归复制）
        /// </summary>
        /// <param name="source_folder">源文件夹</param>
        /// <param name="dest_folder">目标文件夹</param>
        public static void copy_folder(string source_folder, string dest_folder) {
            if (!Directory.Exists(source_folder)) return;
            string folder_name = (new DirectoryInfo(source_folder)).Name;
            string dest_folder_path = dest_folder + @"\" + folder_name;
            if(!Directory.Exists(dest_folder_path))
                Directory.CreateDirectory(dest_folder_path);
            List<string> dirs = Directory.GetDirectories(source_folder).ToList();
            if (dirs.Count > 0) {
                foreach (string dir in dirs) {
                    copy_folder(dir, dest_folder_path);
                }
            }
            List<string> files = Directory.GetFiles(source_folder).ToList();
            if (files.Count > 0) {
                foreach (string file in files) {
                    if (File.Exists(dest_folder_path + @"\" + Path.GetFileName(file))) {
                        File.Delete(dest_folder_path + @"\" + Path.GetFileName(file));
                    }
                    File.Copy(file, dest_folder_path + @"\" + Path.GetFileName(file));
                }
            }
        }

        /// <summary>
        /// 快速排序-对比委托原型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public delegate int quick_sort_compare<T>(T a, T b);
        /// <summary>
        /// 快速排序（递归版）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pointer_forward"></param>
        /// <param name="pointer_backward"></param>
        /// <param name="compare"></param>
        public static void quick_sort<T>(ref List<T> source, int pointer_forward, int pointer_backward, quick_sort_compare<T> compare)
        {
            if (source == null) return;
            if (pointer_forward < pointer_backward)
            {
                int i = pointer_forward, j = pointer_backward;
                T record = source[pointer_forward];
                while (i < j)
                {
                    while (i < j && compare(source[pointer_backward], record) >= 0)
                        j--;
                    if (i < j)
                        source[i++] = source[j];

                    while (i < j && compare(source[i], record) < 0) 
                        i++;
                    if (i < j)
                        source[j--] = source[i];
                }
                source[i] = record;
                quick_sort(ref source, pointer_forward, i - 1, compare); 
                quick_sort(ref source, i + 1, pointer_backward, compare);
            }
        }

        public static void quick_sort_func<T>(ref List<T> source, int pointer_forward, int pointer_backward, Stack<int> stack, quick_sort_compare<T> compare)
        {
            int low = pointer_forward;
            int high = pointer_backward;
            T temp = source[low];
            while (high > low)
            {
                while (low < high && compare(temp, source[high]) <= 0)
                {
                    high--;
                }
                if (high > low)
                {
                    source[low] = source[high];
                    source[high] = temp;
                }
                while (low < high && compare(temp, source[low]) >= 0)
                {
                    low++;
                }
                if (high > low)
                {
                    source[high] = source[low];
                    source[low] = temp;
                }
                if (low == high)
                {
                    if (pointer_forward < low - 1)
                    {
                        stack.Push(pointer_forward);
                        stack.Push(low - 1);
                    }
                    if (pointer_backward > low + 1)
                    {
                        stack.Push(low + 1);
                        stack.Push(pointer_backward);
                    }
                }
            }
        }

        public static void quick_sort_fast<T>(ref List<T> source, int pointer_forward, int pointer_backward, quick_sort_compare<T> compare) {
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            stack.Push(source.Count - 1);
            while (stack.Count > 0)
            {
                int low = stack.Pop();
                int high = stack.Pop();
                int temp;
                if (low > high)
                {
                    temp = low;
                    low = high;
                    high = temp;
                }
                quick_sort_func(ref source, low, high, stack, compare);
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string get_time_stamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string get_time_stamp(DateTime dt) {
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}
