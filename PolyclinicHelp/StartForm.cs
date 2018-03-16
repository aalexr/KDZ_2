using System;
using System.Windows.Forms;
using PolyclinicHelp.Properties;

namespace PolyclinicHelp
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            rowShowNumericUpDown.Enabled = false;
            clinicSource.ListChanged += ClinicBindingSource_ListChanged;
        }

        private void FileOpenedOrCreated(object sender = null, EventArgs e = null)
        {
            fileOpened = true;
            clinicSource.Clear();
            bindingNavigator.Visible = true;
            ClinicView.Visible = true;
        }

        private void FileClosed(object sender = null, EventArgs e = null)
        {
            fileOpened = false;
            bindingNavigator.Visible = false;
            ClinicView.Visible = false;
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog
            {
                Filter = Resources.StartForm_OpenToolStripMenuItem_Click_CSV_____csv
            })
            {
                if (openDialog.ShowDialog() != DialogResult.OK)
                    return;

                FileOpenedOrCreated();
                using (var fieldParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(currentFile = openDialog.FileName))
                {
                    if (fieldParser.TextFieldType == Microsoft.VisualBasic.FileIO.FieldType.Delimited)
                        fieldParser.SetDelimiters(";");
                    else
                    {
                        currentFile = null;
                        MessageBox.Show("Неверный файл", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (fieldParser.EndOfData)
                    {
                        currentFile = null;
                        MessageBox.Show("Файл пуст", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // Пропустить строку названий столбцов
                        fieldParser.ReadFields();

                        while (!fieldParser.EndOfData)
                        {
                            try
                            {
                                clinicSource.Add(new Clinic(fieldParser.ReadFields()));
                            }
                            catch (Microsoft.VisualBasic.FileIO.MalformedLineException)
                            {
                                currentFile = null;
                                if (MessageBox.Show("Входной файл имел неверный формат. Открыть другой файл?", "Ошибка чтения файла", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                                    return;

                                OpenToolStripMenuItem_Click(this, EventArgs.Empty);
                                return;
                            }
                            catch (Exception ex)
                            {
                                currentFile = null;
                                MessageBox.Show(ex.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void ShowHideToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm(this).Show();

        private void BindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (clinicSource.List.Count <= 2)
            {
                clinicSource.Clear();
            }
            clinicSource.Add(new Clinic());
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                clinicSource.RemoveAt(ClinicView.CurrentRow.Index);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Невозможно удалить запись", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e) => await Save(currentFile);

        private async void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog { Filter = Resources.StartForm_SaveAsToolStripMenuItem_Click_Таблица_CSV_____csv })
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                await Save(saveDialog.FileName);
            }

        }

        private async System.Threading.Tasks.Task Save(string path, bool append = false)
        {
            if (fileOpened)
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var writer = new System.IO.StreamWriter(path, append, System.Text.Encoding.Unicode))
                {
                    foreach (var header in Headers)
                        await writer.WriteAsync($"{header};");

                    await writer.WriteLineAsync();

                    foreach (var item in clinicSource)
                    {
                        await writer.WriteLineAsync(item.ToString());
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if (MessageBox.Show("Файл не открыт. Открыть?", "Ошибка сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                    return;

                OpenToolStripMenuItem_Click(this, EventArgs.Empty);
                return;
            }
        }

        private void RowShowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var s = sender as NumericUpDown;
            for (int i = (int)s.Value; i < ClinicView.RowCount - 1; i++)
                сlinicView.Rows[i].Visible = false;
        }

        private void ClinicBindingSource_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            rowShowNumericUpDown.Value = rowShowNumericUpDown.Maximum = clinicSource.Count;
        }

        private void сlinicView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClinicView.BeginEdit(true);
        }

        private void СlinicView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                BindingNavigatorDeleteItem_Click(sender, EventArgs.Empty);
        }

        private void CloseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clinicSource.Clear();
            FileClosed();
        }
    }
}
