using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Models.Pieces;

namespace Chess.Models
{
    public class Player : IEnumerable<IPiece>, IPlayerRelocationHelper
    {
        const int ChessBoardLength = 7;
        private readonly List<IPiece> team;

        public Player(int[] rows, IBoardPathVerifier verifier)
        {
            team = new List<IPiece>
            {
                new Rook(verifier),
                new Knight(verifier),
                new Bishop(verifier),
                new Queen(verifier),
                new King(verifier),
                new Bishop(verifier),
                new Knight(verifier),
                new Rook(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier),
                new Pawn(verifier)
            };

            VerifyNullParam(rows);
            Init(rows);
        }

        public bool TryRemoveFrom(Position upcoming)
        {
            if (upcoming == null)
            {
                return false;
            }

            var enpassant = new Position('5', upcoming.File);
            var piece = team.FirstOrDefault(x => x.Current.Equals(upcoming) || (x.Current.Equals(enpassant) && x.Verifier.IsPossibleEnPassant()));
            return team.Remove(piece);
        }

        public IEnumerator<IPiece> GetEnumerator()
        {
            foreach (var piece in team)
            {
                yield return piece;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsCheckMate(IPiece rivalKing)
        {
            var possibleEscapes = new[] { 1, 0, -1 };
            return possibleEscapes.SelectMany(row => possibleEscapes
                                  .Select(col => new Position(rivalKing.Current.Rank + row, rivalKing.Current.File + col)))
                                  .Where(position => !position.IsOutOfBounds())
                                  .Select(position => team
                                  .Any(teammate => teammate.TryMove(position, RelocationType.Check)))
                                  .All(isMate => isMate);
        }

        public bool TryPromote(IPiece pawn, IPiece promote)
        {
            VerifyNullParam(pawn);
            VerifyNullParam(promote);
            if (!team.Remove(pawn))
            {
                return false;
            }

            team.Add(promote);
            return true;
        }

        private void Init(int[] rows)
        {
            VerifyNullParam(rows);
            int count = 0;
            foreach (var row in rows)
            {
                for (int col = 0; col <= ChessBoardLength; col++)
                {
                    team[count].Current = new Position(row, col);
                    team[count].Id = $"{row}{col}";
                    count++;
                }
            }
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
