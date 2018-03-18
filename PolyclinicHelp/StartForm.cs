using System;
using System.ComponentModel;
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

        #region ToolStripMenuItem_Clicks

        /// <summary>
        /// Открывает файл и добавляет элементы в источник данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog
            {
                Filter = Resources.StartForm_OpenToolStripMenuItem_Click_CSV_____csv
            })
            {
                if (openDialog.ShowDialog() != DialogResult.OK)
                    return;

                FileOpenedOrCreated(openDialog.FileName);

                using (var fieldParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(openDialog.FileName))
                {
                    if (fieldParser.TextFieldType == Microsoft.VisualBasic.FileIO.FieldType.Delimited)
                        fieldParser.SetDelimiters(";");
                    else
                    {
                        currentFile = null;
                        MessageBox.Show(Resources.StartForm_OpenToolStripMenuItem_Click_Invalid_File, Resources.StartForm_OpenToolStripMenuItem_Click_Error_Reading_File, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (fieldParser.EndOfData)
                    {
                        currentFile = null;
                        MessageBox.Show(Resources.StartForm_OpenToolStripMenuItem_Click_Empty_File, Resources.StartForm_OpenToolStripMenuItem_Click_Error_Reading_File, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                FileClosed();

                                if (MessageBox.Show(Resources.StartForm_OpenToolStripMenuItem_Click_Invalid_File__Open_Another, Resources.StartForm_OpenToolStripMenuItem_Click_Error_Reading_File, MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                                    return;

                                OpenToolStripMenuItem_Click(this, EventArgs.Empty);
                                return;
                            }
                            catch (Exception ex)
                            {
                                FileClosed();

                                MessageBox.Show(ex.Message, Resources.StartForm_OpenToolStripMenuItem_Click_Error_Reading_File, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Закрывает форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Отркывает форму показа столбцов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideToolStripMenuItem_Click(object sender, EventArgs e) => new SettingsForm(this).Show();

        private async void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile != null)
                await Save(currentFile);
            else
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private async void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog { Filter = Resources.StartForm_SaveAsToolStripMenuItem_Click_Таблица_CSV_____csv })
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                await Save(saveDialog.FileName);
            }

        }

        private async void дописатьВToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog { Filter = Resources.StartForm_OpenToolStripMenuItem_Click_CSV_____csv })
            {
                if (save.ShowDialog() == DialogResult.OK)
                    await Save(currentFile = save.FileName, true, false);
            }

            OpenToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Очищает источник данных и прячет сетку и некоторые другие элементы формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clinicSource.Clear();
            FileClosed();
        }
        #endregion

        #region BindingNavigator_Clicks

        private void BindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            clinicSource.Add(new Clinic { Rownum = clinicSource.List.Count + 1, PostalCode = 123456 });
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClinicView.CurrentRow != null && ClinicView.CurrentRow.Index != 0)
                    clinicSource.RemoveAt(ClinicView.CurrentRow.Index);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Невозможно удалить запись", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        /// <summary>
        /// Подготавливает форму для отображения данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenedOrCreated(object sender = null, EventArgs e = null)
        {
            ShowHideToolStripMenuItem.Enabled = true;
            fileOpened = true;
            bindingNavigator.Visible = true;
            ClinicView.Visible = true;
            currentFile = sender as string;
            clinicSource.Clear();
        }

        /// <summary>
        /// Подготавливает форму для очистки данных.
        /// </summary>
        private void FileClosed()
        {
            currentFile = null;
            ShowHideToolStripMenuItem.Enabled = false;
            fileOpened = false;
            bindingNavigator.Visible = false;
            ClinicView.Visible = false;
        }

        /// <summary>
        /// Обеспечивает сохранение в файл тремя способами.
        /// </summary>
        /// <param name="path">Полный путь до файла для записи.</param>
        /// <param name="append">true для добавления данных в файл, false для перезаписи.</param>
        /// <param name="writeHeaders"></param>
        /// <returns>Задача, представляющая асинхронную операцию записи в файл.</returns>
        private async System.Threading.Tasks.Task Save(string path, bool append = false, bool writeHeaders = true)
        {
            if (fileOpened)
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var writer = new System.IO.StreamWriter(path, append, System.Text.Encoding.Unicode))
                {
                    if (writeHeaders)
                    {
                        foreach (var header in Headers)
                            await writer.WriteAsync($"{header};");

                        await writer.WriteLineAsync();
                    }

                    foreach (var item in clinicSource)
                    {
                        await writer.WriteLineAsync(item.ToString());
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                if (MessageBox.Show(Resources.StartForm_Save_File_Not_Opened__Openь_, Resources.StartForm_Save_Saving_Error, MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                    return;

                OpenToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }

        private void СlinicView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            ((Clinic)clinicSource.Current)[e.ColumnIndex] = e.Value;
            // ToDO
        }



        private void СlinicView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // If the data source raises an exception when a cell value is 
            // commited, display an error message.
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message);
            }
        }

        private void FindClosestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clinicSource.Current is Clinic current)
            {
                var min = double.MaxValue;
                var index = 0;
                for (var i = 0; i < clinicSource.Count; i++)
                {
                    var clinic = (Clinic)clinicSource[i];
                    var distance = current.Address.Distance(clinic.Address.PointX, clinic.Address.PointY);
                    if (clinic != current && distance < min)
                    {
                        min = distance;
                        index = i;
                    }
                }

                ClinicView.FirstDisplayedScrollingRowIndex = index;
                ClinicView.Rows[index].Selected = true;
            }
        }

        private void сlinicView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (ClinicView.Columns[e.ColumnIndex].HeaderText == "Address")
            {
                ClinicView.CancelEdit();
                new AddressBuilder(this).ShowDialog();
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                for (var i = 1; i < ClinicView.RowCount; i++)
                {
                    if (((string)ClinicView.Rows[i].Cells[15].Value).Contains(
                        ((ToolStripTextBox)sender).Text))
                        ClinicView.Rows[i].Visible = true;
                    else
                        ClinicView.Rows[i].Visible = false;
                }
            }
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (var i = 1; i < ClinicView.RowCount; i++)
                {
                    if (((Address)ClinicView.Rows[i].Cells[3].Value).District.Contains(
                        ((ToolStripTextBox)sender).Text))
                        ClinicView.Rows[i].Visible = true;
                    else
                        ClinicView.Rows[i].Visible = false;
                }
            }
        }

        private void postalCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clinicSource.ApplySort(TypeDescriptor.GetProperties(ClinicView.Columns["PostalCode"])[0], ListSortDirection.Ascending);
        }
    }
}
