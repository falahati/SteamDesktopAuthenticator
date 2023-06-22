using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SteamAuth;
using System.Net;

namespace Steam_Desktop_Authenticator
{
    public partial class ConfirmationFormWeb : Form
    {
        private SteamGuardAccount steamAccount;

        public ConfirmationFormWeb(SteamGuardAccount steamAccount, List<Confirmation> confirmations)
        {
            InitializeComponent();
            this.steamAccount = steamAccount;
            this.Text = String.Format("Trade Confirmations - {0}", steamAccount.AccountName);
            this.loadConfirmations(confirmations);
        }
        
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            List<Confirmation> confirmations = new List<Confirmation>();

            try
            {
                confirmations.AddRange(
                    await steamAccount.FetchConfirmationsAsync()
                );
            }
            catch (WebException)
            {
                this.Close();
            }

            this.loadConfirmations(confirmations);
        }

        private void loadConfirmations(List<Confirmation> confirmations)
        {
            this.flp_confirmations.Controls.Clear();
            foreach (var confirmation in confirmations)
            {
                var item = new ConfirmationItem(
                    confirmation
                );
                item.Accepted += ItemOnAccepted;
                this.flp_confirmations.Controls.Add(item);
            }
        }

        private void ItemOnAccepted(object sender, Confirmation e)
        {
            try
            {
                if (this.steamAccount.AcceptConfirmation(e))
                {
                    this.btnRefresh_Click(sender, EventArgs.Empty);
                }
            }
            catch
            {
                // ignore
            }
        }
    }
}
