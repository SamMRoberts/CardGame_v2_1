namespace SamMRoberts.CardGame.Cards.Extensions
{
    public static class HandExtensions
    {
        public static string ToString(this Hand<CardHolder> hand)
        {
            var cards = string.Empty;
            foreach (ICardHolder card in hand)
            {
                if (card.Visible == CardHolder.Visibility.FaceUp)
                    cards += $"[{card.Card.ToString()}]";
                else if (card.Visible == CardHolder.Visibility.FaceDown)
                    cards += "[XX]";
            }
            return cards;
        }
    }
}