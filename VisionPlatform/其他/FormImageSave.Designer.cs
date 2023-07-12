
namespace VisionPlatform
{
    partial class FormImageSave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageSave));
            this.tLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox_SaveResultImgOK = new System.Windows.Forms.CheckBox();
            this.checkBox_SaveOrgImageOK = new System.Windows.Forms.CheckBox();
            this.checkBox_SaveResultImgNG = new System.Windows.Forms.CheckBox();
            this.checkBox_SaveOrgImageNG = new System.Windows.Forms.CheckBox();
            this.checkBox_ShowData = new System.Windows.Forms.CheckBox();
            this.tLPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLPanel
            // 
            resources.ApplyResources(this.tLPanel, "tLPanel");
            this.tLPanel.Controls.Add(this.checkBox_SaveResultImgOK, 0, 2);
            this.tLPanel.Controls.Add(this.checkBox_SaveOrgImageOK, 0, 1);
            this.tLPanel.Controls.Add(this.checkBox_SaveResultImgNG, 0, 4);
            this.tLPanel.Controls.Add(this.checkBox_SaveOrgImageNG, 0, 3);
            this.tLPanel.Controls.Add(this.checkBox_ShowData, 0, 0);
            this.tLPanel.Name = "tLPanel";
            // 
            // checkBox_SaveResultImgOK
            // 
            resources.ApplyResources(this.checkBox_SaveResultImgOK, "checkBox_SaveResultImgOK");
            this.checkBox_SaveResultImgOK.Name = "checkBox_SaveResultImgOK";
            this.checkBox_SaveResultImgOK.UseVisualStyleBackColor = true;
            this.checkBox_SaveResultImgOK.CheckedChanged += new System.EventHandler(this.Save);
            // 
            // checkBox_SaveOrgImageOK
            // 
            resources.ApplyResources(this.checkBox_SaveOrgImageOK, "checkBox_SaveOrgImageOK");
            this.checkBox_SaveOrgImageOK.Name = "checkBox_SaveOrgImageOK";
            this.checkBox_SaveOrgImageOK.UseVisualStyleBackColor = true;
            this.checkBox_SaveOrgImageOK.CheckedChanged += new System.EventHandler(this.Save);
            // 
            // checkBox_SaveResultImgNG
            // 
            resources.ApplyResources(this.checkBox_SaveResultImgNG, "checkBox_SaveResultImgNG");
            this.checkBox_SaveResultImgNG.Name = "checkBox_SaveResultImgNG";
            this.checkBox_SaveResultImgNG.UseVisualStyleBackColor = true;
            this.checkBox_SaveResultImgNG.CheckedChanged += new System.EventHandler(this.Save);
            // 
            // checkBox_SaveOrgImageNG
            // 
            resources.ApplyResources(this.checkBox_SaveOrgImageNG, "checkBox_SaveOrgImageNG");
            this.checkBox_SaveOrgImageNG.Name = "checkBox_SaveOrgImageNG";
            this.checkBox_SaveOrgImageNG.UseVisualStyleBackColor = true;
            this.checkBox_SaveOrgImageNG.CheckedChanged += new System.EventHandler(this.Save);
            // 
            // checkBox_ShowData
            // 
            resources.ApplyResources(this.checkBox_ShowData, "checkBox_ShowData");
            this.checkBox_ShowData.Name = "checkBox_ShowData";
            this.checkBox_ShowData.UseVisualStyleBackColor = true;
            this.checkBox_ShowData.CheckedChanged += new System.EventHandler(this.Save);
            // 
            // FormImageSave
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormImageSave";
            this.Load += new System.EventHandler(this.FormImageSave_Load);
            this.tLPanel.ResumeLayout(false);
            this.tLPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPanel;
        private System.Windows.Forms.CheckBox checkBox_SaveResultImgOK;
        private System.Windows.Forms.CheckBox checkBox_SaveOrgImageOK;
        private System.Windows.Forms.CheckBox checkBox_SaveResultImgNG;
        private System.Windows.Forms.CheckBox checkBox_SaveOrgImageNG;
        private System.Windows.Forms.CheckBox checkBox_ShowData;
    }
}