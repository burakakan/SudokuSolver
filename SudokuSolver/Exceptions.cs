using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class InvalidAssignmentException : Exception
    {
        private string _msg;
        public InvalidAssignmentException(byte i, byte j)
        {
            _msg = $"Cannot assign a new digit to the fixed cell at row {i} and column {j}.";
        }

        public override string Message => _msg;
    }
    class NoSolutionException : Exception
    {
        private string _msg;
        public NoSolutionException()
        {
            _msg = "The puzzle stated has no valid solution";
        }

        public override string Message => _msg;
    }
}
