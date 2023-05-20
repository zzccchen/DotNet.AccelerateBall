using DotNet.AccelerateBall.Util;
using NetWorkSpeedMonitor;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace DotNet.AccelerateBall
{
    public partial class MiniForm : Form
    {
        private NetworkAdapter[] adapters;
        private Thread monitorMemoryThread = null;
        private Thread monitorNetworkThread = null;
        private AppConfig config = new AppConfig();
        private Point mouseOffset;
        private ToolStripMenuItem currentOpacityItem = null;
        public MiniFormLocation miniFormLocation;

        private bool isMouseDown = false;
        public bool isMouseEnter = false;
        public int miniBigFormSpace = 5;
        public int miniFormWidth = 96;
        public int miniFormHeight = 40;

        private static string currentPath = Application.StartupPath; // System.Environment.CurrentDirectory;
        private static string configFileName = "\\config.ini";
        private static string ipmitoolPath = currentPath + "\\ipmitool.exe";
        private static string configFilePath = currentPath + configFileName;

        private static string defaultIp = "192.168.1.100";
        private static string defaultUser = "root";
        private static string defaultPassword = "calvin";
        private static string defaultConfigSection = "ipmi";
        private static int cpu1test = 1;

        private string txtIp;
        private string txtUser;
        private string txtPassword;
        private static System.Timers.Timer timer;

        /*移动时小球出现在bigForm窗体的位置方向枚举*/

        public enum MiniFormLocation
        {
            topLeft,
            topRigh,
            bottomLeft,
            bottomRight
        }

        public MiniForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            StartupSetting.autoRun("DotNet.AccelerateBall.exe", Application.ExecutablePath);
            initParameter();
            // StartMonitorNetwork(); //初始化网络流量监控
            timer = new System.Timers.Timer(1000);
            // 设置定时器触发事件的处理方法
            timer.Elapsed += Timer_Elapsed;

            // 设置定时器为可重复触发
            timer.AutoReset = true;

            // 启动定时器
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // 这里是你要执行的循环体代码
            cpu1test += 1;
            this.CPU1.Text = "." + cpu1test;
        }

        public void initParameter()
        {
            config.loadConfigFile(); //加载配置文件
            currentOpacityItem = getCurrentOpacityItem(config.getOpacity());
            setOpacity(currentOpacityItem, config.getOpacity()); //设置透明度
            Location = config.getMiniBallInitLocation(); //设置小球的坐标
            TopMost = config.getTopMost();
            if (TopMost)
            {
                showStyle2.Image = new Bitmap(Properties.Resources.dot);
                showStyle1.Image = null;
            }
            else
            {
                showStyle1.Image = new Bitmap(Properties.Resources.dot);
                showStyle2.Image = null;
            }
            if (File.Exists(configFilePath))
            {
                string ip = IniHelper.Read(defaultConfigSection, "ip", defaultIp, configFilePath);
                string user = IniHelper.Read(defaultConfigSection, "user", defaultUser, configFilePath);
                string password = IniHelper.Read(defaultConfigSection, "password", defaultPassword, configFilePath);
                txtIp = ip;
                txtUser = user;
                txtPassword = password;
            }
            else
            {
                IniHelper.Write(defaultConfigSection, "ip", defaultIp, configFilePath);
                IniHelper.Write(defaultConfigSection, "user", defaultUser, configFilePath);
                IniHelper.Write(defaultConfigSection, "password", defaultPassword, configFilePath);
                txtIp = defaultIp;
                txtUser = defaultUser;
                txtPassword = defaultPassword;
            }
        }

        private static string execute(string parameter)
        {
            Process process = null;
            string result = string.Empty;
            try
            {
                process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                process.StandardInput.WriteLine(parameter + "& exit");
                process.StandardInput.AutoFlush = true;
                result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExceptionOccurred:{ 0},{ 1}", ex.Message, ex.StackTrace.ToString());
                return null;
            }
        }

        #region 内存使用率监控和网速监控

        /*开始监控网络*/

        private void StartMonitorNetwork()
        {
            NetworkMonitor monitor = new NetworkMonitor();
            this.adapters = monitor.Adapters;
            if (this.adapters != null && adapters.Length > 0)
            {
                foreach (NetworkAdapter adapter in adapters)
                {
                    monitor.StartMonitoring(adapter);
                }
                monitorNetworkThread = new Thread(NetworkMonitor);
                monitorNetworkThread.IsBackground = true;
                monitorNetworkThread.Start();
            }
            else
            {
                MessageBox.Show("无网卡");
            }
        }

        /*网络监控*/

        private void NetworkMonitor()
        {
            while (this.adapters != null && adapters.Length > 0)
            {
                Thread.Sleep(500);
                double downloadSpeedKbps = 0;
                double uploadSpeedKbps = 0;

                foreach (NetworkAdapter adapter in adapters)
                {
                    downloadSpeedKbps += adapter.DownloadSpeedKbps;
                    uploadSpeedKbps += adapter.UploadSpeedKbps;
                }
                this.CPU1.Text = getFormatNetworkSpeed(uploadSpeedKbps);
                this.CPU2.Text = getFormatNetworkSpeed(downloadSpeedKbps);
            }
        }

        /*获取格式化的网速*/

        private string getFormatNetworkSpeed(double speedKbps)
        {
            string speed = "";
            if (speedKbps >= 100)
            {
                speed = String.Format("{0:N0}", speedKbps);
            }
            else if (speedKbps >= 10)
            {
                speed = String.Format("{0:N1}", speedKbps);
            }
            else
            {
                if (speedKbps == 0.0 || speedKbps == 0.00)
                {
                    speed = String.Format("{0:N0}", speedKbps);
                }
                else
                {
                    speed = String.Format("{0:N2}", speedKbps);
                }
            }
            return speed;
        }

        #endregion 内存使用率监控和网速监控

        #region 小球的右键菜单单击事件

        /*退出程序*/

        private void quit_Click(object sender, EventArgs e)
        {
            if (monitorMemoryThread != null)
            {
                monitorMemoryThread.Abort();
                monitorMemoryThread.Join();
            }
            if (monitorNetworkThread != null)
            {
                monitorNetworkThread.Abort();
                monitorNetworkThread.Join();
            }
            config.saveInfos(this.Location.X, this.Location.Y, (int)(this.Opacity * 100), this.TopMost);
            notifyIcon.Dispose();
            Application.Exit();
        }

        /*显示或隐藏所有窗口*/

        private void showhide_Click(object sender, EventArgs e)
        {
            if (showhide.Text == "隐藏")
            {
                this.Hide();
                showhide.Text = "显示";
            }
            else
            {
                this.Show();
                showhide.Text = "隐藏";
            }
        }

        private void showStyle1_Click(object sender, EventArgs e)
        {
            showStyle1.Image = new Bitmap(Properties.Resources.dot);
            showStyle2.Image = null;
            this.TopMost = false;
        }

        private void showStyle2_Click(object sender, EventArgs e)
        {
            showStyle2.Image = new Bitmap(Properties.Resources.dot);
            showStyle1.Image = null;
            this.TopMost = true;
        }

        private void opacity100_Click(object sender, EventArgs e)
        {
            setOpacity(opacity100, 100);
        }

        private void opacity95_Click(object sender, EventArgs e)
        {
            setOpacity(opacity95, 95);
        }

        private void opacity85_Click(object sender, EventArgs e)
        {
            setOpacity(opacity85, 85);
        }

        private void opacity80_Click(object sender, EventArgs e)
        {
            setOpacity(opacity80, 80);
        }

        private void opacity75_Click(object sender, EventArgs e)
        {
            setOpacity(opacity75, 75);
        }

        private void opacity50_Click(object sender, EventArgs e)
        {
            setOpacity(opacity50, 50);
        }

        private void opacity25_Click(object sender, EventArgs e)
        {
            setOpacity(opacity25, 25);
        }

        /*设置窗体的透明度*/

        private void setOpacity(ToolStripMenuItem opacityItem, int opacity)
        {
            currentOpacityItem.Image = null;
            opacityItem.Image = new Bitmap(Properties.Resources.dot);
            this.Opacity = opacity * 0.01;
            currentOpacityItem = opacityItem;
        }

        private ToolStripMenuItem getCurrentOpacityItem(int opacity)
        {
            switch (opacity)
            {
                case 100: return opacity100;
                case 95: return opacity95;
                case 85: return opacity85;
                case 80: return opacity80;
                case 75: return opacity75;
                case 50: return opacity50;
                case 25: return opacity25;
                default: return opacity100;
            }
        }

        #endregion 小球的右键菜单单击事件

        #region 小球的鼠标事件

        private void miniBigFormSpace_MouseEnter(object sender, EventArgs e)
        {
            isMouseEnter = true;
        }

        private void miniBigFormSpace_MouseLeave(object sender, EventArgs e)
        {
            Point p = MousePosition;
            if (p.X - 10 <= this.Left || p.X + 10 >= this.Left + miniFormWidth || p.Y - 10 <= this.Top || p.Y + 10 >= this.Bottom)
            {
                isMouseEnter = false;
            }
        }

        private void miniBigFormSpace_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = new Point(MousePosition.X - this.Location.X, MousePosition.Y - this.Location.Y);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void miniBigFormSpace_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Cursor = Cursors.Default;
        }

        private void miniBigFormSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                Point old = this.Location;
                this.Location = getMiniBallMoveLocation();
            }
        }

        #endregion 小球的鼠标事件

        #region 小球和bigForm的位置方法

        /*小球出现的位置*/

        private Point getMiniBallMoveLocation()
        {
            int x = MousePosition.X - mouseOffset.X;
            int y = MousePosition.Y - mouseOffset.Y;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (Screen.PrimaryScreen.WorkingArea.Width - x < miniFormWidth)
            {
                x = Screen.PrimaryScreen.WorkingArea.Width - miniFormWidth;
            }
            if (Screen.PrimaryScreen.WorkingArea.Height - y < miniFormHeight)
            {
                y = Screen.PrimaryScreen.WorkingArea.Height - miniFormHeight;
            }
            return new Point(x, y);
        }

        #endregion 小球和bigForm的位置方法
    }
}