namespace SamMRoberts.CardGame.Management;

public interface ICommand
{
    Action Action { get; }
    void Execute();
}

public interface IHandler<T> : Components.IComponent
{
    void Handle(T input);
    void Send(ICommand command);
    void Start();
    void LoadExternalCommands(Dictionary<string, ICommand> commands);
}

public interface ILogger
{
    void ShowLog();
    void Log(string message);
}

public interface IManager
{
    abstract static CardGame.Management.Manager Instance();
}

public interface IMediator
{
    public void Send(Components.IComponent component, ICommand command);
    void Send(Components.IComponent component, string ev);
    void Register(Components.IComponent component);
}

public interface IQueue : Components.IComponent
{
    void Enqueue(ICommand command);
}