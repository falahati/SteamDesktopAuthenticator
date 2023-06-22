using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAuth;

namespace Steam_Desktop_Authenticator
{
    public partial class ConfirmationItem : UserControl
    {
        private EventHandler<Confirmation> onAccepted; 
        
        public event EventHandler<Confirmation> Accepted
        {
            add
            {
                onAccepted += value;
            }
            remove
            {
                onAccepted -= value;
            }
        }
        
        public Confirmation Confirmation { get; }

        public ConfirmationItem(Confirmation confirmation) : this()
        {
            Confirmation = confirmation;
        }

        public ConfirmationItem()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.lbl_title.Text = this.Confirmation?.Name;
            this.lbl_description.Text = this.Confirmation?.Summary;

            if (!string.IsNullOrWhiteSpace(this.Confirmation?.Icon))
            {
                try
                {
                    pb_icon.Load(this.Confirmation.Icon);
                }
                catch
                {
                    // ignore
                }
            }
        }

        private void b_accept_Click(object sender, EventArgs e)
        {
            onAccepted?.Invoke(this, Confirmation);
        }
    }
}
