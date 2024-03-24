using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public class Console : IInteractiveConsole
    {
        private object _inputLock = new object();
        private IHandler<string> _handler;
        public IHandler<string> Handler => _handler;

        public Console(IQueue queue)
        {
            _handler = new ConsoleHandler(queue);
        }

        public void Write(string message)
        {
            System.Console.Write(FormatMessage(message));
        }

        public void WriteLine(string message)
        {
            System.Console.WriteLine(FormatMessage(message));
        }

        public string ReadLine()
        {
            return System.Console.ReadLine() ?? string.Empty;
        }

        public async Task<string> ReadLineAsync()
        {
            return await System.Console.In.ReadLineAsync() ?? string.Empty;
        }

        public void Start()
        {
            Task listener = Task.Factory.StartNew(Listen);
            listener.Wait();
        }

        private string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss.ffff");
        }

        private string FormatMessage(string message)
        {
            return $"[{GetTimestamp()}] {message}";
        }

        private async void Listen()
        {
            string? input;
            do
            {
            input = await ReadLineAsync();
                if (input != null)
                {
                    //ICommand command = CreateCommand(() => WriteLine($"Your input was: {input}"));
                    HandleInput(input);
                }
            } while (true);
        }

        private static Command CreateCommand(Action task)
        {
            return new Command(task);
        }

        private void HandleInput(string input)
        {
            _handler.Handle(input);
        }
    }
}