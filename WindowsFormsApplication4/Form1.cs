using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog fb;
        bool isCapture;
        public Form1()
        {
            InitializeComponent();
            fb = new FolderBrowserDialog();
            isCapture = false;
            timer1.Interval = (int)numericUpDown1.Value * 60000;
        }

        private void CaptureImage()
        {
            DateTime currentTime = DateTime.Now;
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bounds.Size);
                }
                string datePath = String.Format("{0}_{1}_{2}", currentTime.Month, currentTime.Day, currentTime.Year);
                string directoryPath = System.IO.Path.Combine(fb.SelectedPath, datePath);
                System.IO.Directory.CreateDirectory(directoryPath);

                string filename = String.Format("{0}_{1}_{2}.png", currentTime.Hour, currentTime.Minute, currentTime.Second);
                string savePath = System.IO.Path.Combine(directoryPath, filename);
                //System.Diagnostics.Debug.WriteLine(savePath);
                bitmap.Save(savePath, ImageFormat.Png);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (!isCapture)
            {
                isCapture = true;
                CaptureImage();
                timer1.Enabled = true;
                btnCapture.Text = "Stop";
            }
            else
            {
                isCapture = false;
                timer1.Enabled = false;
                btnCapture.Text = "Capture";
            }
            
        }

        private void chooseFoler_Click(object sender, EventArgs e)
        {            
            DialogResult result =  fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnCapture.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CaptureImage();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value * 60000;
        }
    }
}
