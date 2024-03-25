using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games
{
    public class Dealer : IDealer
    {
        public IGame Game { get; }
        public string Name { get; set; }
        public int Score { get; set; }
        public Cards.Hand<Cards.CardHolder> Hand { get; set; }

        public Dealer(string name, IGame game)
        {
            Name = name;
            Game = game;
            Score = 0;
            Hand = new();
        }

        public void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Deal(deck, player, Cards.CardHolder.Visibility.VisibleToOwner);
            }
        }

        public void Deal(Cards.IDeck<Cards.Card> deck, IPlayer player, Cards.CardHolder.Visibility visibility)
        {
            player.Hand.AddLast(new CardHolder(deck.GetTop(), visibility));
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

        private static Random random = new Random(0);
        private static int GenerateRandomNumber(int from, int to) => random.Next(from, to);
    }
}