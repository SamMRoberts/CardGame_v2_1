using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public class Console : Component, IInteractiveConsole
    {
        public Console(IQueue queue, IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
            _mediator.Register(this);
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

        private static string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss.ffff");
        }

        private static string FormatMessage(string message)
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
            _mediator.Notify(this, input);
        }
    }
}