
namespace VisionPlatform
{
    partial class Show3
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Show3));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tLPanel_CamShow = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tLPanel_resultShow = new System.Windows.Forms.TableLayoutPanel();
            this.but_Run = new System.Windows.Forms.Button();
            this.tLPanel_ImageSave = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Message = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tLPanel_CamShow.SuspendLayout();
            this.tLPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tLPanel_CamShow, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tLPanel, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tLPanel_CamShow
            // 
            resources.ApplyResources(this.tLPanel_CamShow, "tLPanel_CamShow");
            this.tLPanel_CamShow.Controls.Add(this.panel1, 0, 0);
            this.tLPanel_CamShow.Controls.Add(this.panel2, 1, 0);
            this.tLPanel_CamShow.Controls.Add(this.panel3, 2, 0);
            this.tLPanel_CamShow.Name = "tLPanel_CamShow";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // tLPanel
            // 
            resources.ApplyResources(this.tLPanel, "tLPanel");
            this.tLPanel.Controls.Add(this.tLPanel_resultShow, 1, 0);
            this.tLPanel.Controls.Add(this.but_Run, 0, 0);
            this.tLPanel.Controls.Add(this.tLPanel_ImageSave, 3, 0);
            this.tLPanel.Controls.Add(this.panel_Message, 2, 0);
            this.tLPanel.Name = "tLPanel";
            // 
            // tLPanel_resultShow
            // 
            resources.ApplyResources(this.tLPanel_resultShow, "tLPanel_resultShow");
            this.tLPanel_resultShow.Name = "tLPanel_resultShow";
            // 
            // but_Run
            // 
            this.but_Run.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.but_Run, "but_Run");
            this.but_Run.Name = "but_Run";
            this.but_Run.UseVisualStyleBackColor = false;
            this.but_Run.Click += new System.EventHandler(this.but_Run_Click);
            // 
            // tLPanel_ImageSave
            // 
            resources.ApplyResources(this.tLPanel_ImageSave, "tLPanel_ImageSave");
            this.tLPanel_ImageSave.Name = "tLPanel_ImageSave";
            // 
            // panel_Message
            // 
            resources.ApplyResources(this.panel_Message, "panel_Message");
            this.panel_Message.Name = "panel_Message";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // Show3
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Show3";
            this.Load += new System.EventHandler(this.Show3_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tLPanel_CamShow.ResumeLayout(false);
            this.tLPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TableLayoutPanel tLPanel_CamShow;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tLPanel;
        public System.Windows.Forms.Button but_Run;
        private System.Windows.Forms.TableLayoutPanel tLPanel_resultShow;
        private System.Windows.Forms.TableLayoutPanel tLPanel_ImageSave;
        public System.Windows.Forms.Panel panel_Message;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}