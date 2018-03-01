﻿using System;
using System.Windows.Forms;
using PolyclinicHelp.Properties;

namespace PolyclinicHelp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        public DataGridView ClinicView { get => сlinicView; private set => сlinicView = value; }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Shared.List.Clear();
            using (var openDialog = new OpenFileDialog
            {
                Filter = Resources.StartForm_OpenToolStripMenuItem_Click_CSV_____csv
            })
            {
                if (openDialog.ShowDialog() != DialogResult.OK)
                    return;

                using (var fieldParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(openDialog.FileName))
                {
                    if (fieldParser.TextFieldType == Microsoft.VisualBasic.FileIO.FieldType.Delimited)
                        fieldParser.SetDelimiters(";");
                    else
                        // ToDo: Уточнить тип исключения и ловить его!
                        throw new Exception();

                    if (!fieldParser.EndOfData)
                    {
                        fieldParser.ReadFields();

                        while (!fieldParser.EndOfData)
                        {
                            Shared.List.Add(new Clinic(fieldParser.ReadFields()));
                        }
                    }
                    else
                        // ToDo: Уточнить тип исключения и ловить его!
                        throw new Exception();

                    сlinicView.DataSource = Shared.List;
                }
            }
        }

        private void ShowHideToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm(this).Show();
    }
}
