using System;

namespace Chess.Models.Pieces
{
    public class Position
    {
        const int ChessGridLength = 8;

        public Position(char rank, char file)
        {
            File = "abcdefgh".IndexOf(file);
            Rank = ChessGridLength - (rank - '0');
        }

        public Position(int rank, int file)
        {
            File = file;
            Rank = rank;
        }

        public int File { get; }

        public int Rank { get; }

        public bool Equals(Position other)
        {
            VerifyNullParam(other);
            return File == other.File && Rank == other.Rank;
        }

        public bool IsOutOfBounds()
        {
            return File < 0 || File > ChessGridLength - 1 ||
                   Rank < 0 || Rank > ChessGridLength - 1;
        }

        private void VerifyNullParam(object param)
        {
            if (param != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(param));
        }
    }
}
