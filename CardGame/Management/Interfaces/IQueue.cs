using System.ComponentModel;

namespace SamMRoberts.CardGame.Management
{
    public interface IQueue : Components.IComponent
    {
        void Enqueue(ICommand command);
    }
}