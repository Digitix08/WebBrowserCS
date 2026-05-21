
namespace WebBrowserCS
{
    partial class MainHistory
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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.TimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.URLHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BrowserHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SingleElemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.navigateToEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoreElemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.SingleElemContextMenu.SuspendLayout();
            this.MoreElemContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TimeHeader,
            this.URLHeader,
            this.BrowserHeader});
            this.tableLayoutPanel1.SetColumnSpan(this.listView1, 2);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(3, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(794, 389);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // TimeHeader
            // 
            this.TimeHeader.Text = "Time";
            this.TimeHeader.Width = 179;
            // 
            // URLHeader
            // 
            this.URLHeader.Text = "URL";
            this.URLHeader.Width = 435;
            // 
            // BrowserHeader
            // 
            this.BrowserHeader.Text = "Browser";
            this.BrowserHeader.Width = 95;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(103, 420);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(694, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Note: All history entries will open in the default browser engine";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(103, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(694, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Double click a row or select it and press Enter to browse to that entry";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SingleElemContextMenu
            // 
            this.SingleElemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigateToEntryToolStripMenuItem,
            this.copyEntryToolStripMenuItem,
            this.deleteEntryToolStripMenuItem});
            this.SingleElemContextMenu.Name = "SingleElemContextMenu";
            this.SingleElemContextMenu.Size = new System.Drawing.Size(160, 70);
            // 
            // navigateToEntryToolStripMenuItem
            // 
            this.navigateToEntryToolStripMenuItem.Name = "navigateToEntryToolStripMenuItem";
            this.navigateToEntryToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.navigateToEntryToolStripMenuItem.Text = "Navigate to URL";
            this.navigateToEntryToolStripMenuItem.Click += new System.EventHandler(this.navigateToEntryToolStripMenuItem_Click);
            // 
            // copyEntryToolStripMenuItem
            // 
            this.copyEntryToolStripMenuItem.Name = "copyEntryToolStripMenuItem";
            this.copyEntryToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyEntryToolStripMenuItem.Text = "Copy URL";
            this.copyEntryToolStripMenuItem.Click += new System.EventHandler(this.copyEntryToolStripMenuItem_Click);
            // 
            // deleteEntryToolStripMenuItem
            // 
            this.deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
            this.deleteEntryToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deleteEntryToolStripMenuItem.Text = "Delete Entry";
            this.deleteEntryToolStripMenuItem.Click += new System.EventHandler(this.EraseHistoryElem);
            // 
            // MoreElemContextMenu
            // 
            this.MoreElemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntriesToolStripMenuItem});
            this.MoreElemContextMenu.Name = "SingleElemContextMenu";
            this.MoreElemContextMenu.Size = new System.Drawing.Size(146, 26);
            // 
            // deleteEntriesToolStripMenuItem
            // 
            this.deleteEntriesToolStripMenuItem.Name = "deleteEntriesToolStripMenuItem";
            this.deleteEntriesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteEntriesToolStripMenuItem.Text = "Delete Entries";
            this.deleteEntriesToolStripMenuItem.Click += new System.EventHandler(this.EraseHistoryElem);
            // 
            // MainHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainHistory";
            this.Text = "Browser history";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainHistory_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.SingleElemContextMenu.ResumeLayout(false);
            this.MoreElemContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader TimeHeader;
        private System.Windows.Forms.ColumnHeader URLHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader BrowserHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip SingleElemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem navigateToEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEntryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip MoreElemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteEntriesToolStripMenuItem;
    }
}