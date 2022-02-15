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
            string charset = textBox1.Text;
            int fwidth = Convert.ToInt32(textBox2.Text);
            int fheight = Convert.ToInt32(textBox3.Text);
            float bestfontw = fwidth;
            Font font = new Font(ft.FontFamily, bestfontw, ft.Style);
            Bitmap bmp = new Bitmap(fwidth, fheight * charset.Length);
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < charset.Length; i++)
            {
                while (g.MeasureString(charset[i].ToString(), font).Width > fwidth)
                    font = new Font(ft.FontFamily, bestfontw-=0.01f, ft.Style);
            }
            g.Clear(Color.Transparent);
            StringFormat stf = new StringFormat();
            stf.Alignment = StringAlignment.Center;
            stf.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < charset.Length; i++)
            {
                Rectangle rect = new Rectangle(0, i * fheight, fwidth-1, fheight-1);
                g.DrawRectangle(Pens.Red, rect);
                g.DrawString(charset[i].ToString(), font, Brushes.White, rect, stf);
            }
            string outputname = "FONT.PNG";
            bmp.Save(outputname);
            Process.Start("explorer", outputname);
        }
    }
}