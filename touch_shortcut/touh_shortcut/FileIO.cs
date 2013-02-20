using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace touch_shortcut
{
    class config_shortcuts
    {
        public string name;
        public string keyvalue;

        public config_shortcuts(string n, string k){
                this.name=n;
                this.keyvalue=k;
        }

    }


    class FileIO
    {
        public static string[] lines;
        private String name;
        public FileIO(String fname)
        {
           this.name=fname;
        }

        public void read_custom(){
            lines = System.IO.File.ReadAllLines(name);
        }
    }
}
