using System;

namespace Chess.ViewModel
{
    public class Position
    {
        public Position(int rank, int file)
        {
            File = file;
            Rank = rank;
        }

        public int File { get; set; }

        public int Rank { get; set; }

        public static explicit operator Position(Models.Pieces.Position position)
        {
            if (position != null)
            {
                return new Position(position.Rank, position.File);
            }

            throw new ArgumentNullException(nameof(position));
        }

        public static Position ToPosition(Position left, Position right)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Position other)
        {
            return File == other.File && Rank == other.Rank;
        }
    }
}
