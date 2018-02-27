using System.Windows.Forms;

namespace PolyclinicHelp
{
    public partial class SettingsForm : Form
    {
        private StartForm parent;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(StartForm parent) : this()
        {
            this.parent = parent;
        }

        private void CheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            parent.PolyclinicHelpView.Columns[check.Text].Visible = check.Checked;
        }
    }
}
