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
        public IPlayer Player { get => _player; }
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
            Welcome();
        }

        public void Start()
        {
            // TODO: Add logic to send to queue
            Task.Factory.StartNew(() => _dealer.GetAndShuffle());
            Task.Factory.StartNew(() =>  _dealer.Deal(_deck, _player, 2));
            Task.Factory.StartNew(() =>  _dealer.Deal(_deck, _dealer, 2));
            _console.WriteLine("Player hand: " + _player.Hand[0] + _player.Hand[1].FaceSymbol + ":" + _player.Hand[1].SuitSymbol);
            _console.WriteLine("Dealer hand: " + _dealer.Hand[0] + _dealer.Hand[1].ToString);

            /*
            _deck.Shuffle();
            _player.Hand.Add(_deck.Draw());
            _dealer.Hand.Add(_deck.Draw());
            _player.Hand.Add(_deck.Draw());
            _dealer.Hand.Add(_deck.Draw());
            */
            //_console.WriteLine("Player hand: " + _player.Hand);
            //_console.WriteLine("Dealer hand: " + _dealer.Hand);
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

        private void Welcome()
        {
            System.Console.Clear();
            _console.WriteLine("Welcome to Blackjack!\n");;
        }
    }
}