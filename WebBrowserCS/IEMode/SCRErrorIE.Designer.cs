
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
            this.LogLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.clearLogs = new System.Windows.Forms.Button();
            this.logList = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogLayoutPanel
            // 
            this.LogLayoutPanel.ColumnCount = 2;
            this.LogLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.LogLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LogLayoutPanel.Controls.Add(this.clearLogs, 0, 2);
            this.LogLayoutPanel.Controls.Add(this.logList, 0, 1);
            this.LogLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.LogLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.LogLayoutPanel.Name = "LogLayoutPanel";
            this.LogLayoutPanel.RowCount = 3;
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LogLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.LogLayoutPanel.Size = new System.Drawing.Size(733, 431);
            this.LogLayoutPanel.TabIndex = 3;
            // 
            // clearLogs
            // 
            this.clearLogs.Location = new System.Drawing.Point(4, 398);
            this.clearLogs.Margin = new System.Windows.Forms.Padding(4);
            this.clearLogs.Name = "clearLogs";
            this.clearLogs.Size = new System.Drawing.Size(100, 28);
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
            this.logList.Location = new System.Drawing.Point(4, 29);
            this.logList.Margin = new System.Windows.Forms.Padding(4);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(725, 361);
            this.logList.TabIndex = 3;
            this.logList.UseCompatibleStateImageBehavior = false;
            this.logList.View = System.Windows.Forms.View.Details;
            this.logList.DoubleClick += new System.EventHandler(this.logList_DoubleClick);
            this.logList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.logList_KeyUp);
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
            // SCRErrorIE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 431);
            this.Controls.Add(this.LogLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SCRErrorIE";
            this.Text = "Script Errors";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SCRErrorIE_FormClosing);
            this.LogLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LogLayoutPanel;
        private System.Windows.Forms.Button clearLogs;
        private System.Windows.Forms.ListView logList;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Desc;
    }
}