namespace Chess.ViewModel
{
    public class RelocationSet
    {
        public RelocationSet()
        {
            WhiteBorderBrush = "Transparent";
            BlackBorderBrush = "Transparent";
        }

        public string White { get; set; }

        public string WhiteBorderBrush { get; set; }

        public string Black { get; set; }

        public string BlackBorderBrush { get; set; }
    }
}
