using Standard = SamMRoberts.CardGame.Cards.Types.Standard;
using SamMRoberts.CardGame.Management;
using SamMRoberts.CardGame.Cards;

namespace SamMRoberts.CardGame.Games;

public class Blackjack : Components.Component, IGame
{
    private IDeck<Card> _deck;
    private IPlayer _player;
    protected IDealer _dealer;
    private Components.IInteractiveConsole _console;
    private IHandler<string> _handler;
    private IDeckBuilder<Standard.Faces, Standard.Suits> _builder;
    private IQueue _queue;

    public Blackjack(string name, Components.IInteractiveConsole console, IHandler<string> handler, IQueue queue) : base(name)
    {
        var handSize = 14;
        Name = name;
        _builder = new Builder<Standard.Faces, Standard.Suits>();
        _deck = _builder.BuildDeck(Standard.CardEvaluator<Standard.Faces>.GetFaceSymbolDelegate);
        _player = new Games.Player("Player 1", this, handSize);
        _dealer = new Games.Dealer("Dealer", this, handSize);
        _console = console;
        _handler = handler;
        _queue = queue;
        Welcome();
    }

    public IPlayer Player { get => _player; }
    public IDeck<Card> Deck { get => _deck; }


    public void Start()
    {

        _handler.LoadExternalCommands(GetCommands());
        _dealer.GetAndShuffle();
        InitialDeal();
    }

    public void Restart()
    {
        _deck = _builder.BuildDeck(Standard.CardEvaluator<Standard.Faces>.GetFaceSymbolDelegate);
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

    public Dictionary<string, ICommand> GetCommands()
    {
        return new Dictionary<string, ICommand>
        {
            { "hit", new Command(() => this._dealer.Deal(_deck, _player, CardHolder.Visibility.FaceUp))},
            { "stand", new Command(() => System.Console.WriteLine("Stand!")) },
        };
    }

    public override void Receive(ICommand command)
    {
        System.Diagnostics.Debug.WriteLine(this.Name + ": Received command.");
    }

    public override void Send(ICommand command)
    {
        System.Diagnostics.Debug.WriteLine(this.Name + ": Sending command.");
        //_queue.Enqueue(command);
    }

    private void Welcome()
    {
        System.Console.Clear();
        _console.WriteLine("Welcome to Blackjack!\n");
        _console.WriteLine("Commands: /help, /exit, /showlog, /hit, /stand\n");
    }

    private void WriteHand(IPlayer player)
    {
        _console.Write($"{player.Name} hand:");

        for (int i = 0; i < player.Hand.Count; i++)
        {
            _console.Write(player.Hand[i].ToString());
        }

    }

    private void InitialDeal()
    {
        _dealer.Deal(_deck, _player, CardHolder.Visibility.FaceUp, false);
        _dealer.Deal(_deck, _dealer, CardHolder.Visibility.FaceDown, false);
        _dealer.Deal(_deck, _player, CardHolder.Visibility.FaceUp);
        _dealer.Deal(_deck, _dealer, CardHolder.Visibility.FaceUp);
    }
}