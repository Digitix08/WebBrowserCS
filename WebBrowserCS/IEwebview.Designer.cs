
namespace WebBrowserCS
{
    partial class IEwebview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GoToUrl = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Back = new System.Windows.Forms.PictureBox();
            this.GoHome = new System.Windows.Forms.PictureBox();
            this.Reload = new System.Windows.Forms.PictureBox();
            this.Forward = new System.Windows.Forms.PictureBox();
            this.GoTo = new System.Windows.Forms.PictureBox();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.CurrentUrl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Search = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Forward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoTo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Search)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(603, 363);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Search);
            this.panel1.Controls.Add(this.Back);
            this.panel1.Controls.Add(this.GoHome);
            this.panel1.Controls.Add(this.Reload);
            this.panel1.Controls.Add(this.Forward);
            this.panel1.Controls.Add(this.GoToUrl);
            this.panel1.Controls.Add(this.GoTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 30);
            this.panel1.TabIndex = 6;
            // 
            // GoToUrl
            // 
            this.GoToUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.GoToUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoToUrl.Location = new System.Drawing.Point(121, 0);
            this.GoToUrl.Margin = new System.Windows.Forms.Padding(2);
            this.GoToUrl.MaximumSize = new System.Drawing.Size(24577, 100);
            this.GoToUrl.Name = "GoToUrl";
            this.GoToUrl.Size = new System.Drawing.Size(338, 29);
            this.GoToUrl.TabIndex = 17;
            this.GoToUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoToUrl_KeyDown);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(2, 36);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(599, 305);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowser1_Navigated);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.WebBrowser1_ProgressChanged);
            // 
            // Back
            // 
            this.Back.Image = global::WebBrowserCS.Properties.Resources.arrow_back;
            this.Back.Location = new System.Drawing.Point(1, 0);
            this.Back.Margin = new System.Windows.Forms.Padding(2);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(28, 28);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Back.TabIndex = 18;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            this.Back.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Back.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // GoHome
            // 
            this.GoHome.Image = global::WebBrowserCS.Properties.Resources.home;
            this.GoHome.Location = new System.Drawing.Point(31, 0);
            this.GoHome.Margin = new System.Windows.Forms.Padding(2);
            this.GoHome.Name = "GoHome";
            this.GoHome.Size = new System.Drawing.Size(28, 28);
            this.GoHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GoHome.TabIndex = 14;
            this.GoHome.TabStop = false;
            this.GoHome.Click += new System.EventHandler(this.Home_Click);
            this.GoHome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.GoHome.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Reload
            // 
            this.Reload.Image = global::WebBrowserCS.Properties.Resources.arrow_reload;
            this.Reload.Location = new System.Drawing.Point(61, 0);
            this.Reload.Margin = new System.Windows.Forms.Padding(2);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(28, 28);
            this.Reload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Reload.TabIndex = 13;
            this.Reload.TabStop = false;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            this.Reload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Reload.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Forward
            // 
            this.Forward.Image = global::WebBrowserCS.Properties.Resources.arrow_forward;
            this.Forward.Location = new System.Drawing.Point(91, 0);
            this.Forward.Margin = new System.Windows.Forms.Padding(2);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(28, 28);
            this.Forward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Forward.TabIndex = 15;
            this.Forward.TabStop = false;
            this.Forward.Click += new System.EventHandler(this.Forward_Click);
            this.Forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // GoTo
            // 
            this.GoTo.Image = global::WebBrowserCS.Properties.Resources.go;
            this.GoTo.Location = new System.Drawing.Point(463, 0);
            this.GoTo.Margin = new System.Windows.Forms.Padding(2);
            this.GoTo.Name = "GoTo";
            this.GoTo.Size = new System.Drawing.Size(28, 28);
            this.GoTo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GoTo.TabIndex = 16;
            this.GoTo.TabStop = false;
            this.GoTo.Click += new System.EventHandler(this.GoTo_Click);
            this.GoTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.GoTo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 14);
            this.toolStripProgressBar1.Step = 1;
            // 
            // CurrentUrl
            // 
            this.CurrentUrl.Name = "CurrentUrl";
            this.CurrentUrl.Size = new System.Drawing.Size(62, 15);
            this.CurrentUrl.Text = "CurrentUrl";
            this.CurrentUrl.Click += new System.EventHandler(this.CurrentUrl_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(52, 15);
            this.toolStripStatusLabel2.Text = "IE-based";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.CurrentUrl,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(603, 20);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Search
            // 
            this.Search.Image = global::WebBrowserCS.Properties.Resources.search;
            this.Search.Location = new System.Drawing.Point(495, 0);
            this.Search.Margin = new System.Windows.Forms.Padding(2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(28, 28);
            this.Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Search.TabIndex = 19;
            this.Search.TabStop = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            this.Search.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Search.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // IEwebview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IEwebview";
            this.Size = new System.Drawing.Size(603, 363);
            this.Load += new System.EventHandler(this.IEwebview_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Forward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoTo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.PictureBox GoHome;
        private System.Windows.Forms.PictureBox Reload;
        private System.Windows.Forms.PictureBox Forward;
        private System.Windows.Forms.TextBox GoToUrl;
        private System.Windows.Forms.PictureBox GoTo;
        internal System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.PictureBox Search;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentUrl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}
