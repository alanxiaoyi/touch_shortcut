using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;

namespace touch_shortcut
{
    public partial class keyform : Form
    {

        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public string[] selected;
        public int count;
        public int position_base=0;

        private const int SnapDist = 0;
        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta <= SnapDist;
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnShown(e);
            Screen scn = Screen.FromPoint(this.Location);
            if (DoSnap(this.Left, scn.WorkingArea.Left)) this.Left = scn.WorkingArea.Left;
            if (DoSnap(this.Top, scn.WorkingArea.Top)) this.Top = scn.WorkingArea.Top;
            if (DoSnap(scn.WorkingArea.Right, this.Right)) this.Left = scn.WorkingArea.Right - this.Width;
            if (DoSnap(scn.WorkingArea.Bottom, this.Bottom)) this.Top = scn.WorkingArea.Bottom - this.Height;
            if (this.Bottom <= configform.cf.Bottom || this.Top<configform.cf.Bottom) this.Top = configform.cf.Top-this.Size.Height;
       
        }


        public keyform()
        {
            InitializeComponent();
             this.Location = new Point(100, 100);
             this.Cursor = System.Windows.Forms.Cursors.Hand;
             this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
             this.ShowInTaskbar = false;
        /*   this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
             this.ForeColor = System.Drawing.SystemColors.Desktop;
             this.BackColor = System.Drawing.Color.Blue;      
             this.Opacity = 0.1;*/
             this.AutoSize = true;
        }

/*************************************************************************
*****************************click event handller*************************
**************************************************************************/
       private  void btn_Click(object sender, EventArgs e)
        {
           
            string name = ((Button)sender).Name;
            switch (name)
            {
               case "up_key":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
                    break;
                case "left_key":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
                    break;
                case "down_key":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
                    break;
                case "right_key":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
                    break;
            /*    case "altf4_key":
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.MENU);
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.F4);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.MENU);
                    break;
                case "backspace_key":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
                    break;
                case "ctrlv_key":
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_V);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    break;
                case "ctrlc_key":
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_C);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);

                    break;*/
                default:
                   string keys="";             
                   foreach (config_shortcuts shortcut in configform.config_map)
                   {
                       if (shortcut.name == name)
                       {
                           keys = shortcut.keyvalue;
                       }
                   }
                   string[] each_key = keys.Split('+');

                       foreach (string key in each_key)
                       {

                           Type typ = typeof(VirtualKeyCode);
                           InputSimulator.SimulateKeyDown((VirtualKeyCode)Enum.Parse(typ, key));
                       }
                       foreach (string key in each_key.Reverse<string>())
                       {
                            Type typ = typeof(VirtualKeyCode);
                            InputSimulator.SimulateKeyUp((VirtualKeyCode)Enum.Parse(typ, key));
                       }

                    break;

            } 
       }


