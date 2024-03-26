using System.Windows.Input;
using SamMRoberts.CardGame.Management;

namespace SamMRoberts.CardGame.Components
{
    public interface IInteractiveConsole : IWriter, IAsyncReader<string>, IComponent
    {
        //public IHandler<string> Handler { get; }
        void Start();
    }
}