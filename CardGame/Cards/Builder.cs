namespace SamMRoberts.CardGame.Cards;

public class Builder<TFace, TSuit>: IDeckBuilder<TFace, TSuit>
    where TFace : Enum
    where TSuit : Enum
{

    public IDeck<Card> BuildDeck<TFaceDelegate>(TFaceDelegate faceDelegate)
        where TFaceDelegate : Delegate
    {
        Deck<Card> deck = new Deck<Card>();

        string faceSymbol;

        foreach (TFace face in Enum.GetValues(typeof(TFace)))
        {
            var faceDelegateResult = faceDelegate.DynamicInvoke(face);
            faceSymbol = faceDelegateResult?.ToString() ?? string.Empty;

            foreach (TSuit suit in Enum.GetValues(typeof(TSuit)))
            {
                deck.AddTop(new Card(face, faceSymbol, suit));
            }
        }
        return deck;
    }
}