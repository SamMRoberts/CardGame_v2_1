using System.Collections.Concurrent;
using SamMRoberts.CardGame.Components;

namespace SamMRoberts.CardGame.Management
{
    public class Manager : IManager, IQueue
    {
        private static Manager? _instance;
        private BlockingCollection<ICommand> _queue;
        private IInteractiveConsole _console;
        private Games.IGame _game;

        public Manager()
        {
            _queue = [];
            _console = new Components.Console(this);
            Start();
        }

        public void Enqueue(ICommand command)
        {
            _queue.Add(command);
        }

        public void Start()
        {
            _console.Handler.LoadExternalCommands(Games.BlackjackCommands.GetCommands());
            _game = new Games.Blackjack(_console, new Components.ConsoleHandler(this));
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
                    command?.Execute();
                }
                Thread.Sleep(100);
            }
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