using System.Drawing;

namespace BarcodeMaster.CodeDesigner.Styles
{
    /// <summary>
    /// Class Standard contains logic to draw common QR code
    /// </summary>
    class Standard : Style
    {
        Bitmap QRCode;
        readonly Color[] Palet =
        {
            Color.White,
            Color.Black
        };
        int modulSize;

        public override Bitmap Draw(byte[,] template, int modulSize)
        {
            int width, heigh;

            this.modulSize = modulSize;
            width = modulSize * 8 + template.GetLength(1) * modulSize;
            heigh = modulSize * 8 + template.GetLength(0) * modulSize;
            QRCode = new Bitmap(width, heigh);
            
            for(int y = 0; y < heigh; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    QRCode.SetPixel(x, y, Palet[0]);
                }
            }

            for(int row = 0; row < template.GetLength(0); row++)
            {
                for(int column = 0; column < template.GetLength(1); column++)
                {
                    DrawModul(column, row, template[row, column]);
                }
            }

            return QRCode;
        }

        private void DrawModul(int column, int row, byte value)
        {
            if (value == 0)
                return;

            int x0, y0;

            x0 = modulSize * 4 + modulSize * column;
            y0 = modulSize * 4 + modulSize * row;
            
            for(int y  = y0; y < y0 + modulSize; y++)
            {
                for (int x = x0; x < x0 + modulSize; x++)
                {
                    QRCode.SetPixel(x, y, Palet[1]);
                }
            }
        }
    }
}
