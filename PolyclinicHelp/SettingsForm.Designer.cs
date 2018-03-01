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
            this.ShowContent = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // ShowContent
            // 
            this.ShowContent.CheckOnClick = true;
            this.ShowContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowContent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowContent.FormattingEnabled = true;
            this.ShowContent.Location = new System.Drawing.Point(0, 0);
            this.ShowContent.MultiColumn = true;
            this.ShowContent.Name = "ShowContent";
            this.ShowContent.Size = new System.Drawing.Size(284, 261);
            this.ShowContent.TabIndex = 0;
            this.ShowContent.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ShowContent_ItemCheck);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ShowContent);
            this.Name = "SettingsForm";
            this.Text = "Столбцы";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ShowContent;
    }
}