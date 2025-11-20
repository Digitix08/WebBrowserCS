
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.CurrentUrl = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ScrErr = new System.Windows.Forms.ToolStripStatusLabel();
            this.GoToUrl = new System.Windows.Forms.TextBox();
            this.GoTo = new System.Windows.Forms.PictureBox();
            this.Search = new System.Windows.Forms.PictureBox();
            this.Back = new System.Windows.Forms.PictureBox();
            this.Forward = new System.Windows.Forms.PictureBox();
            this.Reload = new System.Windows.Forms.PictureBox();
            this.GoHome = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Forward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoHome)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.GoToUrl, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.GoTo, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.Search, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.Back, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Forward, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Reload, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.GoHome, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(603, 363);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 8);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.CurrentUrl,
            this.status,
            this.toolStripStatusLabel2,
            this.ScrErr});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(603, 20);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
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
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(35, 15);
            this.status.Text = "Done";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(52, 15);
            this.toolStripStatusLabel2.Text = "IE-based";
            // 
            // ScrErr
            // 
            this.ScrErr.Name = "ScrErr";
            this.ScrErr.Size = new System.Drawing.Size(66, 15);
            this.ScrErr.Text = "Log output";
            this.ScrErr.Click += new System.EventHandler(this.ScrErr_Click);
            // 
            // GoToUrl
            // 
            this.GoToUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.GoToUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoToUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoToUrl.Location = new System.Drawing.Point(122, 2);
            this.GoToUrl.Margin = new System.Windows.Forms.Padding(2);
            this.GoToUrl.MaximumSize = new System.Drawing.Size(24577, 100);
            this.GoToUrl.Name = "GoToUrl";
            this.GoToUrl.Size = new System.Drawing.Size(419, 29);
            this.GoToUrl.TabIndex = 24;
            this.GoToUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoToUrl_KeyDown);
            // 
            // GoTo
            // 
            this.GoTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoTo.Image = global::WebBrowserCS.Properties.Resources.go;
            this.GoTo.Location = new System.Drawing.Point(545, 2);
            this.GoTo.Margin = new System.Windows.Forms.Padding(2);
            this.GoTo.Name = "GoTo";
            this.GoTo.Size = new System.Drawing.Size(26, 26);
            this.GoTo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GoTo.TabIndex = 23;
            this.GoTo.TabStop = false;
            this.GoTo.Click += new System.EventHandler(this.GoTo_Click);
            this.GoTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.GoTo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Search
            // 
            this.Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Search.Image = global::WebBrowserCS.Properties.Resources.search;
            this.Search.Location = new System.Drawing.Point(575, 2);
            this.Search.Margin = new System.Windows.Forms.Padding(2);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(26, 26);
            this.Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Search.TabIndex = 26;
            this.Search.TabStop = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            this.Search.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Search.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Back
            // 
            this.Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Back.Image = global::WebBrowserCS.Properties.Resources.arrow_back;
            this.Back.Location = new System.Drawing.Point(2, 2);
            this.Back.Margin = new System.Windows.Forms.Padding(2);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(26, 26);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Back.TabIndex = 25;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            this.Back.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Back.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Forward
            // 
            this.Forward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Forward.Image = global::WebBrowserCS.Properties.Resources.arrow_forward;
            this.Forward.Location = new System.Drawing.Point(92, 2);
            this.Forward.Margin = new System.Windows.Forms.Padding(2);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(26, 26);
            this.Forward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Forward.TabIndex = 22;
            this.Forward.TabStop = false;
            this.Forward.Click += new System.EventHandler(this.Forward_Click);
            this.Forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // Reload
            // 
            this.Reload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reload.Image = global::WebBrowserCS.Properties.Resources.arrow_reload;
            this.Reload.Location = new System.Drawing.Point(62, 2);
            this.Reload.Margin = new System.Windows.Forms.Padding(2);
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(26, 26);
            this.Reload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Reload.TabIndex = 20;
            this.Reload.TabStop = false;
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            this.Reload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.Reload.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            // 
            // GoHome
            // 
            this.GoHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GoHome.Image = global::WebBrowserCS.Properties.Resources.home;
            this.GoHome.Location = new System.Drawing.Point(32, 2);
            this.GoHome.Margin = new System.Windows.Forms.Padding(2);
            this.GoHome.Name = "GoHome";
            this.GoHome.Size = new System.Drawing.Size(26, 26);
            this.GoHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GoHome.TabIndex = 21;
            this.GoHome.TabStop = false;
            this.GoHome.Click += new System.EventHandler(this.Home_Click);
            this.GoHome.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
            this.GoHome.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_invert);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Forward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoHome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox GoToUrl;
        private System.Windows.Forms.PictureBox GoTo;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.PictureBox Forward;
        private System.Windows.Forms.PictureBox Reload;
        private System.Windows.Forms.PictureBox GoHome;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentUrl;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel ScrErr;
        private System.Windows.Forms.PictureBox Search;
    }
}
