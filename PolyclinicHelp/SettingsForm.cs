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

        private readonly StartForm _parentForm;

        public SettingsForm(StartForm parentForm) : this()
        {
            _parentForm = parentForm;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < _parentForm.ClinicView.ColumnCount; i++)
            {
                ShowContent.Items.Add(_parentForm.ClinicView.Columns[i].HeaderText, _parentForm.ClinicView.Columns[i].Visible);
            }
        }

        private void ShowContent_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sender is CheckedListBox checkedList && checkedList.SelectedItem != null && _parentForm?.ClinicView?.Columns != null)
                // ReSharper disable once PossibleNullReferenceException
                    _parentForm.ClinicView.Columns[(string) checkedList.SelectedItem].Visible =
                        e.NewValue == CheckState.Checked;
        }
    }
}
