using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repack_shell
{
    public class package_com_owspace_wezeit : package_universal
    {
        public new void RunTask() {
            SetInsertActivity("com.owspace.wezeit.activity.MainActivity");
            base.RunTask();
        }
    }
}
