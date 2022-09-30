using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCM_Notice.Views
{
    public interface IFcmNotice
    {
        public TextBox txtTitle { get; set; }
        public TextBox txtMessage { get; set; }
        public ComboBox cmboTarget { get; set; }

        public event EventHandler btnSend;
        public event EventHandler cmboValChange;
    }
}
