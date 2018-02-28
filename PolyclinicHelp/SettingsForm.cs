using System;
using System.Windows.Forms;

namespace PolyclinicHelp
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private StartForm parentForm;

        public SettingsForm(StartForm parentForm) : this()
        {
            this.parentForm = parentForm;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            parentForm.ClinicView.Columns[check.Text].Visible = check.Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < parentForm.ClinicView.ColumnCount; i++)
            {
                ShowContent.Items.Add(parentForm.ClinicView.Columns[i].HeaderText, parentForm.ClinicView.Columns[i].Visible);
            }
        }

        private void ShowContent_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (sender is CheckedListBox clb)
            {
                if (clb.SelectedItem == null)
                    return;

                parentForm.ClinicView.Columns[clb.SelectedItem.ToString()].Visible = e.NewValue == CheckState.Checked;
            }
        }
    }
}
