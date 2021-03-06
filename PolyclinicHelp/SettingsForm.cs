﻿using System;
using System.Windows.Forms;

namespace PolyclinicHelp
{
    public partial class SettingsForm : Form
    {
        private SettingsForm()
        {
            InitializeComponent();
        }

        private readonly StartForm _parentForm;

        public SettingsForm(StartForm parentForm) : this() => _parentForm = parentForm;

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < _parentForm.ClinicView.ColumnCount; i++)
                ShowContent.Items.Add(item: _parentForm.ClinicView.Columns[i].HeaderText, isChecked: _parentForm.ClinicView.Columns[i].Visible);
        }

        private void ShowContent_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sender is CheckedListBox checkedList && checkedList.SelectedItem != null && _parentForm?
                    .ClinicView?
                    .Columns != null)
                // ReSharper disable once PossibleNullReferenceException
                try
                {
                    _parentForm.ClinicView.Columns[checkedList.SelectedIndex].Visible =
                    e.NewValue == CheckState.Checked;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Открыт ли файл?");
                }
        }
    }
}
