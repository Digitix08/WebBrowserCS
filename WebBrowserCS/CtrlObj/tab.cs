using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS.controls
{
    public partial class Tab : UserControl
    {
        public string Title = "Tab";
        public Control ControlledTab = null;
        public string Uri = "";
        bool selected = false;

        public delegate void OnTitleChanged(string value, Control caller);
        public event OnTitleChanged TitleChanged;
        public delegate void OnTabSelect(Control caller);
        public event OnTabSelect TabSelected;
        public delegate void OnTabClose(Control caller);
        public event OnTabClose TabClosed;

        public Tab()
        {
            InitializeComponent();
            label1.Text = Title;
        }

        public void SelectTab()
        {
            if (!selected)
            {
                Color tablecolor = tableLayoutPanel1.BackColor;
                tableLayoutPanel1.BackColor = tableLayoutPanel1.ForeColor;
                tableLayoutPanel1.ForeColor = tablecolor;
                selected = true;
            }
        }

        public void DeselectTab()
        {
            if (selected)
            {
                Color tablecolor = tableLayoutPanel1.BackColor;
                tableLayoutPanel1.BackColor = tableLayoutPanel1.ForeColor;
                tableLayoutPanel1.ForeColor = tablecolor;
                selected = false;
            }
        }

        public void setData(string text = "", Uri URL = null, Control tab = null)
        {
            if (text != null) Title = text;
            if (URL != null) Uri = URL.ToString();
            if (tab != null) ControlledTab = tab;

            label1.Text = Title;
            TitleChanged?.Invoke(text, this);
        }

        protected void OnResize(object sender, EventArgs e)
        {
            Control obj = (Control)sender;
            // Force height to equal width
            if (obj.Width != obj.Height)
            {
                obj.Height = obj.Width;
            }
        }

        private void OnResizeControl(object sender, EventArgs e)
        {
            button1.Height = this.Height - 6;
            pictureBox1.Height = this.Height - 6;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Tag != null) TabClosed?.Invoke(this);
        }

        private void TabClicked(object sender, EventArgs e)
        {
            if (Tag != null) TabSelected?.Invoke(this);
        }
    }
}
