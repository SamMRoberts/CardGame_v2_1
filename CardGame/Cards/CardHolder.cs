namespace SamMRoberts.CardGame.Cards
{
    public struct CardHolder : ICardHolder
    {
        public enum Visibility
        {
            Hidden,
            VisibleToOwner,
            VisibleToAll,
        }
        public Card Card { get; set; }
        public Visibility Visible { get; set; }
        public readonly Enum Face { get => Card.Face; }
        public readonly string FaceSymbol { get => Card.FaceSymbol; }
        public readonly Enum Suit { get => Card.Suit; }
        public readonly char SuitSymbol { get => Card.SuitSymbol; }
        public CardHolder(Card card, Visibility visible)
        {
            Card = card;
            Visible = visible;
        }

        public readonly override string ToString() => $"{Card.FaceSymbol}{Card.SuitSymbol}";
    }
}