using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games;

public interface IDealer : IPlayer, IShuffler
{
    void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, int count);    // Deal a specific number of cards to a player
    void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility);     // Deal a single card to a player
    void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility, bool display = true);
    void Deal(Cards.IDeck<Cards.Card> deck, int count);    // Deal a specific number of cards to all players
    Cards.IDeck<Cards.Card> GetDeck();
    void PutDeck(Cards.IDeck<Cards.Card> deck);
    Cards.IDeck<Cards.Card> Shuffle(Cards.IDeck<Cards.Card> deck);
}

public interface IPlayer
{
    IGame Game { get; }
    string Name { get; set; }
    Cards.Hand<Cards.CardHolder> Hand { get; set; }
    void DisplayHand()
    {
        System.Console.WriteLine($"{this.Name} hand: {Cards.Extensions.HandExtensions.ToString(Hand)}");
    }
}

public interface IGame : Components.IComponent
{
    public IPlayer Player { get; }
    public Cards.IDeck<Cards.Card> Deck { get; }
    void Start();
    void Restart();
    Cards.IDeck<Cards.Card> PickupDeck();
    void PlaceDeck(Cards.IDeck<Cards.Card> deck);
}