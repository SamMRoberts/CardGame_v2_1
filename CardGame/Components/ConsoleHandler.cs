using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public class ConsoleHandler : Component, IHandler<string>, IQueueable
    {
        private Dictionary<string, ICommand> _commands;
        private ILogger _logger;

        public ConsoleHandler(IMediator mediator, ILogger logger) : base(mediator)
        {
            _mediator = mediator;
            _mediator.Register(this);
            _logger = logger;
            _commands = new Dictionary<string, ICommand>();
            LoadCommands();
            LoadExternalCommands(Games.BlackjackCommands.GetCommands());
        }

        public void Handle(string input)
        {
            ArgumentNullException.ThrowIfNull(input);
            _logger.Log(input);

            if (IsCommand(input))
            {
                string[] parts = input.Split(' ');
                string command = (parts[0][1..]).ToLower();
                if (_commands.TryGetValue(command, out ICommand? value))
                {
                    _mediator.Notify(this, value);
                }
                else
                {
                    System.Console.WriteLine("Unknown command.");
                }
            }
            else
            {
                _mediator.Notify(this, new Command(() => System.Console.WriteLine("You think to yourself, \"" + input + "\"")));
            }
        }

        public void Send(ICommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            _mediator.Notify(this, command);
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        private static bool IsCommand(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return input[..1] == "/";
        }

        private void RegisterCommand(string command, ICommand action)
        {
            _commands.Add(command, action);
        }

        private void UnregisterCommand(string command)
        {
            _commands.Remove(command);
        }

        private void LoadCommands()
        {
            RegisterCommand("help", new Command(() => System.Console.WriteLine("Help!")));
            RegisterCommand("exit", new Command(() => System.Environment.Exit(0)));
            RegisterCommand("showlog", new Command(() => _logger.ShowLog()));
        }

        public void LoadExternalCommands(Dictionary<string, ICommand> commands)
        {
            if (commands == null)
            {
                return;
            }
            foreach (KeyValuePair<string, ICommand> command in commands)
            {
                if (command.Value != null)
                RegisterCommand(command.Key, command.Value);
            }
        }
    }
}