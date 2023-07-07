﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StaticFun;

namespace VisionPlatform
{
    public partial class FormLED : Form
    {
        public FormLED()
        {
            InitializeComponent();
            InitUI();
        }
        private void InitUI()
        {
            //COM口
            string[] strPort = SerialPort.GetPortNames();
            if (strPort.Length > 0)
            {
                foreach (var item in strPort)
                {
                    this.cbx_portName.Items.Add(item);
                }
                this.cbx_portName.SelectedIndex = 0;
            }
            //波特率
            this.cbx_baudRate.Items.Add(9600);
            this.cbx_baudRate.Items.Add(19200);
            this.cbx_baudRate.Items.Add(38400);
            this.cbx_baudRate.SelectedIndex = 1;

            //校验位
            this.cbx_parityBit.Items.Add("无校验");
            this.cbx_parityBit.Items.Add("奇校验");
            this.cbx_parityBit.Items.Add("偶校验");
            this.cbx_parityBit.SelectedIndex = 0;

            //数据位
            this.tbx_dataBit.Items.Add(8);
            this.tbx_dataBit.Items.Add(7);
            this.tbx_dataBit.Items.Add(6);
            this.tbx_dataBit.SelectedIndex = 0;
            //停止位
            //this.cmb_stopBits.Items.Add(0);
            this.cbx_stopBit.Items.Add(1);
            //this.cmb_stopBits.Items.Add(1.5);
            this.cbx_stopBit.Items.Add(2);
            this.cbx_stopBit.SelectedIndex = 0;
            //ComDevice.DataReceived += new SerialDataReceivedEventHandler(ComDevice_DataReceived);
        }

        private TMData.LEDRTU InitParam()
        {
            TMData.LEDRTU param = new TMData.LEDRTU();
            try
            {
                int iparity = 0;
                if (this.cbx_parityBit.Text == "无校验")
                {
                    iparity = (int)Parity.None;
                }
                else if (this.cbx_parityBit.Text == "奇校验")
                {
                    iparity = (int)Parity.Odd;
                }
                else if (this.cbx_parityBit.Text == "偶校验")
                {
                    iparity = (int)Parity.Even;
                }

                int iStopBits = 0;
                if (this.cbx_stopBit.Text == "0")
                {
                    iStopBits = (int)StopBits.None;
                }
                else if (this.cbx_stopBit.Text == "1")
                {
                    iStopBits = (int)StopBits.One;
                }
                else if (this.cbx_stopBit.Text == "1.5")
                {
                    iStopBits = (int)StopBits.OnePointFive;
                }
                else if (this.cbx_stopBit.Text == "2")
                {
                    iStopBits = (int)StopBits.Two;
                }
                param.BaudRate = int.Parse(cbx_baudRate.Text);
                param.PortName = cbx_portName.Text;
                param.DataBits = int.Parse(tbx_dataBit.Text);
                param.parity = (Parity)iparity;
                param.stopBits = (StopBits)iStopBits;
                
            }
            catch (SystemException error)
            {

            }
            return param;
        }
        private void ComDevice_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //接收反回数据
            //byte[] ReDatas = new byte[ComDevice.BytesToRead];//返回命令包
            //ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据             
            ////ASCII码显示
            //UpdateRecevie(System.Text.Encoding.Default.GetString(ReDatas));                
            //UpdateReceiveCount(ReDatas.Length);
            //不接受返回数据
           // ComDevice.DiscardInBuffer();
        }

        private void btn_openPort_Click(object sender, EventArgs e)
        {
            //刷新配置
            if (lbl_statu.Text == "已打开")
            {
                DialogResult dr = MessageBox.Show("串口已打开，是否重新设置？", "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                LEDSet.CloseLED();
            }
            try
            {
                LEDSet.OpenLedcom(InitParam());
                lbl_statu.Text = "已打开";
                lbl_statu.ForeColor = Color.Green;
                MessageBox.Show("重新设置并打开串口成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开串口失败:" + ex.ToString());
                return;
            }
           
        }

        private void btn_closePort_Click(object sender, EventArgs e)
        {
            try
            {
                LEDSet.CloseLED();
                lbl_statu.Text = "未打开";
                lbl_statu.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void FormLED_Load(object sender, EventArgs e)
        {
            LoadLED();
        }

        private void LoadLED()
        {
            try
            {
                if (LEDSet.isOpen)
                {
                    lbl_statu.Text = "已打开";
                    lbl_statu.ForeColor = Color.Green;
                }
                else
                {
                    lbl_statu.Text = "未打开";
                    lbl_statu.ForeColor = Color.Red;
                }
                TMData.LEDRTU LedRTU = TMData_Serializer._COMConfig.Led;
                cbx_baudRate.Text = LedRTU.BaudRate.ToString();
                cbx_portName.Text = LedRTU.PortName;
                tbx_dataBit.Text = LedRTU.DataBits.ToString();
                if (Parity.None == LedRTU.parity)
                {
                    cbx_parityBit.Text = "无校验";
                }
                else if (Parity.Odd == LedRTU.parity)
                {
                    cbx_parityBit.Text = "奇校验";
                }
                else if (Parity.Even == LedRTU.parity)
                {
                    cbx_parityBit.Text = "偶校验";
                }
                if (StopBits.One == LedRTU.stopBits)
                {
                    cbx_stopBit.Text = "1";
                }
                else if (StopBits.Two == LedRTU.stopBits)
                {
                    cbx_stopBit.Text = "2";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(TMData_Serializer._COMConfig.Led.PortName !=null)
            {
                DialogResult dr = MessageBox.Show("是否更新光源控制器串口配置文件？", "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }
            TMData_Serializer._COMConfig.Led = InitParam();
            var json = JsonConvert.SerializeObject(TMData_Serializer._COMConfig);
            System.IO.File.WriteAllText(GlobalPath.SavePath.IOPath, json);
            MessageBox.Show("更新并保存成功！");
            StaticFun.MessageFun.ShowMessage("光源控制器串口配置数据保存成功！");
        }
    }
}
