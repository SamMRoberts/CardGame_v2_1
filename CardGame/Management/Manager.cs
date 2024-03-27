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
        private IHandler<string> _handler;
        private Dictionary<DateTime, string> _log;

        public Manager(string name = "Manager") : base(name)
        {
            Name = name;
            _log = new Dictionary<DateTime, string>();
            Mediator = new Mediator();
            Mediator.Register(this);
            _queue = [];
            _console = CreateAndRegisterComponent<Components.Console>("Console", this);
            Start();
        }

        public void Enqueue(ICommand command)
        {
            _queue.Add(command);
        }

        public void Start()
        {
            _handler = CreateAndRegisterComponent<Components.ConsoleHandler>(["ConsoleHandler", this]);
            _game = CreateAndRegisterComponent<Games.Blackjack>(["Blackjack", _console, _handler, this]);
            _game.Start();
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

        public override void Send(ICommand command)
        {
            System.Diagnostics.Debug.WriteLine(this.Name + ": Sending command.");
        }

        public override void Receive(ICommand command)
        {
            //System.Diagnostics.Debug.WriteLine(this.Name + ": Received command.");
            Enqueue(command);
        }

        private T CreateAndRegisterComponent<T>(params object[] args) where T : Component
        {
            T component = (T)Activator.CreateInstance(typeof(T), args);

            Mediator.Register(component);
            return component;
        }
    }
}