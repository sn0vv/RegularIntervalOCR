using System;
using System.Windows.Forms;

namespace RegularIntervalOCR
{
    public partial class MainWindow : Form
    {
        private Screenshot screenshot;

        public MainWindow()
        {
            InitializeComponent();
            screenshot = new Screenshot();
        }

        private async void UpdateOCR()
        {
            var bmp = screenshot.GetBitmap();

            var ocr = new OCR();
            var ocrResult = await ocr.RecognizeText(bmp);
            richTextBox1.Text = ocrResult.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            screenshot.SetCanvas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateOCR();
        }
    }
}