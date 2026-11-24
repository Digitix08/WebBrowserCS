
namespace WebBrowserCS
{
    partial class SCRErrorIE
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCRErrorIE));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.logPage = new System.Windows.Forms.TabPage();
            this.LogLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.clearLogs = new System.Windows.Forms.Button();
            this.logList = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scriptErrorPage = new System.Windows.Forms.TabPage();
            this.ErrLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.clearErrors = new System.Windows.Forms.Button();
            this.errorList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.logPage.SuspendLayout();
            this.LogLayoutPanel.SuspendLayout();
            this.scriptErrorPage.SuspendLayout();
            this.ErrLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.logPage);
            this.tabControl1.Controls.Add(this.scriptErrorPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(550, 350);
            this.tabControl1.TabIndex = 0;
            // 
            // logPage
            // 
            this.logPage.Controls.Add(this.LogLayoutPanel);
            this.logPage.Location = new System.Drawing.Point(4, 22);
            this.logPage.Name = "logPage";
            this.logPage.Padding = new System.Windows.Forms.Padding(3);
            this.logPage.Size = new System.Drawing.Size(542, 324);
            this.logPage.TabIndex = 0;
            this.logPage.Text = "Console Logs";
            this.logPage.UseVisualStyleBackColor = true;
            // 
            // LogLayoutPanel
            // 
            this.LogLayoutPanel.ColumnCount = 2;
            this.LogLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.LogLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LogLayoutPanel.Controls.Add(this.clearLogs, 0, 1);
            this.LogLayoutPanel.Controls.Add(this.logList, 0, 0);
            this.LogLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.LogLayoutPanel.Name = "LogLayoutPanel";
            this.LogLayoutPanel.RowCount = 2;
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LogLayoutPanel.Size = new System.Drawing.Size(536, 318);
            this.LogLayoutPanel.TabIndex = 2;
            // 
            // clearLogs
            // 
            this.clearLogs.Location = new System.Drawing.Point(3, 291);
            this.clearLogs.Name = "clearLogs";
            this.clearLogs.Size = new System.Drawing.Size(75, 23);
            this.clearLogs.TabIndex = 2;
            this.clearLogs.Text = "Clear";
            this.clearLogs.UseVisualStyleBackColor = true;
            this.clearLogs.Click += new System.EventHandler(this.button1_Click);
            // 
            // logList
            // 
            this.logList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type,
            this.Desc});
            this.LogLayoutPanel.SetColumnSpan(this.logList, 2);
            this.logList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logList.FullRowSelect = true;
            this.logList.HideSelection = false;
            this.logList.Location = new System.Drawing.Point(3, 3);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(530, 282);
            this.logList.TabIndex = 3;
            this.logList.UseCompatibleStateImageBehavior = false;
            this.logList.View = System.Windows.Forms.View.Details;
            this.logList.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.logList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 95;
            // 
            // Desc
            // 
            this.Desc.Text = "Description";
            this.Desc.Width = 445;
            // 
            // scriptErrorPage
            // 
            this.scriptErrorPage.Controls.Add(this.ErrLayoutPanel);
            this.scriptErrorPage.Location = new System.Drawing.Point(4, 22);
            this.scriptErrorPage.Name = "scriptErrorPage";
            this.scriptErrorPage.Padding = new System.Windows.Forms.Padding(3);
            this.scriptErrorPage.Size = new System.Drawing.Size(542, 324);
            this.scriptErrorPage.TabIndex = 1;
            this.scriptErrorPage.Text = "Script Errors";
            this.scriptErrorPage.UseVisualStyleBackColor = true;
            // 
            // ErrLayoutPanel
            // 
            this.ErrLayoutPanel.ColumnCount = 2;
            this.ErrLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.ErrLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ErrLayoutPanel.Controls.Add(this.clearErrors, 0, 1);
            this.ErrLayoutPanel.Controls.Add(this.errorList, 0, 0);
            this.ErrLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.ErrLayoutPanel.Name = "ErrLayoutPanel";
            this.ErrLayoutPanel.RowCount = 2;
            this.ErrLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ErrLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.ErrLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ErrLayoutPanel.Size = new System.Drawing.Size(536, 318);
            this.ErrLayoutPanel.TabIndex = 3;
            // 
            // clearErrors
            // 
            this.clearErrors.Location = new System.Drawing.Point(3, 291);
            this.clearErrors.Name = "clearErrors";
            this.clearErrors.Size = new System.Drawing.Size(75, 23);
            this.clearErrors.TabIndex = 2;
            this.clearErrors.Text = "Clear";
            this.clearErrors.UseVisualStyleBackColor = true;
            // 
            // errorList
            // 
            this.errorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader2});
            this.ErrLayoutPanel.SetColumnSpan(this.errorList, 2);
            this.errorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorList.FullRowSelect = true;
            this.errorList.HideSelection = false;
            this.errorList.Location = new System.Drawing.Point(3, 3);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(530, 282);
            this.errorList.TabIndex = 3;
            this.errorList.UseCompatibleStateImageBehavior = false;
            this.errorList.View = System.Windows.Forms.View.Details;
            this.errorList.DoubleClick += new System.EventHandler(this.errorList_DoubleClick);
            this.errorList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.errorList_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Page";
            this.columnHeader1.Width = 112;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Line";
            this.columnHeader3.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 333;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Code";
            this.columnHeader4.Width = 41;
            // 
            // SCRErrorIE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SCRErrorIE";
            this.Text = "Script Errors";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SCRErrorIE_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.logPage.ResumeLayout(false);
            this.LogLayoutPanel.ResumeLayout(false);
            this.scriptErrorPage.ResumeLayout(false);
            this.ErrLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage logPage;
        private System.Windows.Forms.TableLayoutPanel LogLayoutPanel;
        private System.Windows.Forms.Button clearLogs;
        private System.Windows.Forms.ListView logList;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.TabPage scriptErrorPage;
        private System.Windows.Forms.TableLayoutPanel ErrLayoutPanel;
        private System.Windows.Forms.Button clearErrors;
        private System.Windows.Forms.ListView errorList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}