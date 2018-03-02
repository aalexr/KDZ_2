using System;
using System.Windows.Forms;
using PolyclinicHelp.Properties;

namespace PolyclinicHelp
{
    public partial class StartForm : Form
    {
        public StartForm() => InitializeComponent();

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            clinicBindingSource.Clear();

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
                            clinicBindingSource.Add(new Clinic(fieldParser.ReadFields()));
                        }
                    }
                    else
                        // ToDo: Уточнить тип исключения и ловить его!
                        throw new Exception();
                }
            }
        }

        private void ShowHideToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm(this).Show();

        private void BindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (clinicBindingSource.List.Count == 0)
            {

            }
            else
            {
                // ToDO Добавление нового
            }
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            clinicBindingSource.Remove(ClinicView.CurrentRow);
        }

        private async void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog{ Filter = Resources.StartForm_SaveAsToolStripMenuItem_Click_Таблица_CSV_____csv})
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;
                using (var writer = new System.IO.StreamWriter(saveDialog.FileName))
                {
                    for (var i = 0; i < ClinicView.ColumnCount; i++)
                        await writer.WriteAsync(ClinicView.Columns[i].HeaderText + ";");

                    await writer.WriteLineAsync();

                    for (var i = 0; i < clinicBindingSource.Count; i++)
                        await writer.WriteLineAsync(clinicBindingSource.List[i].ToString());
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
