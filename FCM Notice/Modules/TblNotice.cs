using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FCM_Notice.Modules
{
    public class TblNotice
    {
        public int memId { get; set; }
        public string name { get; set; }
        public string fcmToken { get; set; }
    }
}
