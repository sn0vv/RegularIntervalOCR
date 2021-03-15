using System;
using System.Windows.Forms;
//using System.Timers;

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

            var txt = ocrResult.Text;
            richTextBox1.Text = txt;
            ocr.SaveText(txt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            screenshot.SetCanvas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateOCR();
        }


        public void Run()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(MyClock);
            timer.Interval = 3000;
            timer.Enabled = true; // timer.Start()と同じ

            //Application.Run(); // メッセージループを開始
        }

        public void MyClock(object sender, EventArgs e)
        {
            UpdateOCR();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Run();

        }

    }
}