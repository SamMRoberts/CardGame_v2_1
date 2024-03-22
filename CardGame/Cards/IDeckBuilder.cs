namespace SamMRoberts.CardGame.Cards
{
    public interface IDeckBuilder<TFace, TSuit>
        where TFace : Enum
        where TSuit : Enum
    {
        Deck<Card> BuildDeck();
    }
}