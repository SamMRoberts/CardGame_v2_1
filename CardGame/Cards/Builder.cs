namespace SamMRoberts.CardGame.Cards
{
    public class Builder<TFace, TSuit>: IDeckBuilder<TFace, TSuit>
        where TFace : Enum
        where TSuit : Enum
    {
        public Deck<Card> BuildDeck()
        {
            Deck<Card> deck = new();

            foreach (TFace face in Enum.GetValues(typeof(TFace)))
            {
                foreach (TSuit suit in Enum.GetValues(typeof(TSuit)))
                {
                    deck.AddTop(new Card(face, suit));
                }
            }
            return deck;
        }
    }
}