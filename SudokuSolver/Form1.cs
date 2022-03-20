using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }
        public static TextBox[,] grid = new TextBox[9, 9];
        private void Form1_Load(object sender, EventArgs e)
        {
            TileGrid(35,4,11);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            string[,] input = new string[9, 9];

            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                    input[i, j] = grid[i, j].Text;

            SudokuGrid sg1 = new SudokuGrid(input);

            sg1.Solve();

            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                    grid[i, j].Text = sg1[i, j].Digit.ToString();
        }

        private void TileGrid(int _size, int gap1, int gap2)
        {
            Size size = new Size(_size, _size);
            float fontSize = (_size - 8) / 1.5f;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new TextBox()
                    {
                        Multiline = true,
                        Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162))),
                        Location = new Point(15 + j * _size + j * gap1 + j / 3 * gap2, 15 + i * _size + i * gap1 + i / 3 * gap2),
                        Size = size,
                        //Text = i.ToString() + "," + j.ToString(),
                        //Text = "9",
                        TabIndex = i * 9 + j,
                        TextAlign = HorizontalAlignment.Center
                    };
                    Controls.Add(grid[i, j]);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox cell in grid)
                cell.Clear();
        }
    }
}
