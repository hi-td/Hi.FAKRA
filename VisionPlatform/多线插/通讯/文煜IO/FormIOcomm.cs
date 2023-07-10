using Newtonsoft.Json;
using StaticFun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VisionPlatform.TMData;
using WENYU_IO;

namespace VisionPlatform
{
    public partial class FormIOcomm : Form
    {
        public FormIOcomm()
        {
            InitializeComponent();
        }

        private void ScanInPutPort()
        {
            int Result;
            int value = 0;
            Result = WENYU_PIO32P.WY_ReadInPutbit0(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton00.BackColor = Color.Green;
                else INcircleButton00.BackColor = Color.Transparent;
            }
            else
            {
                timer1.Enabled = false;
                MessageBox.Show("板卡连接失败！", "提示", MessageBoxButtons.OK);
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit1(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton01.BackColor = Color.Green;
                else INcircleButton01.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit2(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton02.BackColor = Color.Green;
                else INcircleButton02.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit3(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton03.BackColor = Color.Green;
                else INcircleButton03.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit4(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton04.BackColor = Color.Green;
                else INcircleButton04.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit5(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton05.BackColor = Color.Green;
                else INcircleButton05.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit6(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton06.BackColor = Color.Green;
                else INcircleButton06.BackColor = Color.Transparent;
            }
            Result = WENYU_PIO32P.WY_ReadInPutbit7(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) INcircleButton07.BackColor = Color.Green;
                else INcircleButton07.BackColor = Color.Transparent;
            }

        }
        private void ScanOutPutPort()
        {

            int Result;
            int value = 0;
            Result = WENYU_PIO32P.WY_ReadOutPutBit7(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton07.BackColor = Color.Red;
                else OutcircleButton07.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit6(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton06.BackColor = Color.Red;
                else OutcircleButton06.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit5(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton05.BackColor = Color.Red;
                else OutcircleButton05.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit4(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton04.BackColor = Color.Red;
                else OutcircleButton04.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit3(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton03.BackColor = Color.Red;
                else OutcircleButton03.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit2(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton02.BackColor = Color.Red;
                else OutcircleButton02.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit1(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton01.BackColor = Color.Red;
                else OutcircleButton01.BackColor = Color.Transparent;
            }


            Result = WENYU_PIO32P.WY_ReadOutPutBit0(WENYU.DevID, ref value);
            if (Result == 0)
            {
                if (value == 0) OutcircleButton00.BackColor = Color.Red;
                else OutcircleButton00.BackColor = Color.Transparent;
            }
        }

        private void FormIOcomm_Load(object sender, EventArgs e)
        {

            timer1.Interval = 20;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void OutcircleButton00_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton00.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit0(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit0(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton01_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton01.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit1(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit1(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton02_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton02.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit2(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit2(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton03_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton03.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit3(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit3(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton04_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton04.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit4(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit4(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton05_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton05.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit5(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit5(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton06_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton06.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit6(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit6(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void OutcircleButton07_Click(object sender, EventArgs e)
        {
            int Result;
            if (OutcircleButton07.BackColor == Color.Red)
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit7(WENYU.DevID, 1);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                Result = WENYU_PIO32P.WY_WriteOutPutBit7(WENYU.DevID, 0);
                if (Result != 0)
                {
                    MessageBox.Show("函数返回值错误！", "提示", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScanInPutPort();
            ScanOutPutPort();
        }

        private void FormIOcomm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
