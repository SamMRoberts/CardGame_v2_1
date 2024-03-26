using SamMRoberts.CardGame.Games;

namespace SamMRoberts.CardGame.Cards
{
    public struct CardHolder : ICardHolder
    {
        public enum Visibility
        {
            FaceUp,
            FaceDown,
            Hidden
        }
        public Card Card
        {
            get => Visible == Visibility.FaceUp ? _card : default;
            set => _card = value;
        }
        public Card Peek => Owner is IPlayer ? Card : default;
        public readonly object Owner;
        public Visibility Visible { get; set; }
        public Enum Face { get => Card.Face; }
        public string FaceSymbol { get => Card.FaceSymbol; }
        public Enum Suit { get => Card.Suit; }
        public char SuitSymbol { get => Card.SuitSymbol; }
        private Card _card;
        public CardHolder(object owner, Card card, Visibility visible)
        {
            Owner = owner;
            Card = card;
            Visible = visible;
        }

        public override string ToString()
        {
            if (Visible == Visibility.FaceUp)
            {
                return $"[{Card.FaceSymbol}{Card.SuitSymbol}]";
            }
            {
                return "[XX]";
            }
        }
    }
}