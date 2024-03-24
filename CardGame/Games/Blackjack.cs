using Standard = SamMRoberts.CardGame.Cards.Types.Standard;
using SamMRoberts.CardGame.Management;
using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games
{
    public static class BlackjackCommands
    {
        public static Dictionary<string, ICommand> GetCommands()
        {
            return new Dictionary<string, ICommand>
            {
                { "hit", new Command(() => System.Console.WriteLine("Hit!")) },
                { "stand", new Command(() => System.Console.WriteLine("Stand!")) },
                //{ "double", new Command(() => System.Console.WriteLine("Double!")) },
                //{ "split", new Command(() => System.Console.WriteLine("Split!")) },
                //{ "surrender", new Command(() => System.Console.WriteLine("Surrender!")) }
            };
        }
    }

    public class Blackjack : IGame
    {
        private IDeck<Card> _deck;
        private IPlayer _player;
        private IDealer _dealer;
        private Components.IInteractiveConsole _console;
        private IHandler<string> _handler;
        private IDeckBuilder<Standard.Faces, Standard.Suits> _builder;

        public IDeck<Card> Deck { get => _deck; }

        public Blackjack(Components.IInteractiveConsole console, IHandler<string> handler)
        {
            _builder = new Builder<Standard.Faces, Standard.Suits>();
            _deck = _builder.BuildDeck();
            _player = new Games.Player("Player 1", this);
            _dealer = new Games.Dealer("Dealer", this);
            _console = console;
            _handler = handler;
        }

        public void Start()
        {
            _dealer.GetAndShuffle();

            foreach (var card in _deck)
            {
                _console.WriteLine(card.ToString());
            }

            /*
            _deck.Shuffle();
            _player.Hand.Add(_deck.Draw());
            _dealer.Hand.Add(_deck.Draw());
            _player.Hand.Add(_deck.Draw());
            _dealer.Hand.Add(_deck.Draw());
            */
            _console.WriteLine("Player hand: " + _player.Hand);
            _console.WriteLine("Dealer hand: " + _dealer.Hand);
            //_handler.Start();  // Not implemented yet
        }

        public void Restart()
        {
            _deck = _builder.BuildDeck();
            _player.Hand.Clear();
            _dealer.Hand.Clear();
            Start();
        }

        public IDeck<Card> PickupDeck()
        {
            var temp = _deck;
            _deck = null;
            return temp;
        }

        public void PlaceDeck(IDeck<Card> deck)
        {
            _deck = deck;
        }
    }
}