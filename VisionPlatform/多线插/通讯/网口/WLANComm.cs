using Chustange.Functional;
using MC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MC.Enumerate;

namespace WLANComm
{
    public class MITSUBISHI
    {
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
        public void Banka()
        {
            bool bStart = true;
            object lc_read = new object();
            int count = 0;
            bool BTF1 = false;
            bool BTF2 = false;
            bool BTF3 = false;
            bool BTF4 = false;
            int b_cam1out = -1;
            int b_cam2out = -1;
            int b_cam3out = -1;
            int b_cam4out = -1;
            bool b_cam1 = false;
            bool b_cam2 = false;
            bool b_cam3 = false;
            bool b_cam4 = false;
            try
            {
                melsec = new Melsec
                (
                // TMData_Serializer._PLC.SlmpPara.IpAddress,
                //TMData_Serializer._PLC.SlmpPara.ReadPort
                );
                melsec.DataBufferTime = 40;
                melsec.Open();
                while (bStart)
                {
                    lock (lc_read)
                    {
                        if (melsec.Connected)
                        {
                            var rs = melsec.ReadDeviceBatchBit(M3000);
                            if (rs?.Count == 20)
                            {
                                count = 0;
                                //前芯
                                if (!rs[(int)ReadIndex.M300])
                                {
                                    BTF1 = false;
                                }
                                if (rs[(int)ReadIndex.M300])
                                {
                                    melsec.WriteDeviceSingleBit(M300);
                                }
                                if (rs[(int)ReadIndex.M300] && !BTF1)
                                {
                                    BTF1 = true;
                                    if (!b_cam1)
                                    {
                                        b_cam1 = true;
                                    }
                                }
                                //后芯
                                if (!rs[(int)ReadIndex.M305])
                                {
                                    BTF2 = false;
                                }
                                if (rs[(int)ReadIndex.M305])
                                {
                                    melsec.WriteDeviceSingleBit(M305);
                                }
                                if (rs[(int)ReadIndex.M305] && !BTF2)
                                {
                                    BTF2 = true;
                                    if (!b_cam2)
                                    {
                                        b_cam2 = true;
                                    }
                                }
                                //前端
                                if (!rs[(int)ReadIndex.M310])
                                {
                                    BTF3 = false;
                                }
                                if (rs[(int)ReadIndex.M310])
                                {
                                    melsec.WriteDeviceSingleBit(M310);
                                }
                                if (rs[(int)ReadIndex.M310] && !BTF3)
                                {
                                    BTF3 = true;
                                    if (!b_cam3)
                                    {
                                        b_cam3 = true;
                                    }
                                }
                                //后端
                                if (!rs[(int)ReadIndex.M315])
                                {
                                    BTF4 = false;
                                }
                                if (rs[(int)ReadIndex.M315])
                                {
                                    melsec.WriteDeviceSingleBit(M315);
                                }
                                if (rs[(int)ReadIndex.M315] && !BTF4)
                                {
                                    BTF4 = true;
                                    if (!b_cam4)
                                    {
                                        b_cam4 = true;
                                    }
                                }


                                if (b_cam1out == 1)
                                {
                                    //如果产品OK,给PLC发送信号
                                    b_cam1out = -1;
                                    melsec.WriteDeviceSingleBit(M301);
                                }
                                else if (b_cam1out == 0)
                                {
                                    b_cam1out = -1;
                                    //如果产品NG,给PLC发送信号
                                    melsec.WriteDeviceSingleBit(M302);
                                }
                                if (b_cam2out == 1)
                                {
                                    b_cam2out = -1;
                                    melsec.WriteDeviceSingleBit(M306);
                                }
                                else if (b_cam2out == 0)
                                {
                                    b_cam2out = -1;
                                    melsec.WriteDeviceSingleBit(M307);
                                }
                                if (b_cam3out == 1)
                                {
                                    //如果产品OK,给PLC发送信号
                                    b_cam3out = -1;
                                    melsec.WriteDeviceSingleBit(M311);
                                }
                                else if (b_cam3out == 0)
                                {
                                    b_cam3out = -1;
                                    //如果产品NG,给PLC发送信号
                                    melsec.WriteDeviceSingleBit(M312);
                                }
                                if (b_cam4out == 1)
                                {
                                    //如果产品OK,给PLC发送信号
                                    b_cam4out = -1;
                                    ////如果产品OK,给PLC发送信号
                                    melsec.WriteDeviceSingleBit(M316);
                                }
                                else if (b_cam4out == 0)
                                {
                                    b_cam4out = -1;
                                    //如果产品NG,给PLC发送信号
                                    melsec.WriteDeviceSingleBit(M317);
                                }
                            }
                            else
                            {
                                "读取信号有误。".ToLog();
                            }
                        }
                        if (!bStart)
                        {
                            melsec.Close();
                            break;
                        }
                    }
                    Thread.Sleep(2);
                }
            }
            //Thread.Sleep(10);
            catch (SystemException error)
            {
                ("[FormHome]->[Auto]:/t" + error.Message + Environment.NewLine + error.StackTrace).ToLog();
                if (count < 3)
                {
                    if (melsec.Connected)
                    {
                        melsec.Close();
                    }
                    Thread.Sleep(100);
                    Banka();
                }
                //else
                //{
                //    isAuto = false;
                //    but_Run.Text = "运行";
                //    but_Run.BackColor = default;
                //    MessageBox.Show("通讯出错，请重新运行", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                count++;
            }
        }
    }
}
