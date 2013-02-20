using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace touch_shortcut
{
    public partial class circleform : Form
    {
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);



        private const int SnapDist = 50;
        private bool DoSnap(int pos, int edge) {
          int delta = pos - edge;
          return delta <= SnapDist;
        }
        protected override void  OnResizeEnd(EventArgs e) {
          base.OnResizeEnd(e);
          Screen scn = Screen.FromPoint(this.Location); 
          if (DoSnap(this.Left, scn.WorkingArea.Left)) this.Left= scn.WorkingArea.Left;
          if (DoSnap(this.Top, scn.WorkingArea.Top)) this.Top = scn.WorkingArea.Top;
          if (DoSnap(scn.WorkingArea.Right, this.Right)) this.Left = scn.WorkingArea.Right - this.Width;
          if (DoSnap(scn.WorkingArea.Bottom, this.Bottom)) this.Top = scn.WorkingArea.Bottom - this.Height;
        }


        public circleform()
        {
            InitializeComponent();
             this.Location = new Point(100, 100);
             this.Cursor = System.Windows.Forms.Cursors.Hand;
             this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
             this.ShowInTaskbar = false;
           /*this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
             this.ForeColor = System.Drawing.SystemColors.Desktop;
             this.BackColor = System.Drawing.Color.Blue;      
             this.Opacity = 0.8;
             this.AutoSize = true;*/
        }


       protected override void  OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left & this.WindowState == FormWindowState.Normal & configform.kf.Visible==false)
            {

                this.Capture = false;
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }       


        private void button2_Click(object sender, EventArgs e)
        {

            Screen scn = Screen.FromPoint(this.Location);
            Point point = new Point();
            point.X = this.Left;
            point.Y = this.Bottom;
            configform.kf.StartPosition = FormStartPosition.Manual;
            configform.kf.Location = point;

            if (configform.kf.Visible == false)
                configform.kf.Show();
            else configform.kf.Hide();

        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MessageBox.Show("you force quit yourself");
            Application.Exit();
        }
        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return cp;
            }
        }

        private void circleform_Load(object sender, EventArgs e)
        {

        }

        private void exit_menu(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right){
                if (MessageBox.Show("Do you want to Exit?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

    }
}
