using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_BD2.Data
{
    public class MyCell : DataGridViewTextBoxCell
    {
        public MyCell() { }
        public MyCell(string s)
        {
            this.Name = s;
        }
        public string Name
        {
            get; set;
        }
        public string Exp
        {
            set; get;
        }
        public int Row
        {
            set; get;
        }
        public int Col
        {
            set; get;
        }
    }
}
