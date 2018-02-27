namespace PolyclinicHelp
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ShowColumns = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ShowColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShowColumns
            // 
            this.ShowColumns.Controls.Add(this.checkBox1);
            this.ShowColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowColumns.Location = new System.Drawing.Point(0, 0);
            this.ShowColumns.Name = "ShowColumns";
            this.ShowColumns.Size = new System.Drawing.Size(284, 261);
            this.ShowColumns.TabIndex = 0;
            this.ShowColumns.TabStop = false;
            this.ShowColumns.Text = "Показывать столбцы";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "ROWNAME";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ShowColumns);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ShowColumns.ResumeLayout(false);
            this.ShowColumns.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ShowColumns;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}