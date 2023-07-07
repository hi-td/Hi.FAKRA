
namespace VisionPlatform
{
    partial class FormSTATS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSTATS));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_ItemName = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清除数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.text_OK = new System.Windows.Forms.TextBox();
            this.text_NG = new System.Windows.Forms.TextBox();
            this.text_Total = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.text_Yield = new System.Windows.Forms.TextBox();
            this.label_TimeSpan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label_ItemName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label_ItemName
            // 
            resources.ApplyResources(this.label_ItemName, "label_ItemName");
            this.label_ItemName.Name = "label_ItemName";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel8.ContextMenuStrip = this.contextMenuStrip1;
            this.tableLayoutPanel8.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.text_OK, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.text_NG, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.text_Total, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.label11, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.text_Yield, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.label_TimeSpan, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除数据});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // 清除数据
            // 
            resources.ApplyResources(this.清除数据, "清除数据");
            this.清除数据.Name = "清除数据";
            this.清除数据.Click += new System.EventHandler(this.清除数据_Click);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // text_OK
            // 
            this.text_OK.BackColor = System.Drawing.Color.LightGreen;
            resources.ApplyResources(this.text_OK, "text_OK");
            this.text_OK.Name = "text_OK";
            this.text_OK.ReadOnly = true;
            // 
            // text_NG
            // 
            this.text_NG.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.text_NG, "text_NG");
            this.text_NG.Name = "text_NG";
            this.text_NG.ReadOnly = true;
            // 
            // text_Total
            // 
            resources.ApplyResources(this.text_Total, "text_Total");
            this.text_Total.Name = "text_Total";
            this.text_Total.ReadOnly = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // text_Yield
            // 
            this.text_Yield.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.text_Yield, "text_Yield");
            this.text_Yield.Name = "text_Yield";
            this.text_Yield.ReadOnly = true;
            // 
            // label_TimeSpan
            // 
            resources.ApplyResources(this.label_TimeSpan, "label_TimeSpan");
            this.label_TimeSpan.Name = "label_TimeSpan";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FormSTATS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSTATS";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_ItemName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除数据;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox text_OK;
        private System.Windows.Forms.TextBox text_NG;
        private System.Windows.Forms.TextBox text_Total;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox text_Yield;
        private System.Windows.Forms.Label label_TimeSpan;
        private System.Windows.Forms.Label label1;
    }
}