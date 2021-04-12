//using Grapevine;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegularIntervalOCR
{



    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());


        }


    }



}
