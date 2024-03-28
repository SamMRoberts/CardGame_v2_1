using System.Collections;
using SamMRoberts.CardGame.Games;

namespace SamMRoberts.CardGame.Cards;

public struct CardHolder : ICardHolder
{
    public CardHolder(object owner, Card card, Visibility visible = Visibility.Hidden)
    {
        Owner = owner;
        Card = card;
        Visible = visible;
    }

    private Card _card;
    public readonly object Owner;

    public enum Visibility
    {
        Hidden,
        FaceUp,
        FaceDown
    }

    public Card Card
    {
        get => Visible == Visibility.FaceUp ? _card : default;
        set => _card = value;
    }

    public Card Peek => Owner is IPlayer ? Card : default;
    public Visibility Visible { get; set; }
    public Enum Face { get => Card.Face; }
    public string FaceSymbol { get => Card.FaceSymbol; }
    public Enum Suit { get => Card.Suit; }
    public char SuitSymbol { get => Card.SuitSymbol; }

    public override string ToString()
    {
        if (Visible == Visibility.FaceUp)
        {
            return $"[{Card.FaceSymbol}{Card.SuitSymbol}]";
        }
        else if (Visible == Visibility.FaceDown)
        {
            return "[XX]";
        }
        else
        {
            return string.Empty;
        }
    }
}