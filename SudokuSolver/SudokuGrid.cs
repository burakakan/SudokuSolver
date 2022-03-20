using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class SudokuGrid
    {
        public SudokuGrid(string[,] input)
        {
            Grid = new Cell[9, 9];

            for (byte i = 0; i < 9; i++)
                for (byte j = 0; j < 9; j++)
                {
                    if (string.IsNullOrEmpty(input[i, j]))
                    {
                        Grid[i, j].Digit = 0;
                        Grid[i, j].Fixed = false;
                    }
                    else
                    {
                        Grid[i, j].Digit = Convert.ToByte(input[i, j]);
                        Grid[i, j].Fixed = true;
                    }
                }
        }
        public Cell[,] Grid { get; set; }

        public void Solve()
        {
            byte d;
            for (byte i = 0; i < 9; i++) //row
            {
                for (byte j = 0; j < 9;) //column
                {
                    Form1.grid[i, j].BackColor = Color.Aquamarine;
                    //MessageBox.Show("");
                    if (!Grid[i, j].Fixed)
                    {
                        d = Grid[i, j].Digit;
                        do
                            d++;
                        while (Illegal(i, j, d) && d <= 9);

                        if (d > 9)
                        {
                            Grid[i, j].Digit = 0;
                            Form1.grid[i, j].Text = "";

                            (i, j) = PreviousNonFixed(i, j);
                            continue;
                        }
                        Grid[i, j].Digit = d;
                        Form1.grid[i, j].Text = d.ToString();
                        //MessageBox.Show("");
                    }
                    Form1.grid[i, j].BackColor = Color.White;
                    j++; // if d is not over 9 continue to the next cell
                }
            }
        }

        private bool Illegal(byte i_d, byte j_d, byte d)
        {
            for (byte i = 0; i < 9; i++)
                if (Grid[i, j_d].Digit == d && i_d != i)
                    return true;

            for (byte j = 0; j < 9; j++)
                if (Grid[i_d, j].Digit == d && j_d != j)
                    return true;

            byte i_lower = (byte)(i_d / 3 * 3);
            byte i_upper = (byte)(i_lower + 3);

            byte j_lower = (byte)(j_d / 3 * 3);
            byte j_upper = (byte)(j_lower + 3);

            for (byte i = i_lower; i < i_upper; i++)
                for (byte j = j_lower; j < j_upper; j++)
                    if (Grid[i, j].Digit == d && (j_d != j || i_d != i))
                        return true;

            return false;
        }
        
        private (byte,byte) PreviousNonFixed(byte i, byte j)
        {
            do
            {
                //if (j != 0)
                //    j--;
                //else
                //{
                //    j = 8;
                //    i--;
                //}

                (i, j) = j != 0 ? ((byte, byte))(i, j - 1) : ((byte, byte))(i - 1, 8);
            }
            while (Grid[i, j].Fixed);

            return (i, j);
        }

        public Cell this[byte i, byte j]
        {
            get { return Grid[i, j]; }
            set
            {
                if (!Grid[i, j].Fixed)
                    Grid[i, j] = value;
                else
                    throw new InvalidAssignmentException(i, j);
            }
        }
        internal struct Cell
        {
            public byte Digit { get; set; }
            public bool Fixed { get; set; }
        }
    }
}
