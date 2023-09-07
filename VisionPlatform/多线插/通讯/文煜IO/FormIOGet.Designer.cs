namespace VisionPlatform
{
    partial class FormIOGet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIOGet));
            this.but_Save = new System.Windows.Forms.Button();
            this.tLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // but_Save
            // 
            resources.ApplyResources(this.but_Save, "but_Save");
            this.but_Save.Name = "but_Save";
            this.but_Save.UseVisualStyleBackColor = true;
            this.but_Save.Click += new System.EventHandler(this.but_Save_Click);
            // 
            // tLPanel
            // 
            resources.ApplyResources(this.tLPanel, "tLPanel");
            this.tLPanel.Name = "tLPanel";
            // 
            // FormIOGet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPanel);
            this.Controls.Add(this.but_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormIOGet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button but_Save;
        private System.Windows.Forms.TableLayoutPanel tLPanel;
    }
}