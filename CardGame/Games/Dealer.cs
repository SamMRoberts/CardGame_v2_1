using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games;

public class Dealer : IDealer
{
    private static Random random = new Random(0);

    public Dealer(string name, IGame game)
    {
        Name = name;
        Game = game;
        Hand = new();
    }

    public Dealer(string name, IGame game, int handSize)
    {
        Name = name;
        Game = game;
        Hand = new(handSize);
    }

    public IGame Game { get; }
    public string Name { get; set; }
    public Cards.Hand<Cards.CardHolder> Hand { get; set; }

    public void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Deal(deck, player, Cards.CardHolder.Visibility.FaceDown);
        }
    }

    public void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility, bool display = true)
    {
        player.Hand.AddLast(new CardHolder(player, deck.GetTop(), visibility));
        if (display)
            player.DisplayHand();
    }

    public void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility)
    {
        player.Hand.AddLast(new CardHolder(player, deck.GetTop(), visibility));
        player.DisplayHand();
    }

    public void Deal(Cards.IDeck<Cards.Card> deck, int count)
    {
        for (int i = 0; i < count; i++)
        {
            //foreach (IPlayer player in Game.Player)
            //{
                //player.Hand.AddLast(deck.GetTop());
            //}
        }
    }

    public IDeck<Cards.Card> GetDeck()
    {
        return Game.PickupDeck();
    }

    public void PutDeck(IDeck<Card> deck)
    {
        Game.PlaceDeck(deck);
    }

    public IDeck<Card> Shuffle(IDeck<Card> deck)
    {
        for (int i = 0; i < deck.Count - 1; i++)
        {
            int j = GenerateRandomNumber(i, deck.Count);
            (deck[j], deck[i]) = (deck[i], deck[j]);
        }
        return deck;
    }

    public void GetAndShuffle()
    {
        var deck = GetDeck();
        Game.PlaceDeck(Shuffle(deck));
    }

    public IEnumerable<T> Shuffle<T>(IEnumerable<T> list)
    {
        throw new NotImplementedException();
    }

    private static int GenerateRandomNumber(int from, int to)
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        return random.Next(from, to);
    }
}