using Chess.Models.MoveTypes;
using Chess.Models.Pieces;
using Chess.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chess.Models
{
    public class Board : IBoardPathVerifier, IMoveExecutor
    {
        private const int BoardLength = 7;
        private readonly Player black;
        private readonly Player white;
        private readonly List<Field> fields;
        private readonly RelocationPattern pattern = new ();
        private readonly History history = new ();
        private Player ongoing;
        private Player awaiting;

        public Board()
        {
            fields = InitBoard();
            black = new Player(new[] { 0, 1 }, this);
            white = new Player(new[] { BoardLength, BoardLength - 1 }, this);
            AddPlayers(black.Concat(white));
        }

        public bool ExecuteMove(string move, int index, bool direction, out IEnumerable<IPiece> pieces)
        {
            if (direction)
            {
                if (index < history.Count - 1)
                {
                    return ExecuteFromHistory(index + 1, out pieces);
                }

                (ongoing, awaiting) = index % (1 + 1) == 0 ? (white, black) : (black, white);
                return ExecuteForward(move, out pieces);
            }

            return ExecuteFromHistory(index + 1, out pieces);
        }

        public bool IsFreePath(Pieces.Position current, Pieces.Position upcoming, bool capture)
        {
            if (current == null || upcoming == null)
            {
                return false;
            }

            var (distance, x, y) = GetCoordinates(current, upcoming);
            return Enumerable.Range(1, distance)
                             .Select(item => fields
                             .FirstOrDefault(field => field.Position.Equals(new (current.Rank + (item * y), current.File + (item * x)))))
                             .SkipLast(capture ? 1 : 0)
                             .All(field => field.IsFree);
        }

        public bool IsFreePath(Pieces.Position upcoming)
        {
            return fields.FirstOrDefault(x => x.Position.Equals(upcoming)).IsFree;
        }

        public bool TryUpdatePath(Pieces.Position current, Pieces.Position upcoming)
        {
            if (current == null || upcoming == null)
            {
                return false;
            }

            fields.FirstOrDefault(field => field.Position.Equals(current)).IsFree = true;
            fields.FirstOrDefault(field => field.Position.Equals(upcoming)).IsFree = false;
            return true;
        }

        public bool IsPossibleEnPassant()
        {
            var lastMove = history[^(1 + 1)].Key;
            var regex = new Regex("^[a-h]5$");
            return regex.IsMatch(lastMove);
        }

        private bool ExecuteFromHistory(int index, out IEnumerable<IPiece> pieces)
        {
            pieces = history[index].Value;
            return pieces != null;
        }

        private bool ExecuteForward(string move, out IEnumerable<IPiece> pieces)
        {
            var relocationType = pattern.Solve(move);
            var relocation = GetRelocationType(relocationType, move);
            if (relocation?.Act(relocationType) != true)
            {
                pieces = null;
                return false;
            }

            pieces = ongoing.Concat(awaiting);
            history.Add(move, pieces);
            return true;
        }

#pragma warning disable S1541 // Methods and properties should not be too complex
        private IMove GetRelocationType(RelocationType type, string move)
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            return type switch
            {
                RelocationType.Move => new Move(move, GetPieces(move[0], true)),
                RelocationType.Check => new CheckMove(move, GetPieces(move[0], true), GetPieces('K', false).First()),
                RelocationType.Capture => new CapturingMove(move, GetPieces(move[0], true), awaiting),
                RelocationType.Castling => new CastlingMove(move, GetPieces('K', true).First(), GetPieces('R', true)),
                RelocationType.CheckMate => new CheckMate(move, GetPieces(move[0], true), GetPieces('K', false).First(), ongoing),
                RelocationType.Promotion => new PromotionMove(move, GetPieces(move[0], true), awaiting),
                RelocationType.DisambiguousMove => new DisambiguatingMove(move, GetPieces(move[0], true)),
                RelocationType.DisambiguousCapture => new DisambiguatingCaptureMove(move, GetPieces(move[0], true), awaiting),
                RelocationType.CapturingPromotion => new CapturingPromotionMove(move, GetPieces(move[0], true), awaiting, ongoing),
                _ => null
            };
        }

        private IEnumerable<IPiece> GetPieces(char name, bool currentTeam)
        {
            var team = currentTeam ? ongoing : awaiting;
            return name switch
            {
                'R' => team.Where(x => x.GetType().Name == "Rook"),
                'N' => team.Where(x => x.GetType().Name == "Knight"),
                'B' => team.Where(x => x.GetType().Name == "Bishop"),
                'Q' => team.Where(x => x.GetType().Name == "Queen"),
                'K' => team.Where(x => x.GetType().Name == "King"),
                _ => team.Where(x => x.GetType().Name == "Pawn"),
            };
        }

        private void AddPlayers(IEnumerable<IPiece> players)
        {
            foreach (var piece in players)
            {
                fields.FirstOrDefault(field => field.Position.Equals(piece.Current)).IsFree = false;
            }

            history.Add("", players);
        }

        private List<Field> InitBoard()
        {
            return Enumerable.Range(0, BoardLength + 1)
                             .SelectMany(row => Enumerable.Range(0, BoardLength + 1)
                             .Select(col => new Field { Position = new (row, col), IsFree = true }))
                             .ToList();
        }

        private (int, int, int) GetCoordinates(Pieces.Position current, Pieces.Position next)
        {
            var distance = Math.Abs(current.Rank - next.Rank);
            var x = Math.Sign(next.File - current.File);
            var y = Math.Sign(next.Rank - current.Rank);
            return (distance == 0 ? Math.Abs(current.File - next.File) : distance, x, y);
        }
    }
}
