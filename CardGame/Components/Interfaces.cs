namespace SamMRoberts.CardGame.Components;

public interface IAsyncReader<T>
{
    Task<T> ReadLineAsync();
}

public interface IComponent
{
    void SetMediator(Management.IMediator mediator);
    void Receive(Management.ICommand command);
    void Send(Management.ICommand command);
}

public interface IWriter
{
    void Write(string message);
    void WriteLine(string message);
}

public interface IInteractiveConsole : IWriter, IAsyncReader<string>, IComponent
{
    //public IHandler<string> Handler { get; }
    void Start();
}