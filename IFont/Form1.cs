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
            int FontWidth = Convert.ToInt32(textBox2.Text);
            int FontHeight = FontWidth;

            float BestFontSize = FontWidth;

            Font font = new Font(ft.FontFamily, BestFontSize, ft.Style);
            Bitmap bmp = new Bitmap(FontWidth, FontHeight * Charset.Length);
            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < Charset.Length; i++)
            {
                while (g.MeasureString(Charset[i].ToString(), font).Width > FontWidth)
                    font = new Font(ft.FontFamily, BestFontSize-=0.01f, ft.Style);
            }

            for (int i = 0; i < Charset.Length; i++)
            {
                while (g.MeasureString(Charset[i].ToString(), font).Height > FontHeight)
                    font = new Font(ft.FontFamily, BestFontSize -= 0.01f, ft.Style);
            }

            g.Clear(Color.Transparent);
            StringFormat stf = new StringFormat();
            stf.Alignment = StringAlignment.Near;
            stf.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < Charset.Length; i++)
            {
                Rectangle rect = new Rectangle(0, i * FontHeight, FontWidth-1, FontHeight-1);
                //g.DrawRectangle(Pens.Red, rect);
                g.DrawString(Charset[i].ToString(), font, Brushes.White, rect, stf);
            }
            string outputname = "FONT.PNG";
            bmp.Save(outputname);
            Process.Start("explorer", outputname);
        }
    }
}