﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionPlatform
{
    public partial class Login : Form
    {
        FormMainUI mainUI;
        public Login(FormMainUI formMainUI)
        {
            mainUI = formMainUI;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入用户名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入密码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {


                    if (textBox1.Text == "111" && textBox2.Text == "111")
                    {
                        MessageBox.Show("登录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        FormUIConfig featureselection = new FormUIConfig();
                        featureselection.TopMost = true;
                        featureselection.ShowDialog();
                        this.Dispose();
                    }
                    else
                    {
                        if (textBox1.Text != "111")
                        {
                            MessageBox.Show("请检查用户名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (textBox1.Text == "111" && textBox2.Text != "111")
                            {
                                MessageBox.Show("密码错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox2.Text = "";
                            }
                        }
                    }
                }
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
