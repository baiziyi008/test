using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace repack
{
    public partial class upload_new_sdk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string load_sdk_paths() {
            string option_str = string.Empty;
            if (Directory.Exists(data.SdkFolder)) {
                List<string> sdk_paths = Directory.GetDirectories(data.SdkFolder).ToList();
                if (sdk_paths.Count > 0) {
                    string sdkpath = string.Empty;
                    foreach (string path in sdk_paths) {
                        sdkpath = path.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                        option_str += "<option value=\"" + sdkpath + "\">" + sdkpath + "</option>";
                    }
                }
            }
            return option_str;
        }
    }
}