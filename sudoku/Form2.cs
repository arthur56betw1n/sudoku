using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string entered;        

        private void Pick(object sender, EventArgs e)
        {
            Button inputbutton = (Button)sender;
            entered = ReturnNumber(inputbutton);

            this.Close();
        }

        private string ReturnNumber(Button inputbutton)
        {
            return inputbutton.Text;
        }

        private void buttonNull_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
