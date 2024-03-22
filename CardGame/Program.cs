using SamMRoberts.CardGame.Cards;
using Standard = SamMRoberts.CardGame.Cards.Types.Standard;

IDeckBuilder<Standard.Faces, Standard.Suits> builder = new Builder<Standard.Faces, Standard.Suits>();
Deck<Card> deck =builder.BuildDeck();

/*
for (int i = 0; i < deck.Count; i++)
{
    Console.WriteLine($"{deck[i].FaceSymbol}{deck[i].SuitSymbol}");
}
*/
Console.WriteLine($"{deck[20].CompareTo(deck[6])}");
Console.WriteLine($"{deck[20].Equals(deck[6])}");
Console.WriteLine($"{deck[6].Equals(deck[6])}");
Console.WriteLine($"{deck[1].Equals(deck[2])}");
Console.WriteLine($"{deck[12].ToString()}");