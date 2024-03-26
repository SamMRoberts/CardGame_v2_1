using System.Collections.Concurrent;
using System.Reflection;
using SamMRoberts.CardGame.Components;

namespace SamMRoberts.CardGame.Management
{
    public class Manager : Component, IManager, IQueue, ILogger
    {
        private static Manager? _instance;
        private BlockingCollection<ICommand> _queue;
        private IInteractiveConsole _console;
        private Games.IGame _game;
        private Dictionary<DateTime, string> _log;

        public Manager()
        {
            _log = new Dictionary<DateTime, string>();
            _mediator = new Mediator(this);
            _mediator.Register(this);
            _queue = [];
            _console = new Components.Console(this, _mediator);
            Start();
        }

        public void Enqueue(ICommand command)
        {
            _queue.Add(command);
        }

        public void Start()
        {
            _game = new Games.Blackjack(_console, new Components.ConsoleHandler(_mediator, this), this, _mediator);
            _game.Start();
            //Task.Factory.StartNew(Test);
            Task process = Task.Factory.StartNew(Process);
            _console.Start();
            process.Wait();
        }

        public void Exit()
        {
            _console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        static Manager IManager.Instance()
        {
            _instance ??= new Manager();
            return _instance;
        }

        private void Process()
        {
            while (true)
            {
                if (_queue.TryTake(out ICommand? command))
                {
                    Task.Factory.StartNew(() => command.Execute());
                }
                Thread.Sleep(100);
            }
        }

        public void ShowLog()
        {
            foreach (KeyValuePair<DateTime, string> entry in _log)
            {
                System.Console.WriteLine($">[{entry.Key.ToString("HH:mm:ss.ffff")}]: {entry.Value}");
            }
        }

        public void Log(string message)
        {
            if (message == null)
                return;
            _log.Add(DateTime.Now, message);
        }


        private void Test()
        {
            int i = 0;
            do
            {
                ICommand command = new Command(() => _console.WriteLine("Hello, World! " + i));
                Enqueue(command);
                i++;
                Thread.Sleep(1000);
            } while (true);
        }
    }
}