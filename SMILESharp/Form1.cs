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
            buffer.Palette.Entries[0] = Color.FromArgb(255, 0, 0, 0);
            buffer.Palette.Entries[1] = Color.White;
            buffer.Palette.Entries[2] = Color.Red;
            buffer.Palette.Entries[3] = Color.Green;
            buffer.Palette.Entries[4] = Color.Yellow;
            buffer.Palette.Entries[5] = Color.Blue;
            buffer.Palette.Entries[6] = Color.Purple;
            buffer.Palette.Entries[7] = Color.Aqua;
            buffer.Palette.Entries[8] = Color.Black;
            buffer.Palette.Entries[9] = Color.White;
            buffer.Palette.Entries[10] = Color.Red;
            buffer.Palette.Entries[11] = Color.Green;
            buffer.Palette.Entries[12] = Color.Yellow;
            buffer.Palette.Entries[13] = Color.Blue;
            buffer.Palette.Entries[14] = Color.Purple;
            buffer.Palette.Entries[15] = Color.Aqua;

            var boundsRect = new Rectangle(pbxCanvas.Location.X, pbxCanvas.Location.Y, 128, 128);
            var bmpData = buffer.LockBits(boundsRect, ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);

            var ptr = bmpData.Scan0;
            Marshal.Copy(graphicsData, 0, ptr, graphicsData.Length);
            buffer.UnlockBits(bmpData);

            Invalidate();
        }
    }
}
