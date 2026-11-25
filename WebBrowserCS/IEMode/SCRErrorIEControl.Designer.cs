
namespace WebBrowserCS
{
    partial class SCRErrorIEControl
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
            this.ScrErrorPanel = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.disableErrorLog = new System.Windows.Forms.CheckBox();
            this.disableJS = new System.Windows.Forms.CheckBox();
            this.ScrErrorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScrErrorPanel
            // 
            this.ScrErrorPanel.ColumnCount = 3;
            this.ScrErrorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.ScrErrorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.ScrErrorPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ScrErrorPanel.Controls.Add(this.button1, 0, 1);
            this.ScrErrorPanel.Controls.Add(this.listView1, 0, 0);
            this.ScrErrorPanel.Controls.Add(this.disableErrorLog, 2, 1);
            this.ScrErrorPanel.Controls.Add(this.disableJS, 1, 1);
            this.ScrErrorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScrErrorPanel.Location = new System.Drawing.Point(0, 0);
            this.ScrErrorPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScrErrorPanel.Name = "ScrErrorPanel";
            this.ScrErrorPanel.RowCount = 2;
            this.ScrErrorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ScrErrorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.ScrErrorPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.ScrErrorPanel.Size = new System.Drawing.Size(772, 335);
            this.ScrErrorPanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 302);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type,
            this.Desc});
            this.ScrErrorPanel.SetColumnSpan(this.listView1, 3);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(4, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(764, 290);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
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
            // disableErrorLog
            // 
            this.disableErrorLog.AutoSize = true;
            this.disableErrorLog.Location = new System.Drawing.Point(337, 302);
            this.disableErrorLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disableErrorLog.Name = "disableErrorLog";
            this.disableErrorLog.Size = new System.Drawing.Size(205, 21);
            this.disableErrorLog.TabIndex = 5;
            this.disableErrorLog.Text = "Disable console.log redirect";
            this.disableErrorLog.UseVisualStyleBackColor = true;
            this.disableErrorLog.CheckedChanged += new System.EventHandler(this.disableErrorLog_CheckedChanged);
            // 
            // disableJS
            // 
            this.disableJS.AutoSize = true;
            this.disableJS.Location = new System.Drawing.Point(137, 302);
            this.disableJS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disableJS.Name = "disableJS";
            this.disableJS.Size = new System.Drawing.Size(140, 21);
            this.disableJS.TabIndex = 4;
            this.disableJS.Text = "Disable JS Errors";
            this.disableJS.UseVisualStyleBackColor = true;
            this.disableJS.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // SCRErrorIEControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScrErrorPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SCRErrorIEControl";
            this.Size = new System.Drawing.Size(772, 335);
            this.ScrErrorPanel.ResumeLayout(false);
            this.ScrErrorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ScrErrorPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.CheckBox disableJS;
        private System.Windows.Forms.CheckBox disableErrorLog;
    }
}
