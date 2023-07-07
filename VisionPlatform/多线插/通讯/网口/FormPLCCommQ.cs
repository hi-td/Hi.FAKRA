using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MC.Enumerate;
using Chustange.Functional;
using Newtonsoft.Json;
using MC;
using System.Threading;

namespace VisionPlatform
{
    public partial class FormPLCCommQ : Form
    {
        public FormPLCCommQ()
        {
            InitializeComponent();
        }
        private object lc_read = new object();
        Melsec melsec = new Melsec();
        MC.Address M3000 = new MC.Address(SoftType.M, 300, DataType.Bit, null, 0, 20);

        MC.Address M300 = new MC.Address(SoftType.M, 300, DataType.Bit, false);
        MC.Address M301 = new MC.Address(SoftType.M, 301, DataType.Bit, true);
        MC.Address M302 = new MC.Address(SoftType.M, 302, DataType.Bit, true);
        MC.Address M305 = new MC.Address(SoftType.M, 305, DataType.Bit, false);
        MC.Address M306 = new MC.Address(SoftType.M, 306, DataType.Bit, true);
        MC.Address M307 = new MC.Address(SoftType.M, 307, DataType.Bit, true);
        MC.Address M310 = new MC.Address(SoftType.M, 310, DataType.Bit, false);
        MC.Address M311 = new MC.Address(SoftType.M, 311, DataType.Bit, true);
        MC.Address M312 = new MC.Address(SoftType.M, 312, DataType.Bit, true);
        MC.Address M315 = new MC.Address(SoftType.M, 315, DataType.Bit, false);
        MC.Address M316 = new MC.Address(SoftType.M, 316, DataType.Bit, true);
        MC.Address M317 = new MC.Address(SoftType.M, 317, DataType.Bit, true);
        public enum ReadIndex
        {
            M300 = 0,
            M301,
            M302,
            M305 = 5,
            M306,
            M307,
            M310 = 10,
            M311,
            M312,
            M315 = 15,
            M316,
            M317
        }
        private void btn_Read_Click(object sender, EventArgs e)
        {
            if (btn_Read.Text == "读打开")
            {
                if (!string.IsNullOrWhiteSpace(txt_IpAddress.Text))
                {
                    if (short.TryParse(txt_ReadPort.Text, out var readPort))
                    {
                        melsec = new Melsec(txt_IpAddress.Text.Trim(), readPort);
                        if (!melsec.Connected)
                        {
                            melsec.Open();
                        }

                        Task.Delay(1).Wait();
                        if (melsec.Connected)
                        {
                            new Task(Read, TaskCreationOptions.LongRunning).Start();
                            //TMData_Serializer._globalData.SlmpPara.IpAddress = txt_IpAddress.Text;
                            //TMData_Serializer._globalData.SlmpPara.ReadPort = readPort;
                            btn_Read.Text = "读关闭";
                        }

                    }
                }
            }
            else
            {
                if (melsec.Connected)
                {
                    melsec.Close();
                }

                btn_Read.Text = "读打开";
            }
        }
        private void Read()
        {
            if (melsec == null) return;
            while (melsec.Connected)
            {
                lock (lc_read)
                {
                    var rs = melsec.ReadDeviceBatchBit(M3000);
                    if (rs?.Count == 20)
                    {
                        //前芯
                        if (rs[(int)ReadIndex.M300])
                        {
                            lbl_M300.Text("true");
                        }
                        else
                        {
                            lbl_M300.Text("false");
                        }
                        //后芯
                        if (rs[(int)ReadIndex.M305])
                        {
                            lbl_M305.Text("true");
                        }
                        else
                        {
                            lbl_M305.Text("false");
                        }
                        //前端
                        if (rs[(int)ReadIndex.M310])
                        {
                            lbl_M310.Text("true");
                        }
                        else
                        {
                            lbl_M310.Text("false");
                        }
                        //后端
                        if (rs[(int)ReadIndex.M315])
                        {
                            lbl_M310.Text("true");
                        }
                        else
                        {
                            lbl_M315.Text("false");
                        }
                    }
                    Thread.Sleep(10);
                }
                Task.Delay(1).Wait();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                TMData.SlmpPara param = new TMData.SlmpPara();
                param.IpAddress = txt_IpAddress.Text;
                param.ReadPort = int.Parse(txt_ReadPort.Text);
                param.WritePort = int.Parse(txt_WritePort.Text);
                //TMData_Serializer._globalData.SlmpPara = param;
                //导入序列化参数
                var json = JsonConvert.SerializeObject(param);
                string tup_Path = System.AppDomain.CurrentDomain.BaseDirectory + "PLCpz.json";
                System.IO.File.WriteAllText(tup_Path, json);
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SystemException error)
            {
                MessageBox.Show("保存失败！"+error.ToString());
            }
        }

        private void FormPLCCommQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            melsec.Close();
        }
    }
}
