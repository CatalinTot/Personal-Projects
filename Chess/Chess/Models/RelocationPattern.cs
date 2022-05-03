using System.Text.RegularExpressions;

namespace Chess.Models
{
    public enum RelocationType
    {
        Move = 0,
        DisambiguousMove,
        DisambiguousCapture,
        Capture,
        Castling,
        Check,
        CheckMate,
        Promotion,
        CapturingPromotion,
        Default = -1
    }

    public class RelocationPattern
    {
        readonly Regex[] patterns;

        public RelocationPattern()
        {
            var move = new Regex("^[a-h][1-8]$|^[RNBQK][a-h][1-8]$");
            var disambiguous = new Regex("^[RNBQK][a-h][a-h][1-8]$|^[RNBQK][a-h][1-8][a-h][1-8]$");
            var disambiguousCapture = new Regex("^[RNBQK][a-h]x[a-h][1-8]$|^[RNBQK][a-h][1-8]x[a-h][1-8]$|^[a-h]x[a-h][1-8]$");
            var capture = new Regex("^[RNBQK]x[a-h][1-8]$");
            var castling = new Regex(@"^[O\-O]|[O\-O\-O]$");
            var check = new Regex(@"^[RNBQK][a-h][1-8]\+$|^[a-h][1-8]\+$");
            var checkmate = new Regex("^[RNBQK][a-h][1-8]#$|^[a-h][1-8]#$");
            var promotion = new Regex("^[a-h]8=[RNBQ]$");
            var capturingPromotion = new Regex("^[a-h]x[a-h]8=[RNBQ]$");

            patterns = new[] { move, disambiguous, disambiguousCapture, capture, castling, check, checkmate, promotion, capturingPromotion };
        }

        public RelocationType Solve(string move)
        {
            for (int i = 0; i < patterns.Length; i++)
            {
                if (patterns[i].IsMatch(move))
                {
                    return (RelocationType)i;
                }
            }

            return (RelocationType)(-1);
        }
    }
}
