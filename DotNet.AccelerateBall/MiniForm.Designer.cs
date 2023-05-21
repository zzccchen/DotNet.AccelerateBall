using System.Reflection.Emit;
using System.Windows.Forms;

namespace DotNet.AccelerateBall
{
    partial class MiniForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniForm));
            this.mainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showhide = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.transparecy = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity100 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity95 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity85 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity80 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity75 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity50 = new System.Windows.Forms.ToolStripMenuItem();
            this.opacity25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quit = new System.Windows.Forms.ToolStripMenuItem();
            this.CPU1 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.CPU2 = new System.Windows.Forms.Label();
            this.CpuUsage = new System.Windows.Forms.Label();
            this.miniBallControl = new DotNet.AccelerateBall.MainBallControl();
            this.mainContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContextMenu
            // 
            this.mainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showhide,
            this.showStyle,
            this.transparecy,
            this.toolStripSeparator1,
            this.quit});
            this.mainContextMenu.Name = "mainContextMenu";
            this.mainContextMenu.Size = new System.Drawing.Size(125, 98);
            // 
            // showhide
            // 
            this.showhide.Name = "showhide";
            this.showhide.ShowShortcutKeys = false;
            this.showhide.Size = new System.Drawing.Size(124, 22);
            this.showhide.Text = "隐藏";
            this.showhide.Click += new System.EventHandler(this.showhide_Click);
            // 
            // showStyle
            // 
            this.showStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStyle1,
            this.showStyle2});
            this.showStyle.Name = "showStyle";
            this.showStyle.Size = new System.Drawing.Size(124, 22);
            this.showStyle.Text = "显示方式";
            // 
            // showStyle1
            // 
            this.showStyle1.Name = "showStyle1";
            this.showStyle1.Size = new System.Drawing.Size(172, 22);
            this.showStyle1.Text = "不在前端显示";
            this.showStyle1.Click += new System.EventHandler(this.showStyle1_Click);
            // 
            // showStyle2
            // 
            this.showStyle2.Name = "showStyle2";
            this.showStyle2.Size = new System.Drawing.Size(172, 22);
            this.showStyle2.Text = "在其他窗口前显示";
            this.showStyle2.Click += new System.EventHandler(this.showStyle2_Click);
            // 
            // transparecy
            // 
            this.transparecy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opacity100,
            this.opacity95,
            this.opacity85,
            this.opacity80,
            this.opacity75,
            this.opacity50,
            this.opacity25});
            this.transparecy.Name = "transparecy";
            this.transparecy.ShowShortcutKeys = false;
            this.transparecy.Size = new System.Drawing.Size(124, 22);
            this.transparecy.Text = "透明度";
            // 
            // opacity100
            // 
            this.opacity100.AutoSize = false;
            this.opacity100.Name = "opacity100";
            this.opacity100.ShowShortcutKeys = false;
            this.opacity100.Size = new System.Drawing.Size(152, 22);
            this.opacity100.Text = "不透明";
            this.opacity100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.opacity100.Click += new System.EventHandler(this.opacity100_Click);
            // 
            // opacity95
            // 
            this.opacity95.Name = "opacity95";
            this.opacity95.ShowShortcutKeys = false;
            this.opacity95.Size = new System.Drawing.Size(104, 22);
            this.opacity95.Text = "95";
            this.opacity95.Click += new System.EventHandler(this.opacity95_Click);
            // 
            // opacity85
            // 
            this.opacity85.Name = "opacity85";
            this.opacity85.ShowShortcutKeys = false;
            this.opacity85.Size = new System.Drawing.Size(104, 22);
            this.opacity85.Text = "85";
            this.opacity85.Click += new System.EventHandler(this.opacity85_Click);
            // 
            // opacity80
            // 
            this.opacity80.Name = "opacity80";
            this.opacity80.ShowShortcutKeys = false;
            this.opacity80.Size = new System.Drawing.Size(104, 22);
            this.opacity80.Text = "80";
            this.opacity80.Click += new System.EventHandler(this.opacity80_Click);
            // 
            // opacity75
            // 
            this.opacity75.Name = "opacity75";
            this.opacity75.ShowShortcutKeys = false;
            this.opacity75.Size = new System.Drawing.Size(104, 22);
            this.opacity75.Text = "75";
            this.opacity75.Click += new System.EventHandler(this.opacity75_Click);
            // 
            // opacity50
            // 
            this.opacity50.Name = "opacity50";
            this.opacity50.ShowShortcutKeys = false;
            this.opacity50.Size = new System.Drawing.Size(104, 22);
            this.opacity50.Text = "50";
            this.opacity50.Click += new System.EventHandler(this.opacity50_Click);
            // 
            // opacity25
            // 
            this.opacity25.Name = "opacity25";
            this.opacity25.ShowShortcutKeys = false;
            this.opacity25.Size = new System.Drawing.Size(104, 22);
            this.opacity25.Text = "25";
            this.opacity25.Click += new System.EventHandler(this.opacity25_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // quit
            // 
            this.quit.Name = "quit";
            this.quit.ShowShortcutKeys = false;
            this.quit.Size = new System.Drawing.Size(124, 22);
            this.quit.Text = "退出";
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // CPU1
            // 
            this.CPU1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CPU1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.CPU1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPU1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.CPU1.Location = new System.Drawing.Point(55, 6);
            this.CPU1.Margin = new System.Windows.Forms.Padding(0);
            this.CPU1.Name = "CPU1";
            this.CPU1.Size = new System.Drawing.Size(28, 20);
            this.CPU1.TabIndex = 3;
            this.CPU1.Text = "00";
            this.CPU1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CPU1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseDown);
            this.CPU1.MouseEnter += new System.EventHandler(this.miniBigFormSpace_MouseEnter);
            this.CPU1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseMove);
            this.CPU1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseUp);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.mainContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "电脑悬浮球";
            this.notifyIcon.Visible = true;
            // 
            // CPU2
            // 
            this.CPU2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CPU2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.CPU2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPU2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.CPU2.Location = new System.Drawing.Point(55, 27);
            this.CPU2.Margin = new System.Windows.Forms.Padding(0);
            this.CPU2.Name = "CPU2";
            this.CPU2.Size = new System.Drawing.Size(28, 20);
            this.CPU2.TabIndex = 3;
            this.CPU2.Text = "00";
            this.CPU2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CPU2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseDown);
            this.CPU2.MouseEnter += new System.EventHandler(this.miniBigFormSpace_MouseEnter);
            this.CPU2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseMove);
            this.CPU2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseUp);
            // 
            // CpuUsage
            // 
            this.CpuUsage.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.CpuUsage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(201)))), ((int)(((byte)(208)))));
            this.CpuUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CpuUsage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.CpuUsage.Location = new System.Drawing.Point(5, 14);
            this.CpuUsage.Margin = new System.Windows.Forms.Padding(0);
            this.CpuUsage.Name = "CpuUsage";
            this.CpuUsage.Size = new System.Drawing.Size(42, 26);
            this.CpuUsage.TabIndex = 3;
            this.CpuUsage.Text = "100";
            this.CpuUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CpuUsage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseDown);
            this.CpuUsage.MouseEnter += new System.EventHandler(this.miniBigFormSpace_MouseEnter);
            this.CpuUsage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseMove);
            this.CpuUsage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseUp);
            // 
            // miniBallControl
            // 
            this.miniBallControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.miniBallControl.Location = new System.Drawing.Point(0, 0);
            this.miniBallControl.Name = "miniBallControl";
            this.miniBallControl.Size = new System.Drawing.Size(156, 80);
            this.miniBallControl.TabIndex = 5;
            this.miniBallControl.Text = "miniBallControl";
            this.miniBallControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseDown);
            this.miniBallControl.MouseEnter += new System.EventHandler(this.miniBigFormSpace_MouseEnter);
            this.miniBallControl.MouseLeave += new System.EventHandler(this.miniBigFormSpace_MouseLeave);
            this.miniBallControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseMove);
            this.miniBallControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.miniBigFormSpace_MouseUp);
            // 
            // MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(156, 80);
            this.ContextMenuStrip = this.mainContextMenu;
            this.Controls.Add(this.CpuUsage);
            this.Controls.Add(this.CPU2);
            this.Controls.Add(this.CPU1);
            this.Controls.Add(this.miniBallControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MiniForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "悬浮球";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.WhiteSmoke;
            this.mainContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mainContextMenu;
        private System.Windows.Forms.ToolStripMenuItem transparecy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quit;
        private System.Windows.Forms.Label CPU1;
        private MainBallControl miniBallControl;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem showhide;
        private System.Windows.Forms.ToolStripMenuItem opacity95;
        private System.Windows.Forms.ToolStripMenuItem opacity85;
        private System.Windows.Forms.ToolStripMenuItem opacity80;
        private System.Windows.Forms.ToolStripMenuItem opacity75;
        private System.Windows.Forms.ToolStripMenuItem opacity50;
        private System.Windows.Forms.ToolStripMenuItem opacity25;
        private System.Windows.Forms.ToolStripMenuItem opacity100;
        private System.Windows.Forms.ToolStripMenuItem showStyle;
        private System.Windows.Forms.ToolStripMenuItem showStyle1;
        private System.Windows.Forms.ToolStripMenuItem showStyle2;
        private System.Windows.Forms.Label CPU2;
        private System.Windows.Forms.Label CpuUsage;
    }
}

