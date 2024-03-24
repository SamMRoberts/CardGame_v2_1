using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public class ConsoleHandler : IHandler<string>, IQueueable
    {
        private readonly IQueue _queue;
        private Dictionary<string, ICommand> _commands;

        public ConsoleHandler(IQueue queue)
        {
            _commands = new Dictionary<string, ICommand>();
            _queue = queue;
            LoadCommands();
        }

        public void Handle(string input)
        {
            if (IsCommand(input))
            {
                string[] parts = input.Split(' ');
                string command = (parts[0][1..]).ToLower();
                if (_commands.TryGetValue(command, out ICommand? value))
                {
                    Send(value);
                }
                else
                {
                    System.Console.WriteLine("Unknown command.");
                }
            }
            else
            {
                Send(new Command(() => System.Console.WriteLine("You think to yourself, \"" + input + "\"")));
            }
        }

        public void Send(ICommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            _queue.Enqueue(command);
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