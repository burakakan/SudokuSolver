using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        int pad = 17, cellSize = 40, gap1 = 5, gap2 = 14;
        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            TileGrid(pad, cellSize, gap1, gap2);

            btnSolve.Location = new Point(grid[0, 8].Right + pad*2, pad);
            btnSolve.Size = new Size(3 * cellSize, cellSize);
            btnSolve.Font = new Font("Microsoft Sans Serif", ((float)(cellSize * 52 / 31)) / 4);

            ClientSize = new Size(btnSolve.Right + pad, grid[8, 0].Bottom + pad);

            btnClear.Location = new Point(btnSolve.Location.X, btnSolve.Bottom + gap2);
            btnClear.Size = btnSolve.Size;
            btnClear.Font = btnSolve.Font;

            lblTime.Location = new Point(btnClear.Location.X, btnClear.Bottom + gap2);
            lblTime.Size = new Size(btnSolve.Size.Width, btnSolve.Size.Height * 3);
            lblTime.Font = new Font("Microsoft Sans Serif", ((float)(cellSize * 6 / 5)) / 4);
        }
        public static TextBox[,] grid = new TextBox[9, 9];
        
        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {
                lblTime.Text = "Solving...";
                string[,] input = new string[9, 9];

                for (byte i = 0; i < 9; i++)
                    for (byte j = 0; j < 9; j++)
                        input[i, j] = grid[i, j].Text;

                SudokuGrid sudokuGrid = new SudokuGrid(input);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                sudokuGrid.Solve();
                stopwatch.Stop();

                Font font = new Font(grid[0, 0].Font.FontFamily, grid[0, 0].Font.Size, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));

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

                lblTime.Text = "Solution Time:\n" + stopwatch.ElapsedMilliseconds.ToString() + " ms";
            }
            catch(Exception exc)
            {
                lblTime.Text = "";
                MessageBox.Show(exc.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grid[0, 3].Text = "8";
            grid[0, 5].Text = "1";
            grid[1, 7].Text = "4";
            grid[1, 8].Text = "3";
            grid[2, 0].Text = "5";
            grid[2, 2].Text = "4";
            grid[3, 2].Text = "5";
            grid[3, 4].Text = "7";
            grid[3, 6].Text = "8";
            grid[4, 6].Text = "1";
            grid[5, 1].Text = "2";
            grid[5, 4].Text = "3";
            grid[6, 0].Text = "6";
            grid[6, 7].Text = "7";
            grid[6, 8].Text = "5";
            grid[7, 2].Text = "3";
            grid[7, 3].Text = "4";
            grid[8, 3].Text = "2";
            grid[8, 6].Text = "6";
        }

        private void TileGrid(int pad, int _size, int gap1, int gap2)
        {
            Size size = new Size(_size, _size);
            float fontSize = (_size - 8) / 1.5f;
            Font font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new TextBox()
                    {
                        Multiline = true,
                        Font = font,
                        Location = new Point(pad + j * _size + j * gap1 + j / 3 * gap2, pad + i * _size + i * gap1 + i / 3 * gap2),
                        Size = size,
                        TabIndex = i * 9 + j,
                        TextAlign = HorizontalAlignment.Center,
                        MaxLength = 1
                    };
                    grid[i,j].GotFocus += new EventHandler(cell_OnFocus);
                    Controls.Add(grid[i, j]);
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
            lblTime.Text = "";
        }
    }
}
