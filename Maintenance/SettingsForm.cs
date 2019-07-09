using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Maintenance
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/xCONFLiCTiONx/Maintenance/#help");
        }
    }
}
