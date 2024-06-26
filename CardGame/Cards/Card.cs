﻿namespace SamMRoberts.CardGame.Cards;

public readonly struct Card : IComparable<Card>, IEquatable<Card>
{
    public readonly Enum Face;
    public readonly string FaceSymbol;
    public readonly Enum Suit;
    public readonly char SuitSymbol;

    public Card(Enum face, string faceSymbol, Enum suit)
    {
        Face = face;
        Suit = suit;
        FaceSymbol = faceSymbol;
        SuitSymbol = Convert.ToChar(suit);
    }

    public int CompareTo(Card other)
    {
        if ((int)(object)this.Face > (int)(object)other.Face) return 1;
        else if ((int)(object)this.Face < (int)(object)other.Face) return -1;
        else return 0;
    }

    public bool Equals(Card other)
    {
        if (((int)(object)this.Face == (int)(object)other.Face) && ((int)(object)this.Suit == (int)(object)other.Suit)) return true;
        else return false;
    }

    public override string ToString() => $"{FaceSymbol}{SuitSymbol}";
}