using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace repack
{
    public class data
    {
        public static string DataFolder = AppDomain.CurrentDomain.BaseDirectory + @"data\";
        public static string DecompilerProject = DataFolder + @"decompiler_project"; 
        public static string PublishApk = DataFolder + @"publish_apk";
        public static string OriginalApk = DataFolder + @"original_apk";
        public static string KeystoreFolder = DataFolder + @"keystore";
        public static string Database = DataFolder + @"repark.db";
        public static string SdkFolder = DataFolder + @"sdk";
        public static string TempFolder = DataFolder + @"temp";

        public static string stiven_keygen_file = KeystoreFolder + @"\stiven_keygen.keystore";
        public static string stiven_keygen_password = @"shi66312";
        public static string stiven_keygen_alias = @"stiven_keygen";

        private static repack_tools.tool_keystore Keystore = null;
        public static repack_tools.tool_keystore GetPublicKeyStore() {
            if (Keystore == null)
            {
                Keystore = new repack_tools.tool_keystore();
                Keystore.KeystorePath = stiven_keygen_file;
                Keystore.Alias = stiven_keygen_alias;
                Keystore.Password1 = stiven_keygen_password;
                Keystore.Password2 = stiven_keygen_password;
            }
            return Keystore;
        }

        public static repack_tools.tool_keystore GetGuijunKeyStore() {
            repack_tools.tool_keystore key = new repack_tools.tool_keystore();
            key.KeystorePath = KeystoreFolder + @"\guijun.keystore";
            key.Alias = "demo.keystore";
            key.Password1 = "Guijun1975";
            key.Password2 = "Guijun1975";
            return key;
        }
    }
}