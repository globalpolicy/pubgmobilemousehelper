namespace PUBG_Mouse_Helper
{
    partial class SavePresetAsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavePresetAsDialog));
            this.textBoxPresetName = new System.Windows.Forms.TextBox();
            this.buttonOkSave = new System.Windows.Forms.Button();
            this.buttonCancelSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPresetName
            // 
            this.textBoxPresetName.Location = new System.Drawing.Point(12, 12);
            this.textBoxPresetName.Name = "textBoxPresetName";
            this.textBoxPresetName.Size = new System.Drawing.Size(243, 20);
            this.textBoxPresetName.TabIndex = 0;
            this.textBoxPresetName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPresetName_KeyDown);
            // 
            // buttonOkSave
            // 
            this.buttonOkSave.Location = new System.Drawing.Point(44, 38);
            this.buttonOkSave.Name = "buttonOkSave";
            this.buttonOkSave.Size = new System.Drawing.Size(75, 23);
            this.buttonOkSave.TabIndex = 1;
            this.buttonOkSave.Text = "Ok";
            this.buttonOkSave.UseVisualStyleBackColor = true;
            this.buttonOkSave.Click += new System.EventHandler(this.buttonOkSave_Click);
            // 
            // buttonCancelSave
            // 
            this.buttonCancelSave.Location = new System.Drawing.Point(151, 38);
            this.buttonCancelSave.Name = "buttonCancelSave";
            this.buttonCancelSave.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelSave.TabIndex = 2;
            this.buttonCancelSave.Text = "Cancel";
            this.buttonCancelSave.UseVisualStyleBackColor = true;
            this.buttonCancelSave.Click += new System.EventHandler(this.buttonCancelSave_Click);
            // 
            // SavePresetAsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 73);
            this.Controls.Add(this.buttonCancelSave);
            this.Controls.Add(this.buttonOkSave);
            this.Controls.Add(this.textBoxPresetName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SavePresetAsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter preset name";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPresetName;
        private System.Windows.Forms.Button buttonOkSave;
        private System.Windows.Forms.Button buttonCancelSave;
    }
}