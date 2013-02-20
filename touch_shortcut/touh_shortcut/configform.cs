using System;
using System.Collections;
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
    public partial class configform : Form
    {
        public static keyform kf = new keyform();
        public static circleform cf = new circleform();
        public static ArrayList config_map=new ArrayList();     //store all the content from file

        public configform()
        {
            InitializeComponent();
            FileIO io = new FileIO("./config.txt");
            io.read_custom();
            foreach (string line in FileIO.lines)
            {
                if (line.Trim() != "" && line.Trim().IndexOf('%')!=0)
                {
                    string[] new_shortcut = line.Split(null as string[], StringSplitOptions.RemoveEmptyEntries); //pass white space for splitting
                    config_shortcuts tmp = new config_shortcuts(new_shortcut[0], new_shortcut[1]);
                    config_map.Add(tmp);
                    checkedListBox1.Items.Add(new_shortcut[0]);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int i = 0;
            kf.selected = new string[checkedListBox1.CheckedItems.Count];
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                kf.selected[i] = itemChecked.ToString();
                i++;              
            }
            kf.count = checkedListBox1.SelectedItems.Count;
            cf.Show();
            this.Hide();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
