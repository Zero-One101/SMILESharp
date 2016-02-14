using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SMILESharp
{
    public partial class Form1 : Form
    {
        private SMRom rom;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                rom = new SMRom();
            }
            catch (BadRomException)
            {
                Close();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var graphicsData = rom.GetGraphicsData();
            var buffer = new Bitmap(256, 256, PixelFormat.Format4bppIndexed);
            var palette = buffer.Palette;
            palette.Entries[0] = Color.FromArgb(255, 0, 0, 0);
            palette.Entries[1] = Color.White;
            palette.Entries[2] = Color.Red;
            palette.Entries[3] = Color.Green;
            palette.Entries[4] = Color.Yellow;
            palette.Entries[5] = Color.Blue;
            palette.Entries[6] = Color.Purple;
            palette.Entries[7] = Color.Aqua;
            palette.Entries[8] = Color.Black;
            palette.Entries[9] = Color.White;
            palette.Entries[10] = Color.Red;
            palette.Entries[11] = Color.Green;
            palette.Entries[12] = Color.Yellow;
            palette.Entries[13] = Color.Blue;
            palette.Entries[14] = Color.Purple;
            palette.Entries[15] = Color.Aqua;
            buffer.Palette = palette;

            var boundsRect = new Rectangle(0, 0, 256, 256);
            var bmpData = buffer.LockBits(boundsRect, ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);

            var ptr = bmpData.Scan0;
            Marshal.Copy(graphicsData, 0, ptr, graphicsData.Length);
            buffer.UnlockBits(bmpData);

            pbxCanvas.Image = buffer;

            Invalidate();
        }
    }
}
