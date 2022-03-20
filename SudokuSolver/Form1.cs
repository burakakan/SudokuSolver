using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            TileGrid(35, 4, 11);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            string[,] input = new string[9, 9];

            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                    input[i, j] = grid[i, j].Text;

            SudokuGrid sudokuGrid = new SudokuGrid(input);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            sudokuGrid.Solve();
            stopwatch.Stop();

            Font font = new Font(grid[0,0].Font.FontFamily, grid[0, 0].Font.Size, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));

            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                {
                    grid[i, j].ReadOnly = true;
                    if (string.IsNullOrEmpty(grid[i, j].Text))
                    {
                        grid[i, j].BackColor = Color.White;
                        grid[i, j].Font = font;
                        grid[i, j].Text = sudokuGrid[i, j].Digit.ToString();
                    }
                    else
                        grid[i, j].BackColor = Color.LightGray;
                }
            MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
        }

        private void TileGrid(int _size, int gap1, int gap2)
        {
            Size size = new Size(_size, _size);
            float fontSize = (_size - 8) / 1.5f;
            Font font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new TextBox()
                    {
                        Multiline = true,
                        Font = font,
                        Location = new Point(15 + j * _size + j * gap1 + j / 3 * gap2, 15 + i * _size + i * gap1 + i / 3 * gap2),
                        Size = size,
                        TabIndex = i * 9 + j,
                        TextAlign = HorizontalAlignment.Center,
                        MaxLength = 1
                    };
                    grid[i,j].GotFocus += new EventHandler(cell_OnFocus);
                    Controls.Add(grid[i, j]);
                }
            }
        }
        private void cell_OnFocus(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                ((TextBox)sender).SelectAll();
            });
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox cell in grid)
            {
                cell.ReadOnly = false;
                cell.Clear();
                cell.BackColor = Color.White;
            }
        }
    }
}
