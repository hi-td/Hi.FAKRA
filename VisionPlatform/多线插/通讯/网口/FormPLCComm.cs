using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Chustange.Functional;
using Chustange.Mould;
using Newtonsoft.Json;


namespace VisionPlatform
{
    public partial class FormPLCComm : Form
    {
        private SLMP _readSlmp;
        private SLMP _writeSlmp;

        private static Address M3000 = new Address { DataType = Edt.Bit, Index = 3000, SoftType = Est.M, Value = 0 };
        private static Address M3001 = new Address { DataType = Edt.Bit, Index = 3001, SoftType = Est.M, Value = 0 };
        private static Address M3002 = new Address { DataType = Edt.Bit, Index = 3002, SoftType = Est.M, Value = 0 };
        private static Address M3003 = new Address { DataType = Edt.Bit, Index = 3003, SoftType = Est.M, Value = 0 };
        private object lc_read = new object();
        private object lc_write = new object();
        static readonly List<Address> ReadAddresses = new List<Address>() { M3000, M3001 };

        List<object> ReadDatas = new List<object>();
        public FormPLCComm()
        {
            InitializeComponent();
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            if (btn_Read.Text == "读打开")
            {
                if (!string.IsNullOrWhiteSpace(txt_IpAddress.Text))
                {
                    if (short.TryParse(txt_ReadPort.Text, out var readPort))
                    {
                        _readSlmp = new SLMP(txt_IpAddress.Text.Trim(), readPort);
                        if (!_readSlmp.Connected)
                        {
                            _readSlmp.Open();
                        }

                        Task.Delay(1).Wait();
                        if (_readSlmp.Connected)
                        {
                            new Task(Read, TaskCreationOptions.LongRunning).Start();
                            //TMData_Serializer._globalData.SlmpPara.IpAddress = txt_IpAddress.Text;
                            //TMData_Serializer._globalData.SlmpPara.ReadPort = readPort;
                            btn_Read.Text = "读关闭";
                        }

                    }
                }
                "as".ToLog();
            }
            else
            {
                if (_readSlmp.Connected)
                {
                    _readSlmp.Close();
                }

                btn_Read.Text = "读打开";
            }
        }

        private void Read()
        {
            var ReadListAddress = new List<Address> { M3000, M3001 };
            if (_readSlmp == null) return;
            while (_readSlmp.Connected)
            {
                lock (lc_read)
                {
                    _readSlmp.ReadDeviceRandom(ReadListAddress, ref ReadDatas);
                    if (ReadDatas != null)
                    {
                        if (ReadDatas.Count == ReadListAddress.Count)
                        {
                            lbl_M100.Text(ReadDatas[0].HasValue().ToString());
                            lbl_M101.Text(ReadDatas[1].HasValue().ToString());
                        }
                    }
                }

                Task.Delay(1).Wait();
            }
        }


        private void btn_Write_Click(object sender, EventArgs e)
        {
            if (btn_Write.Text == "写打开")
            {
                if (!string.IsNullOrWhiteSpace(txt_IpAddress.Text))
                {
                    if (short.TryParse(txt_WritePort.Text, out var writeResult))
                    {
                        _writeSlmp = new SLMP(txt_IpAddress.Text.Trim(), writeResult);
                        if (!_writeSlmp.Connected)
                        {
                            _writeSlmp.Open();
                        }

                        Task.Delay(1).Wait();
                        if (_writeSlmp.Connected)
                        {
                            //TMData_Serializer._globalData.SlmpPara.IpAddress = txt_IpAddress.Text;
                            //TMData_Serializer._globalData.SlmpPara.WritePort = writeResult;
                            btn_Write.Text = "写关闭";
                        }

                    }
                }
            }
            else
            {
                if (_writeSlmp.Connected)
                {
                    _writeSlmp.Close();
                }

                btn_Write.Text = "写打开";
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
                MessageBox.Show(error.ToString());
            }
        }

        private void btn_M102_Click(object sender, EventArgs e)
        {
            if (_writeSlmp == null) return;
            if (_writeSlmp.Connected)
            {
                lock (lc_write)
                {
                    if (!short.TryParse(txt_M102.Text, out var m102))
                    {
                        MessageBox.Show("当前输入值类型有误，请检查!（只能输入数值！）");
                        return;
                    }

                    if (m102 < 0 || m102 > 1)
                    {
                        MessageBox.Show("当前输入值不合法，请检查！（只能输入0或1）");
                        return;
                    }

                    M3002.Value = m102;
                    var listM102 = new List<Address> { M3002 };
                    _writeSlmp.WriteDevie(M3002);
                }
            }
        }

        private void btn_M103_Click(object sender, EventArgs e)
        {
            if (_writeSlmp == null) return;
            if (_writeSlmp.Connected)
            {
                lock (lc_write)
                {
                    if (!short.TryParse(txt_M103.Text, out var m103))
                    {
                        MessageBox.Show("当前输入值类型有误，请检查!（只能输入数值！）");
                        return;
                    }

                    if (m103 < 0 || m103 > 1)
                    {
                        MessageBox.Show("当前输入值不合法，请检查！（只能输入0或1）");
                        return;
                    }

                    M3003.Value = m103;
                    var listM103 = new List<Address> { M3003 };
                    _writeSlmp.WriteDevie(M3003);
                }
            }
        }
    }
}
