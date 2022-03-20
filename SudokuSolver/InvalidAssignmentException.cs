using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class InvalidAssignmentException : Exception
    {
        string _msg;
        public InvalidAssignmentException(byte i, byte j)
        {
            _msg = $"Cannot assign a new digit to the cell at row {i} and column {j}. It is a fixed cell.";
        }

        public override string Message => _msg;
    }
}
