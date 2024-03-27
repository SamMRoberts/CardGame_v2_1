using System.ComponentModel;

namespace SamMRoberts.CardGame.Management
{
    public interface IHandler<T> : Components.IComponent
    {
        void Handle(T input);
        void Send(ICommand command);
        void Start();
        void LoadExternalCommands(Dictionary<string, ICommand> commands);
    }
}