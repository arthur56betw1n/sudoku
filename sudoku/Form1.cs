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
        MyButton[,] cell = new MyButton[9, 9];
        Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        Form2 inputform = new Form2();
        int lives;
        DateTime time;

        public Form1()
        {
            InitializeComponent();

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    num[i, j] = ((j * 3 + j / 3 + i) % 9 + 1);

                    cell[i, j] = new MyButton();

                    cell[i, j].Size = new Size(40, 40);
                    cell[i, j].FlatStyle = FlatStyle.Flat;
                    cell[i, j].FlatAppearance.BorderSize = 0;
                    cell[i, j].Font = new Font("century gothic", 20, FontStyle.Bold);
                    cell[i, j].Top = 25 + j * 42;
                    cell[i, j].Left = 25 + i * 42;
                    cell[i, j].Value = num[i, j];
                    cell[i, j].Text = cell[i, j].Value.ToString();

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

            GenerateNewGame();
        }

        private void RefreshCellText()
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    cell[i, j].Value = num[i, j];
                    cell[i, j].Text = cell[i, j].Value.ToString();
                }
            }
        }

        private int Swap(int counter)
        {
            if (counter <= 0)
                return 0;

            int k = rnd.Next(9);
            int z = 0;

            switch (k)
            {
                case 0:
                    z = rnd.Next(1, 3);
                    break;
                case 1:
                    z = rnd.Next(0, 3);
                    break;
                case 2:
                    z = rnd.Next(0, 2);
                    break;
                case 3:
                    z = rnd.Next(4, 6);
                    break;
                case 4:
                    z = rnd.Next(3, 6);
                    break;
                case 5:
                    z = rnd.Next(3, 5);
                    break;
                case 6:
                    z = rnd.Next(7, 9);
                    break;
                case 7:
                    z = rnd.Next(6, 9);
                    break;
                case 8:
                    z = rnd.Next(6, 8);
                    break;
            }

            for (int j = 0; j < 9; j++)
            {
                int temp;
                temp = num[k, j];
                num[k, j] = num[z, j];
                num[z, j] = temp;
            }

            int x = rnd.Next(9);
            int y = 0;

            switch (x)
            {
                case 0:
                    y = rnd.Next(1, 3);
                    break;
                case 1:
                    y = rnd.Next(0, 3);
                    break;
                case 2:
                    y = rnd.Next(0, 2);
                    break;
                case 3:
                    y = rnd.Next(4, 6);
                    break;
                case 4:
                    y = rnd.Next(3, 6);
                    break;
                case 5:
                    y = rnd.Next(3, 5);
                    break;
                case 6:
                    y = rnd.Next(7, 9);
                    break;
                case 7:
                    y = rnd.Next(6, 9);
                    break;
                case 8:
                    y = rnd.Next(6, 8);
                    break;
            }

            for (int i = 0; i < 9; i++)
            {
                int temp;
                temp = num[i, x];
                num[i, x] = num[i, y];
                num[i, y] = temp;
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

        private void cell_Click(object sender, EventArgs e)
        {
            MyButton b = (MyButton)sender;
            if (b.Text == "")
            {
                inputform.Location = new Point(this.Left + b.Left + b.Width + 10, this.Top + b.Top + b.Height + 32);
                if (inputform.ShowDialog() == DialogResult.OK)
                {
                    if (inputform.entered == b.Value.ToString())
                    {
                        b.Text = inputform.entered;
                        CheckGameOver();
                    }
                    else
                    {
                        lives--;
                        labelLives.Text = lives.ToString();
                        MessageBox.Show("Неверное число!");
                        CheckGameOver();
                    }
                }
            }
        }

        private void GenerateNewGame()
        {
            Swap(25);
            RefreshCellText();
            lives = 3;
            labelLives.Text = lives.ToString();
        }

        private void CheckGameOver()
        {
            bool end = true;
            foreach (MyButton c in this.Controls.OfType<MyButton>())
                if (c.Text == "")
                    end = false;
            if (lives == 0)
            {
                timer1.Stop();
                MessageBox.Show("Игра окончена, слишком много ошибок!\nПопробуй заново!");
                ShowNumbers();
            }
            if (end)
            {
                timer1.Stop();
                MessageBox.Show("Ты справился! Поздравляем!\nТвое время: " + labelTimer.Text);
            }
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateNewGame();
            HideNumbers();
            TimerStart();
        }

        private void сдатьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            ShowNumbers();
            labelLives.Text = "0";
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Заполни свободные клетки цифрами от 1 до 9 так, " +
                "чтобы в каждой строке, в каждом столбце и в каждом малом квадрате 3×3 " +
                "каждая цифра встречалась бы только один раз.");
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("arthur56betw1n, 2018");
        }

        private void TimerStart()
        {
            time = new DateTime(0, 0);
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time.AddSeconds(1);
            labelTimer.Text = time.ToString("mm:ss");
        }

        private void ChangeColorTheme1()
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
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
                }
            }
        }

        private void ChangeColorTheme2()
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int div = j / 3;
                    switch (div)
                    {
                        case 0:
                            cell[i, j].BackColor = Color.LightYellow;

                            if (i / 3 == 1)
                                cell[i, j].BackColor = Color.SandyBrown;
                            break;
                        case 1:
                            cell[i, j].BackColor = Color.SandyBrown;

                            if (i / 3 == 1)
                                cell[i, j].BackColor = Color.LightYellow;
                            break;
                        case 2:
                            goto case 0;
                    }

                    if (cell[i, j].BackColor == Color.LightYellow)
                        cell[i, j].ForeColor = Color.SandyBrown;
                    else
                        cell[i, j].ForeColor = Color.White;
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChangeColorTheme1();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeColorTheme2();
        }
    }
}

