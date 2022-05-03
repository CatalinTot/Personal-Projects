using Chess.Models.Pieces;
using System.Collections.Generic;

namespace Chess.Models
{
    public class History
    {
        private readonly List<KeyValuePair<string, List<IPiece>>> items = new ();

        public int Count => items.Count;

        public KeyValuePair<string, List<IPiece>> this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                {
                    return new KeyValuePair<string, List<IPiece>>("", null);
                }

                return items[index];
            }
        }

        public void Add(string move, IEnumerable<IPiece> pieces)
        {
            if (pieces == null)
            {
                return;
            }

            var list = new List<IPiece>();
            foreach (var piece in pieces)
            {
                list.Add(ClonePiece(piece));
            }

            items.Add(new KeyValuePair<string, List<IPiece>>(move, list));
        }

        private IPiece ClonePiece(IPiece piece)
        {
            return piece.GetType().Name switch
            {
                "Rook" => new Rook(piece.Verifier) { Id = piece.Id, Current = piece.Current },
                "Knight" => new Knight(piece.Verifier) { Id = piece.Id, Current = piece.Current },
                "Bishop" => new Bishop(piece.Verifier) { Id = piece.Id, Current = piece.Current },
                "Queen" => new Queen(piece.Verifier) { Id = piece.Id, Current = piece.Current },
                "King" => new King(piece.Verifier) { Id = piece.Id, Current = piece.Current },
                _ => new Pawn(piece.Verifier) { Id = piece.Id, Current = piece.Current },
            };
        }
    }
}
