using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalypsoResultConverter;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string testpath = @"C:\Users\Public\Documents\Zeiss\CALYPSO\workarea\results\";
            var test = new CalypsoTableResult(testpath);
            test.ResultAdded += Test_ResultAdded;
        }

        private void Test_ResultAdded(object sender, CalypsoResultEventArgs cre)
        {
            var temp = cre.op_result;
        }
    }
}
