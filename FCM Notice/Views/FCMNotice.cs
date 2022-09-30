using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCM_Notice.Presenters;

namespace FCM_Notice.Views
{
    public partial class FCMNotice : Form,IFcmNotice
    {
        public FCMNotice()
        {
            InitializeComponent();
            FCMPresenter p = new FCMPresenter(this);
            this.cmbo_Target.TextChanged += delegate { cmboValChange?.Invoke(this, EventArgs.Empty); };
            this.btn_Send.Click+=delegate { btnSend?.Invoke(this,EventArgs.Empty); }; 
        }

        public TextBox txtTitle { get => txt_title; set => txt_title = value; }
        public TextBox txtMessage { get => txt_Message; set => txt_Message=value; }
        public ComboBox cmboTarget { get => cmbo_Target; set => cmbo_Target=value; }

        public event EventHandler btnSend;
        public event EventHandler cmboValChange;
    }
}
