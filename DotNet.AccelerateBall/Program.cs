using System;
using System.Windows.Forms;

namespace DotNet.AccelerateBall
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //程序的链接地址是：http://blog.csdn.net/yuanwofei/article/details/16339825
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MiniForm());
        }
    }
}