using System.Diagnostics;

namespace IFont
{
    public partial class Form1 : Form
    {
        Font ft;

        public Form1()
        {
            InitializeComponent();
            ft = new Font("Arial", 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.ShowDialog();
            ft = fd.Font;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Charset = textBox1.Text;
            int FontSize = Convert.ToInt32(textBox2.Text);

            float BestFontSize = FontSize;

            int NumRow = (int)Math.Sqrt(Charset.Length * 2);

            int BitmapWidth = FontSize * NumRow;
            int BitmapHeight =
                ((Charset.Length / NumRow) + ((Charset.Length % NumRow) != 0 ? 1 : 0)) * FontSize;

            Font font = new Font(ft.FontFamily, BestFontSize, ft.Style);
            Bitmap bmp = new Bitmap(BitmapWidth, BitmapHeight);
            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < Charset.Length; i++)
            {
                while (g.MeasureString(Charset[i].ToString(), font).Width > FontSize)
                    font = new Font(ft.FontFamily, BestFontSize-=0.01f, ft.Style);
            }

            for (int i = 0; i < Charset.Length; i++)
            {
                while (g.MeasureString(Charset[i].ToString(), font).Height > FontSize)
                    font = new Font(ft.FontFamily, BestFontSize -= 0.01f, ft.Style);
            }

            g.Clear(Color.Transparent);
            StringFormat stf = new StringFormat();
            stf.Alignment = StringAlignment.Near;
            stf.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int X = 0, Y = 0;
            for (int i = 0; i < Charset.Length; i++)
            {
                if ((i % NumRow) == 0 && i != 0) 
                {
                    X = 0;
                    Y += FontSize;
                }
                Rectangle rect = new Rectangle(X, Y, FontSize - 1, FontSize - 1);
                //g.DrawRectangle(Pens.Red, rect);
                g.DrawString(Charset[i].ToString(), font, Brushes.White, rect, stf);
                X += FontSize;
            }
            string outputname = "FONT.PNG";
            bmp.Save(outputname);
            Process.Start("explorer", outputname);
        }
    }
}