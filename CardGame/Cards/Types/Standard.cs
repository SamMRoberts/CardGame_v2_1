namespace SamMRoberts.CardGame.Cards.Types.Standard;

public enum Faces
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public enum Suits
{
    Clubs = '♣',
    Diamonds = '♦',
    Hearts = '♥',
    Spades = '♠'
}

public enum Colors
{
    Red,
    Black
}

public static class CardEvaluator<TFace>
{
    public delegate string GetFaceSymbol(TFace face);
    public static readonly GetFaceSymbol GetFaceSymbolDelegate = GetFaceSymbolMethod;
    private static string GetFaceSymbolMethod(TFace face) => face switch
    {
        Faces.Two => "2",
        Faces.Three => "3",
        Faces.Four => "4",
        Faces.Five => "5",
        Faces.Six => "6",
        Faces.Seven => "7",
        Faces.Eight => "8",
        Faces.Nine => "9",
        Faces.Ten => "10",
        Faces.Jack => "J",
        Faces.Queen => "Q",
        Faces.King => "K",
        Faces.Ace => "A",
        _ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
    };
}