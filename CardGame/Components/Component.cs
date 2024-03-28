using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components;

public abstract class Component : IComponent
{
    protected string Name;

    public Component(string name)
    {
        this.Name = name;
    }

    public IMediator Mediator { get; set; }

    public abstract void Send(ICommand command);
    public abstract void Receive(ICommand command);

    public void SetMediator(IMediator mediator)
    {
        this.Mediator = mediator;
    }
}