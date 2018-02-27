using System;
using System.Windows.Forms;

namespace PolyclinicHelp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "CSV | *.csv"
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
                            Shared.List.Add(new Polyclinic(fieldParser.ReadFields()));
                        }
                    }
                    else
                        // ToDo: Уточнить тип исключения и ловить его!
                        throw new Exception();

                    PolyclinicHelpView.DataSource = Shared.List;
                }
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {

        }
    }
}
