using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Grapevine.Server;

namespace RegularIntervalOCR
{
    public partial class MainWindow : Form
    {
        private Screenshot screenshot;
        private RestServer server;

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

  

        private void button5_Click(object sender, EventArgs e)
        {
            ServerSettings settings = new ServerSettings()
            {
                Port = maskedTextBox1.Text,
                PublicFolder = new PublicFolder("web")
            };

            server = new RestServer(settings);
            server.Start();

            button5.Enabled = false;
            button4.Enabled = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            server.Stop();
            button5.Enabled = true;
            button4.Enabled = false;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //数字と空白しか入力できないようにする
            this.maskedTextBox1.Mask = "9999";
            //Int32型に変換できるか検証する
            this.maskedTextBox1.ValidatingType = typeof(int);
            //TypeValidationCompletedイベントハンドラを追加する
            this.maskedTextBox1.TypeValidationCompleted += maskedTextBox1_TypeValidationCompleted;
        }

        private void maskedTextBox1_TypeValidationCompleted(
            object sender, TypeValidationEventArgs e)
        {
            //Int32型に変換できるか確かめる
            if (!e.IsValidInput)
            {
                //Int32型への変換に失敗した時は、フォーカスが移動しないようにする
                MessageBox.Show("数値を入力してください");
                e.Cancel = true;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Stop();
            }
        }
    }
}