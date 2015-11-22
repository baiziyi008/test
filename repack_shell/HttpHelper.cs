using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace repack_shell
{
    /// <summary>
    /// HTTP 请求类
    /// </summary>
    public class HttpHelper
    {
        public static byte[] HttpGet(string url_data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url_data);
            request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; QQDownload 534; TencentTraveler 4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; CIBA; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.2)";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            Stream s = response.GetResponseStream();
            List<byte> bytes = new List<byte>();
            int ret = -1;
            while ((ret = s.ReadByte()) != -1)
            {
                bytes.Add((byte)ret);
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="FileName">保存的文件地址</param>
        public static void DownLoadFile(String url, String FileName)
        {
            try
            {
                //httpRqst.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; QQDownload 534; TencentTraveler 4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; CIBA; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.2)";
                FileStream outputStream = new FileStream(FileName, FileMode.Create);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; QQDownload 534; TencentTraveler 4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; CIBA; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.2)";
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                Stream httpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = httpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = httpStream.Read(buffer, 0, bufferSize);
                }
                httpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception) { }
        }

        static public string HttpPost(string Url, string postDataStr)
        {
            try
            {
                CookieContainer cookie = new CookieContainer();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                request.CookieContainer = cookie;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));//gb2312
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