       /*************************************************************************
       *****************add the controller onto the form*************************
       **************************************************************************/
        private void Create_arrowkeys()
        {
            Button up_key = new Button();
            up_key.Location = new System.Drawing.Point(38, position_base);
            up_key.Name = "up_key";
            up_key.Size = new System.Drawing.Size(72, 34);
            up_key.TabIndex = 0;
            up_key.Text = "up";
            up_key.UseVisualStyleBackColor = true;
 //         System.Drawing.Point p = new Point(12, 13 + i * 30);
            this.Controls.Add(up_key);
            up_key.Click += new EventHandler(btn_Click);


            Button left_key = new Button();
            left_key.Location = new System.Drawing.Point(12, 43);
            left_key.Name = "left_key";
            left_key.Size = new System.Drawing.Size(66, 34);
            left_key.TabIndex = 1;
            left_key.Text = "left";
            left_key.UseVisualStyleBackColor = true;
            this.Controls.Add(left_key);
            left_key.Click += new EventHandler(btn_Click);


            Button down_key = new Button();
            down_key.Location = new System.Drawing.Point(38, 82);
            down_key.Name = "down_key";
            down_key.Size = new System.Drawing.Size(72, 34);
            down_key.TabIndex = 2;
            down_key.Text = "down";
            down_key.UseVisualStyleBackColor = true;
            this.Controls.Add(down_key);
            down_key.Click += new EventHandler(btn_Click);

           
            Button right_key = new Button();
            right_key.Location = new System.Drawing.Point(72, 43);
            right_key.Name = "right_key";
            right_key.Size = new System.Drawing.Size(66, 34);
            right_key.TabIndex = 3;
            right_key.Text = "right";
            right_key.UseVisualStyleBackColor = true;
            this.Controls.Add(right_key);
            right_key.Click += new EventHandler(btn_Click);


            position_base += 126; ;
        }
 /*       private void Create_altf4()
        {
            Button altf4_key = new Button();
            altf4_key.Location = new System.Drawing.Point(12, position_base);
            altf4_key.Name = "altf4_key";
            altf4_key.Size = new System.Drawing.Size(198, 34);
            altf4_key.TabIndex = 3;
            altf4_key.Text = "alt+f4";
            altf4_key.UseVisualStyleBackColor = true;
            this.Controls.Add(altf4_key);
            altf4_key.Click += new EventHandler(btn_Click);
            position_base += 44;

        }

        private void Create_backspace()
        {
            Button backspace_key = new Button();
            backspace_key.Location = new System.Drawing.Point(12, position_base);
            backspace_key.Name = "backspace_key";
            backspace_key.Size = new System.Drawing.Size(198, 34);
            backspace_key.TabIndex = 3;
            backspace_key.Text = "backspace";
            backspace_key.UseVisualStyleBackColor = true;
            this.Controls.Add(backspace_key);
            backspace_key.Click += new EventHandler(btn_Click);
            position_base += 44;

        }
        private void Create_ctrlv()
        {

            Button ctrlv_key = new Button();
            ctrlv_key.Location = new System.Drawing.Point(12, position_base);
            ctrlv_key.Name = "ctrlv_key";
            ctrlv_key.Size = new System.Drawing.Size(198, 34);
            ctrlv_key.TabIndex = 3;
            ctrlv_key.Text = "ctrl+v";
            ctrlv_key.UseVisualStyleBackColor = true;
            this.Controls.Add(ctrlv_key);
            ctrlv_key.Click += new EventHandler(btn_Click);
            position_base += 44;
        }
        private void Create_ctrlc()
        {
            Button ctrlc_key = new Button();
            ctrlc_key.Location = new System.Drawing.Point(12, position_base);
            ctrlc_key.Name = "ctrlc_key";
            ctrlc_key.Size = new System.Drawing.Size(198, 34);
            ctrlc_key.TabIndex = 3;
            ctrlc_key.Text = "ctrl+c";
            ctrlc_key.UseVisualStyleBackColor = true;
            this.Controls.Add(ctrlc_key);
            ctrlc_key.Click += new EventHandler(btn_Click);
            position_base += 44;
        }*/
        private void Create_config(config_shortcuts shortcut)
        {
            Button config_key = new Button();
            config_key.Location = new System.Drawing.Point(12, position_base);
            config_key.Name = shortcut.name;
            config_key.Size = new System.Drawing.Size(120, 44);
            config_key.TabIndex = 3;
            config_key.Text = shortcut.name;
            config_key.UseVisualStyleBackColor = true;
            this.Controls.Add(config_key);
            config_key.Click += new EventHandler(btn_Click);

            position_base += 46;
        }

        private void Create_warninglabel()
        {
            Label warninglabel = new Label();
            warninglabel.Name = "warninglabel";
            warninglabel.Text = "No Key!";
            this.Controls.Add(warninglabel);
        }



/*************************************************************************
********************************Form load*********************************
**************************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            bool no_key = true;
            int pos = Array.IndexOf(selected, "arrowkeys");
            if (pos >- 1)
            {
                Create_arrowkeys();
                no_key = false;
            }
   /*         pos = Array.IndexOf(selected, "alt+f4(quit)");
            if (pos > -1)
            {
                Create_altf4();
                no_key = false;

            }
            pos = Array.IndexOf(selected, "backspace");
            if (pos > -1)
            {
                Create_backspace();
                no_key = false;

            }
            pos = Array.IndexOf(selected, "ctrl+c(copy)");
            if (pos > -1)
            {
                Create_ctrlc();
                no_key = false;

            }
            pos = Array.IndexOf(selected, "ctrl+v(paste)");
            if (pos > -1)
            {
                Create_ctrlv();
                no_key = false;

            }*/
            foreach (config_shortcuts shortcut in configform.config_map)
            {
                if (Array.IndexOf(selected, shortcut.name) > -1)
                {
                    Create_config(shortcut);
                    no_key = false;
                }

            }

            if (no_key == true)
            {
                Create_warninglabel();

            }



        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MessageBox.Show("you force quit yourself");
            Application.Exit();
        }




  /*      protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left & this.WindowState == FormWindowState.Normal)
            {
                
                this.Capture = false;
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }    */   

	    protected override CreateParams CreateParams
	    {

	        get
	          {
	            CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                return cp;
	            }
	     }

    }
}
