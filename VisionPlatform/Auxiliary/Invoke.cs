using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionPlatform.Auxiliary
{
    public static class Invoke
    {
        public static void Update(this ListView listView, List<string> source, SynchronizationContext context = null)
        {
            if (context == null)
            {
                if (listView.InvokeRequired)
                {
                    listView.BeginInvoke(new Action<List<string>>(x =>
                    {
                        Show(ref listView, source);
                    }), source);
                }
                else
                {
                    Show(ref listView, source);
                }
            }
            else
            {
                context?.Post(x =>
                {
                    if (x is ListView list)
                    {
                        Show(ref listView, source);
                    }
                }, listView);
            }
        }

        private static void Show(ref ListView listView, List<string> source)
        {
            try
            {
                int m = listView.Items.Count;
                listView.Items.Add(m.ToString());
                foreach (string str in source)
                {
                    listView.Items[m].SubItems.Add(str);
                }
                if (0 == source.Count)
                {
                    listView.Items[m].BackColor = Color.Red;
                }
                else
                {
                    if (source.Contains("OK"))
                    {
                        //listView.Items[m].BackColor = Color.Green;
                    }
                    else
                    {
                        listView.Items[m].BackColor = Color.Red;
                    }
                }
                listView.Items[listView.Items.Count - 1].EnsureVisible();
                if (listView.Items.Count > 100)
                {
                    listView.Items.Clear();
                }
                source.Clear();
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
    }
}
