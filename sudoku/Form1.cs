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
    public partial class Form1 : Form
    {
        int[,] num = new int[9, 9];
        Button[,] cell = new Button[9, 9];
        Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        Form2 f2 = new Form2();

        public Form1()
        {
            InitializeComponent();

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    num[i, j] = ((j * 3 + j / 3 + i) % 9 + 1);

                    cell[i, j] = new Button();

                    cell[i, j].Size = new Size(40, 40);
                    cell[i, j].FlatStyle = FlatStyle.Flat;
                    cell[i, j].FlatAppearance.BorderSize = 0;
                    cell[i, j].Font = new Font("century gothic", 20, FontStyle.Bold);
                    cell[i, j].Top = 25 + j * 42;
                    cell[i, j].Left = 25 + i * 42;
                    cell[i, j].Text = num[i, j].ToString();

                    int div = j / 3;
                    switch (div)
                    {
                        case 0:
                            cell[i, j].BackColor = Color.LightBlue;

                            if (i / 3 == 1)
                                cell[i, j].BackColor = Color.LightSlateGray;
                            break;
                        case 1:
                            cell[i, j].BackColor = Color.LightSlateGray;

                            if (i / 3 == 1)
                                cell[i, j].BackColor = Color.LightBlue;
                            break;
                        case 2:
                            goto case 0;
                    }

                    if (cell[i, j].BackColor == Color.LightBlue)
                        cell[i, j].ForeColor = Color.DimGray;
                    else
                        cell[i, j].ForeColor = Color.White;

                    this.Controls.Add(cell[i, j]);
                    this.cell[i, j].Click += new System.EventHandler(this.cell_Click);
                }
            }

            Swap(20);
        }

        private void SwapSelector(int a, int b)
        {
            switch (a)
            {
                case 0:
                    b = rnd.Next(1, 3);
                    break;
                case 1:
                    b = rnd.Next(0, 3);
                    break;
                case 2:
                    b = rnd.Next(0, 2);
                    break;
                case 3:
                    b = rnd.Next(4, 6);
                    break;
                case 4:
                    b = rnd.Next(3, 6);
                    break;
                case 5:
                    b = rnd.Next(3, 5);
                    break;
                case 6:
                    b = rnd.Next(7, 9);
                    break;
                case 7:
                    b = rnd.Next(6, 9);
                    break;
                case 8:
                    b = rnd.Next(6, 8);
                    break;
            }
        }

        private int Swap(int counter)
        {
            if (counter <= 0)
                return 0;

            int k = rnd.Next(9);
            int z = 0;

            SwapSelector(k, z);

            for (int j = 0; j < 9; j++)
            {
                string temp;
                temp = cell[k, j].Text;
                cell[k, j].Text = cell[z, j].Text;
                cell[z, j].Text = temp;
            }

            int x = rnd.Next(9);
            int y = 0;

            SwapSelector(x, y);

            for (int i = 0; i < 9; i++)
            {
                string temp;
                temp = cell[i, x].Text;
                cell[i, x].Text = cell[i, y].Text;
                cell[i, y].Text = temp;
            }
            return Swap(counter - 1);
        }

        private void HideNumbers()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int hide = rnd.Next(2);
                    if (hide == 1)
                        cell[i, j].Text = "";
                }
            }
        }

        private void ShowNumbers()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cell[i, j].Text == "")
                        cell[i, j].Text = num[i, j].ToString();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                HideNumbers();
            else
                ShowNumbers();
        }

        private void cell_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (checkBox1.Checked && b.Text == "")
            {
                f2.Location = new Point(this.Left + b.Left + b.Width + 10, this.Top + b.Top + b.Height + 32);
                if (f2.ShowDialog() == DialogResult.OK)
                {
                    b.Text = f2.entered;
                }
            }
        }
    }
}

