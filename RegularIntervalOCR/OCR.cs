using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace RegularIntervalOCR
{

    public class OCR
    {
        private static string folder;

        public OCR()
        {
            folder = Directory.GetCurrentDirectory();
        }

        public void SaveText(string txt)
        {
            var fileName = "ocr_result.txt";
            //StreamWriter sw = new StreamWriter(fileName, Encoding.GetEncoding("utf-8"));
            //sw.Write(txt);
            //sw.Close();

            // 文字コードを指定
            Encoding enc = Encoding.GetEncoding("utf-8");

            // ファイルを開く
            StreamWriter writer = new StreamWriter($@"{folder}\{fileName}", false, enc);

            // テキストを書き込む
            writer.WriteLine(txt);

            // ファイルを閉じる
            writer.Close();
        }

        public async Task<SoftwareBitmap> ConvertToSoftwareBitmap(Bitmap bmp)
        {
            // 取得したキャプチャ画像をファイルとして保存
            //var folder = Directory.GetCurrentDirectory();
            //folder = Directory.GetCurrentDirectory();
            var fileName = "screenshot.bmp";
            bmp.Save($@"{folder}\{fileName}", ImageFormat.Bmp);

            StorageFolder appFolder = await StorageFolder.GetFolderFromPathAsync(@folder);
            var bmpFile = await appFolder.GetFileAsync(fileName);

            // 保存した画像をSoftwareBitmap形式で読み込み
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await bmpFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            return softwareBitmap;
        }

        public async Task<OcrResult> RecognizeText(Bitmap bmp)
        {
            var sbmp = await ConvertToSoftwareBitmap(bmp);

            OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
            var ocrResult = await ocrEngine.RecognizeAsync(sbmp);
            return ocrResult;
        }

    }



}
