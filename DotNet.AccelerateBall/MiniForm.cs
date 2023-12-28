using NetWorkSpeedMonitor;
using System;
using System.ComponentModel;
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
            initParameter();
            StartMonitor(); //初始化网络流量监控
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

        /*开始监控*/

        private void StartMonitor()
        {
            timer = new System.Timers.Timer(1000);
            // 设置定时器触发事件的处理方法
            timer.Elapsed += background_FetchStates_DoWork;

            // 设置定时器为可重复触发
            timer.AutoReset = false;

            // 启动定时器
            timer.Start();
        }

        private void background_FetchStates_DoWork(object sender, ElapsedEventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            string ip = txtIp;
            string user = txtUser;
            string password = txtPassword;

            string formatSensor = "-I lanplus -H {0} -U {1} -P {2} sensor";
            string parametersSensor = string.Format(formatSensor, ip, user, password);

            string fullExecuteSensor = ipmitoolPath + " " + parametersSensor;

            bgWorker.WorkerReportsProgress = true;
            while (true)
            {
                bgWorker.ReportProgress(0, "start");

                string result = execute(fullExecuteSensor);

                result = result.Replace("\r\n", "\n");
                string[] sensorList = result.Split('\n', '\r');

                //this.CPU1.Text = "." + cpu1test;
                int cpu_flag = 1;
                foreach (var item in sensorList)
                {
                    if (item.Contains("Temp") || item.Contains("CPU Usage"))
                    {
                        string[] temp = new string[8];
                        var src = item.Split('|');
                        temp[0] = src[0];
                        temp[1] = src[1];
                        if (cpu_flag == 1 && temp[0].StartsWith("Temp"))
                        {
                            this.CPU1.Text = src[1].Substring(0, 3);
                            cpu_flag++;
                            int CPU1_int = int.Parse(this.CPU1.Text);
                            if (CPU1_int >= 85)
                            {
                                this.CPU1.ForeColor = System.Drawing.Color.FromArgb(235, 158, 206);
                            }
                            else if (CPU1_int >= 60)
                            {
                                this.CPU1.ForeColor = System.Drawing.Color.FromArgb(253, 213, 59);
                            }
                            else
                            {
                                this.CPU1.ForeColor = System.Drawing.Color.FromArgb(254, 254, 254);
                            }
                            continue;
                        }
                        if (cpu_flag == 2 && temp[0].StartsWith("Temp"))
                        {
                            this.CPU2.Text = src[1].Substring(0, 3);
                            int CPU2_int = int.Parse(this.CPU2.Text);
                            if (CPU2_int >= 85)
                            {
                                this.CPU2.ForeColor = System.Drawing.Color.FromArgb(235, 158, 206);
                            }
                            else if (CPU2_int >= 60)
                            {
                                this.CPU2.ForeColor = System.Drawing.Color.FromArgb(253, 213, 59);
                            }
                            else
                            {
                                this.CPU2.ForeColor = System.Drawing.Color.FromArgb(254, 254, 254);
                            }
                            continue;
                        }
                        if (temp[0].StartsWith("CPU Usage"))
                        {
                            string[] usage_list = src[1].Split('.');
                            this.CpuUsage.Text = usage_list[0];
                            //int usage_int = int.Parse(usage_list[0]);
                            //if (usage_int >= 90)
                            //{
                            //    this.CpuUsage.ForeColor = System.Drawing.Color.FromArgb(212, 59, 26);
                            //}
                            //else
                            //{
                            //    this.CpuUsage.ForeColor = System.Drawing.Color.FromArgb(254, 254, 254);
                            //}
                        }
                        //lstViewSensor.Items.Add(new ListViewItem(temp));
                        bgWorker.ReportProgress(1, temp);
                    }
                }
                bgWorker.ReportProgress(100, "completed");
                // 停止后台任务
            }
            // 释放资源
            bgWorker.Dispose();
        }

        /*监控*/

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