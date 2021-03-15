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

            //Console.WriteLine(DateTime.Now);
            // 出力例：
            // 2005/11/08 19:59:10
            // 2005/11/08 19:59:11
            // 2005/11/08 19:59:12
            // ……
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Run();

            //int num = 0;

            //// タイマーの間隔(ミリ秒)
            ////System.Timers.Timer timer = new System.Timers.Timer(3000);
            //Timer timer = new Timer(3000);

            //// タイマーの処理
            ////timer.Elapsed += (sender, e) =>
            //timer.Elapsed += (s, e2) =>
            //{
            //    if (num < 5)
            //    {
            //        UpdateOCR();
            //        //Console.WriteLine("5回繰り返します");
            //        num++;
            //    }
            //    else
            //    {
            //        timer.Stop();
            //        //Console.WriteLine("処理を終了しました");
            //    }
            //};

            //// タイマーを開始する
            //timer.Start();

            ////Console.ReadKey();
        }

    }
}