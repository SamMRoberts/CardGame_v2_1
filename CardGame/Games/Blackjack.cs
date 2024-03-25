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
            };
        }
    }

    public class Blackjack : IGame, IQueueable
    {
        public IPlayer Player { get => _player; }
        private IDeck<Card> _deck;
        private IPlayer _player;
        private IDealer _dealer;
        private Components.IInteractiveConsole _console;
        private IHandler<string> _handler;
        private IDeckBuilder<Standard.Faces, Standard.Suits> _builder;
        private IQueue _queue;


        public IDeck<Card> Deck { get => _deck; }

        public Blackjack(Components.IInteractiveConsole console, IHandler<string> handler, IQueue queue)
        {
            _builder = new Builder<Standard.Faces, Standard.Suits>();
            _deck = _builder.BuildDeck();
            _player = new Games.Player("Player 1", this);
            _dealer = new Games.Dealer("Dealer", this);
            _console = console;
            _handler = handler;
            _queue = queue;
            Welcome();
        }

        public void Start()
        {
            Send(new Command(() => _dealer.GetAndShuffle()));
            Send(new Command(() => _dealer.Deal(_deck, _player, 2)));
            Send(new Command(() => _dealer.Deal(_deck, _dealer, 2)));
            Send(new Command(() => _console.WriteLine($"Player hand: {_player.Hand[0]} {_player.Hand[1]}")));
            Send(new Command(() => _console.WriteLine($"Dealer hand: {_dealer.Hand[0]} {_dealer.Hand[1]}")));

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
            _console.WriteLine("Welcome to Blackjack!\n");
        }

        public void Send(ICommand command)
        {
            _queue.Enqueue(command);
        }
    }
}