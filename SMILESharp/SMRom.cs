using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMILESharp
{
    class SMRom
    {
        public int HeaderSize { get; private set; }
        private const int pauseTilesOffset = 0x1B0000;
        private const int pauseTilesLength = 0x80;
        private byte[] romData;

        /// <summary>
        /// Class to contain SM ROM data and access methods
        /// </summary>
        public SMRom()
        {
            if (!LoadROM())
            {
                throw new BadRomException("Failed to load ROM");
            }

            GetHeaderSize();
        }

        private void GetHeaderSize()
        {
            HeaderSize = romData.Length % 32768;

            if (HeaderSize != 0 && HeaderSize != 512)
            {
                throw new BadRomException("ROM is an incorrect length.");
            }
        }

        /// <summary>
        /// Prompts the user for a ROM and loads it
        /// </summary>
        /// <returns>True if successfully loaded, else false</returns>
        public bool LoadROM()
        {
            var openFileDialog = new OpenFileDialog()
            {
                AutoUpgradeEnabled = true,
                Filter = "SNES ROM|*.smc;*.sfc",
                FileName = "Super Metroid"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var binReader = new BinaryReader(fStream);
                    romData = binReader.ReadBytes((int) fStream.Length);
                }
                return true;
            }
            return false;
        }

        public byte[] GetGraphicsData()
        {
            var graphicsData = new byte[pauseTilesLength];
            Buffer.BlockCopy(romData, pauseTilesOffset, graphicsData, 0, pauseTilesLength);
            return graphicsData;

        }
    }
}
