using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace RegularIntervalOCR
{

    public class OCR
    {

        public async Task<SoftwareBitmap> ConvertToSoftwareBitmap(Bitmap bmp)
        {
            // 取得したキャプチャ画像をファイルとして保存
            var folder = Directory.GetCurrentDirectory();
            var fileName = "screenshot.bmp";
            StorageFolder appFolder = await StorageFolder.GetFolderFromPathAsync(@folder);
            //bmp.Save(folder + "\\" + fileName, ImageFormat.Bmp);
            bmp.Save($@"{folder}\{fileName}", ImageFormat.Bmp);

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
