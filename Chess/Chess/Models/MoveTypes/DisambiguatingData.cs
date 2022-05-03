using Chess.Models.Pieces;
using System;

namespace Chess.Models.MoveTypes
{
    public class DisambiguatingData
    {
        public DisambiguatingData(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            (File, Rank) = GetDisambiguatingData(data);
        }

        public int File { get; }

        public int Rank { get; }

        public bool IsDisambiguating(IPiece piece)
        {
            if (piece == null)
            {
                return false;
            }

            return (File < 0, Rank < 0) switch
            {
                (true, false) => piece.Current.Rank == Rank,
                (false, true) => piece.Current.File == File,
                _ => piece.Current.File == File && piece.Current.Rank == Rank
            };
        }

        private (int, int) GetDisambiguatingData(string data)
        {
            const string files = "abcdefgh";
            const int gridLength = 8;
            if (data.Length > 1)
            {
                return (files.IndexOf(data[0]), gridLength - (data[0] - '0'));
            }

            return char.IsDigit(data[0]) ? (-1, gridLength - (data[0] - '0')) : (files.IndexOf(data[0]), -1);
        }
    }
}
